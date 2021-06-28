// setup a database to be used to test concrete StronglyTypedId types storage and recall
using ATAP.Utilities.Testing;
using System.Linq;
using ServiceStack;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using ServiceStack.DataAnnotations;
using System.Data;
using ServiceStack.Text;

using ATAP.Utilities.StronglyTypedIds;
using GenericHostExtensions = ATAP.Utilities.GenericHost.Extensions;
using ConfigurationExtensions = ATAP.Utilities.Configuration.Extensions;

using ServiceStack.Testing;
using System.Collections.Concurrent;
using Itenso.TimePeriod;

using System;

using System.Text.Json;
using Xunit.Abstractions;
using System.IO;
using System.Reflection;

using Microsoft.Extensions.Configuration;

namespace ATAP.Utilities.StronglyTypedIds.UnitTests {
  // The DatabaseFixtureServiceStackMSSQL should be setup one time, before all tests are run
  public class DatabaseFixtureServiceStackMSSQL : IDisposable {

        // The list of environment prefixes this test will use
    public static string[] commonTestEnvPrefixes = new string[1] { "CommonTest_" };
    public static string[] specificTestEnvPrefixes = new string[1] { "StronglyTypedIdsUnitTest_" };


    private ServiceStackHost AppHost { get; set; }
    private string ConnectionString { get; set; }
    public IDbConnection Db { get; set; }
    // To detect redundant calls
    private bool _disposed = false;
    public DatabaseFixtureServiceStackMSSQL() {


      // Environment settings for a SQL Server host and instance and connection string parameters

      #region initialStartup and loadedFrom directories
      // When running as a Windows service, the initial working dir is usually %WinDir%\System32, but the program (and configuration files) is probably installed to a different directory
      // When running as a *nix service, the initial working dir could be anything. The program (and machine-wide configuration files) are probably installed in the location where the service starts. //ToDo: verify this
      // When running as a Windows or Linux Console App, the initial working dir could be anything, but the program (and machine-wide configuration files) is probably installed to a different directory.
      // When running as a console app, it is very possible that there may be local (to the initial startup directory) configuration files to load
      // get the initial startup directory
      // get the directory where the executing assembly (usually .exe) and possibly machine-wide configuration files are installed to.
      var initialStartupDirectory = Directory.GetCurrentDirectory(); //ToDo: Catch exceptions
      var loadedFromDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); //ToDo: Catch exceptions
      #endregion

      #region initial testHostConfigurationBuilder and testHostConfigurationRoot
      // Create the initial testHostConfigurationBuilder for this genericHost's ConfigurationRoot. This creates an ordered chain of configuration providers. The first providers in the chain have the lowest priority, the last providers in the chain have a higher priority.
      // Initial configuration does not take Environment into account.
      var testHostConfigurationBuilder = ConfigurationExtensions.ATAPStandardConfigurationBuilder(
        TestingDefaultConfiguration.Production,
        true,
        null,
        TestingStringConstants.genericTestSettingsFileName,
        TestingStringConstants.genericTestSettingsFileSuffix,
        loadedFromDirectory,
        initialStartupDirectory,
        commonTestEnvPrefixes,
        null,
        null);

      // Create this program's initial genericHost's ConfigurationRoot
      var testHostConfigurationRoot = testHostConfigurationBuilder.Build();
      #endregion

      #region (optional) Debugging the  Configuration
      // for debugging and education, uncomment this region and inspect the two section Lists (using debugger Locals) to see exactly what is in the configuration
      //    var sections = testHostConfigurationRoot.GetChildren();
      //    List<IConfigurationSection> sectionsAsListOfIConfigurationSections = new List<IConfigurationSection>();
      //    List<ConfigurationSection> sectionsAsListOfConfigurationSections = new List<ConfigurationSection>();
      //    foreach (var iSection in sections) sectionsAsListOfIConfigurationSections.Add(iSection);
      //    foreach (var iSection in sectionsAsListOfIConfigurationSections) sectionsAsListOfConfigurationSections.Add((ConfigurationSection)iSection);
      #endregion

      #region Environment determination and validation
      // ToDo: Before the genericHost is built, have to use a StringConstant for the string that means "Production", and hope the ConfigurationRoot value for Environment matches the StringConstant
      // Determine the environment (Debug, TestingUnit, TestingX, QA, QA1, QA2, ..., Staging, Production) to use from the initialtestHostConfigurationRoot
      var envNameFromConfiguration = testHostConfigurationRoot.GetValue<string>(TestingStringConstants.EnvironmentConfigRootKey, TestingStringConstants.EnvironmentDefault);

      // optional: Validate that the environment provided is one this program understands how to use
      // Accepting any string for envNameFromConfiguration might pose a security risk, as it will allow arbitrary files to be loaded into the configuration root
      switch (envNameFromConfiguration) {
        case TestingStringConstants.EnvironmentDevelopment:
          // ToDo: Programmers can add things here
          break;
        case TestingStringConstants.EnvironmentProduction:
          // This is the expected leg for Production environment
          break;
        default:
          // IF you want to accept any environment name as OK, just comment out the following throw
          // Keep the throw in here if you want to explicitly disallow any environment other than ones specified in the switch
          // throw new NotImplementedException(exceptionLocalizer["The Environment {0} is not supported", envNameFromConfiguration]);
          break;
      };
      #endregion

      #region final (Environment-aware) testHostConfigurationBuilder and testClassConfigurationBuilder
      // If the initial testHostConfigurationRoot specifies the Environment is production, then the testHostConfigurationBuilder is correct  "as-is"
      //   but if not, build a 2nd (final) testHostConfigurationBuilder, this time including environment-specific configuration providers
      if (envNameFromConfiguration != TestingStringConstants.EnvironmentProduction) {
        // Recreate the ConfigurationBuilder for this genericHost, this time including environment-specific configuration providers.
        testHostConfigurationBuilder = ConfigurationExtensions.ATAPStandardConfigurationBuilder(GenericTestDefaultConfiguration.Production,
          false,
          envNameFromConfiguration,
          TestingStringConstants.genericTestSettingsFileName,
          TestingStringConstants.testSettingsFileNameSuffix,
          loadedFromDirectory,
          initialStartupDirectory,
          commonTestEnvPrefixes,
          null,
          null);
      }

      // Create the testClassConfigurationBuilder, either as Production or as some other environment specific
      IConfigurationBuilder testClassConfigurationBuilder;
      testClassConfigurationBuilder = ConfigurationExtensions.ATAPStandardConfigurationBuilder(
        StronglyTypedIdsUnitTestsDefaultConfiguration.Production,
        envNameFromConfiguration == TestingStringConstants.EnvironmentProduction,
        envNameFromConfiguration,
        Console03StringConstants.SettingsFileName,
        StronglyTypedIdsUnitTests.SettingsFileNameSuffix,
        loadedFromDirectory,
        initialStartupDirectory,
        specificTestEnvPrefixes,
        null,
        null);
      #endregion


      string host = "::1";
      int port = 1433;
      string databaseName = "StronglyTypedIdTestDatabase";
      IOrmLiteDialectProvider provider = SqlServer2017Dialect.Provider;
      // $"Server={host}:{port};Database={databaseName};Trusted_Connection=True";
      //ConnectionString = $"Server={host};Integrated Security=True;Database={databaseName};MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Application Name=Testing";
      ConnectionString = $"Server={host};Integrated Security=True;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Application Name=Testing";

      // Many ServiceStack ORM functions depend on the presence of a ServiceStack host
      // AppHost = new BasicAppHost().Init();

      //  validate the SQL Server instance connection
      OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConnectionString, provider);
      // ToDo: In ATAP DB Management package, create a function which raises a custom exception?
      if (dbFactory == null) { throw new Exception($"Failed to connect to any database with the connection string: \"{ConnectionString}\""); }

      // Does the database already exist? Delete it if so

      // Create the empty database using a Powershell script

      //  apply the Flyway migrations to initialize the database

      // make an IDbConnection to the database server
      Db = dbFactory.Open();
      if (dbFactory == null) { throw new Exception($"Failed to connect to any database with the connection string: \"{ConnectionString}\""); }

      //



      // Add Converters
      // JsonSerializerOptions.Converters.Add(new ATAP.Utilities.StronglyTypedIds.JsonConverter.Shim.SystemTextJson.StronglyTypedIdJsonConverterFactory());

    }

    ~DatabaseFixtureServiceStackMSSQL() => Dispose(false);

    // Public implementation of Dispose pattern callable by consumers.
    public void Dispose() {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing) {
      if (_disposed) {
        return;
      }

      if (disposing) {
        // Need to dispose of Apphost and Db, if they are open, when the Database Fixture is disposed
        AppHost?.Dispose();
        Db?.Close();

      }


      _disposed = true;
    }

  }


  public partial class StronglyTypedIdDatabaseServiceStackMSSQLUnitTests001 {
    protected DatabaseFixtureServiceStackMSSQL DatabaseFixture { get; }
    protected ITestOutputHelper TestOutput { get; }

    public StronglyTypedIdDatabaseServiceStackMSSQLUnitTests001(ITestOutputHelper testOutput, DatabaseFixtureServiceStackMSSQL databaseFixture) {
      DatabaseFixture = databaseFixture;
      TestOutput = testOutput;
      // ToDo: Ensure the System.StringComparison.CurrentCulture is configured properly to match the test data, for String.StartsWith used in the tests
    }
  }
}
