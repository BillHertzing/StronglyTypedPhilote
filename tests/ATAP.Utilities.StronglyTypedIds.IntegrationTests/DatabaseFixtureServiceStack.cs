
using System.Collections.Concurrent;
using System.Data;
using System.Linq;
using System;
using System.Text.Json;
using System.Reflection;
using Microsoft.Extensions.Configuration;

// setup a database to be used to test concrete StronglyTypedId types storage and recall
using ATAP.Utilities.Testing;
using ServiceStack;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using ServiceStack.DataAnnotations;
using ServiceStack.Text;

using ServiceStack.Testing;
using Xunit.Abstractions;

using GenericHostExtensions = ATAP.Utilities.GenericHost.Extensions;
using ConfigurationExtensions = ATAP.Utilities.Configuration.Extensions;
using TestingExtensions = ATAP.Utilities.Testing.Extensions;

using ATAP.Utilities.StronglyTypedIds;
using Itenso.TimePeriod;

namespace ATAP.Utilities.StronglyTypedIds.IntegrationTests {
  // The DatabaseFixtureServiceStackMSSQL should be setup one time, before all tests are run
  public class DatabaseFixtureServiceStackMSSQL : DatabaseFixture, IDisposable {

    // The list of environment prefixes this test will recognize
    public static string[] commonTestEnvPrefixes = new string[1] { "CommonTest_" };
    public static string[] specificTestEnvPrefixes = new string[1] { "StronglyTypedIdsIntegrationTest_" };

    private IConfiguration TestClassConfigurationRoot { get; }
    private IConfiguration TestHostConfigurationRoot { get; }

    /// <summary>
    /// Used by ServiceStack OrmLite packages
    /// </summary>
    private IOrmLiteDialectProvider Provider { get; }

    /// <summary>
    /// part of the Disposing pattern to detect redundant calls
    /// </summary>
    private bool _disposed = false;
    /// <summary>
    /// Default Constructor
    /// </summary>
    public DatabaseFixtureServiceStackMSSQL() : base() {


      #region initial testHostConfigurationBuilder and testHostConfigurationRoot
      // Create the initial testHostConfigurationBuilder for this testHost's ConfigurationRoot. This creates an ordered chain of configuration providers. The first providers in the chain have the lowest priority, the last providers in the chain have a higher priority.
      // Initial configuration does not take Environment into account.
      var testHostConfigurationBuilder = ConfigurationExtensions.ATAPStandardConfigurationBuilder(

        TestingDefaultConfiguration.Production,
        true,
        null,
        TestingStringConstants.genericTestSettingsFileName,
        TestingStringConstants.genericTestSettingsFileSuffix,
        LoadedFromDirectory,
        InitialStartupDirectory,
        commonTestEnvPrefixes,
        null,
        null);

      // Create this program's initial testHost's ConfigurationRoot
      TestHostConfigurationRoot = testHostConfigurationBuilder.Build();
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
      // ToDo: Before the testHost is built, have to use a StringConstant for the string that means "Production", and hope the ConfigurationRoot value for Environment matches the StringConstant
      // Determine the environment (Debug, TestingUnit, TestingX, QA, QA1, QA2, ..., Staging, Production) to use from the initialtestHostConfigurationRoot
      var envNameFromConfiguration = TestHostConfigurationRoot.GetValue<string>(TestingStringConstants.EnvironmentConfigRootKey, TestingStringConstants.EnvironmentDefault);

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
        // Recreate the ConfigurationBuilder for this testHost, this time including environment-specific configuration providers.
        testHostConfigurationBuilder = ConfigurationExtensions.ATAPStandardConfigurationBuilder(TestingDefaultConfiguration.Production,
          false,
          envNameFromConfiguration,
          TestingStringConstants.genericTestSettingsFileName,
          TestingStringConstants.genericTestSettingsFileName,
          LoadedFromDirectory,
          InitialStartupDirectory,
          commonTestEnvPrefixes,
          null,
          null);
      }

      // Create this program's final testHost's ConfigurationRoot
      TestHostConfigurationRoot = testHostConfigurationBuilder.Build();

      // Create the testClassConfigurationBuilder, either as Production or as some other environment specific
      IConfigurationBuilder testClassConfigurationBuilder;
      testClassConfigurationBuilder = ConfigurationExtensions.ATAPStandardConfigurationBuilder(
        TestingDefaultConfiguration.Production,
        envNameFromConfiguration == TestingStringConstants.EnvironmentDefault,
        envNameFromConfiguration,
        StronglyTypedIdsIntegrationTestsStringConstants.TestClassSettingsFileName,
        StronglyTypedIdsIntegrationTestsStringConstants.TestClassSettingsFileNameSuffix,
        LoadedFromDirectory,
        InitialStartupDirectory,
        specificTestEnvPrefixes,
        null,
        null);
      #endregion

      TestClassConfigurationRoot = testClassConfigurationBuilder.Build();
      DatabaseName = TestClassConfigurationRoot.GetValue<string>(TestingStringConstants.DatabaseNameConfigRootKey, TestingStringConstants.DatabaseNameDefault);
      // ToDo: Move items specific to an integration technology into separate packages
      IOrmLiteDialectProvider provider;
      switch (envNameFromConfiguration) {
        case TestingStringConstants.EnvironmentUnitTest: {
            provider = SqlServer2017Dialect.Provider; // ToDo: remove this
            break;
          }
        case TestingStringConstants.EnvironmentMSSQLIntegrationTest: {
            throw new NotSupportedException($"The environment {envNameFromConfiguration} is not supported.");
          }
        case TestingStringConstants.EnvironmentMySQLIntegrationTest: {
            throw new NotSupportedException($"The environment {envNameFromConfiguration} is not supported.");
          }
        case TestingStringConstants.EnvironmentSQLLiteIntegrationTest: {
            throw new NotSupportedException($"The environment {envNameFromConfiguration} is not supported.");
          }
        case TestingStringConstants.EnvironmentSSOrmLiteMSSQLIntegrationTest: {
            ConnectionString = TestClassConfigurationRoot.GetValue<string>(TestingStringConstants.DatabaseConnectionStringConfigRootKey, TestingStringConstants.DatabaseConnectionStringDefault); ;
            var ProviderString = TestClassConfigurationRoot.GetValue<string>(TestingStringConstants.DatabaseProviderConfigRootKey, TestingStringConstants.DatabaseProviderDefault);
            switch (ProviderString) {
              case "SqlServer2017Dialect.Provider": {
                  Provider = SqlServer2017Dialect.Provider;
                  break;
                }
              default: throw new NotSupportedException($"The DatabaseProvider {ProviderString} is not supported.");
            }
            break;
          }
        case TestingStringConstants.EnvironmentSSOrmLiteMySQLIntegrationTest: {
            throw new NotSupportedException($"The environment {envNameFromConfiguration} is not supported.");
          }
        case TestingStringConstants.EnvironmentSSOrmLiteSQLLiteIntegrationTest: {
            throw new NotSupportedException($"The environment {envNameFromConfiguration} is not supported.");
          }
        case TestingStringConstants.EnvironmentTestDapperMSSQL: {
            throw new NotSupportedException($"The environment {envNameFromConfiguration} is not supported.");
          }
        case TestingStringConstants.EnvironmentTestDapperMySQL: {
            throw new NotSupportedException($"The environment {envNameFromConfiguration} is not supported.");
          }
        case TestingStringConstants.EnvironmentTestDapperSQLite: {
            throw new NotSupportedException($"The environment {envNameFromConfiguration} is not supported.");
          }
        case TestingStringConstants.EnvironmentEFCoreIntegrationTest: {
            throw new NotSupportedException($"The environment {envNameFromConfiguration} is not supported.");
          }
        // ToDo: Localize exception messages for tests?
        default: throw new NotSupportedException($"The environment {envNameFromConfiguration} is not supported.");
      }
      //SqlServer2017Dialect.Provider;
      // $"Server={host}:{port};Database={databaseName};Trusted_Connection=True";
      //ConnectionString = $"Server={host};Integrated Security=True;Database={databaseName};MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Application Name=Testing";

      //  validate the Database connection
      // Only for ServiceStack OrmLite database technology
      OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConnectionString, Provider);
      // ToDo: In ATAP DB Management package, create a function which raises a custom exception?
      if (dbFactory == null) { throw new Exception($"Failed to create a dbFactory with the connection string: \"{ConnectionString}\""); }

      // make an IDbConnection to the database server
      Db = dbFactory.Open();
      if (Db == null) { throw new Exception($"Failed to open the database server with the connection string: \"{ConnectionString}\""); }

      // Does the database already exist? Delete it if so
      // ToDo: Move database creation into the testing package
      Db.CreateDatabaseServiceStack(DatabaseName);

      // Create an empty database. Delete the database if it already exists

      // ToDo: apply the Flyway migrations to initialize the database

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
        // AppHost?.Dispose();
        Db?.Close();

      }


      _disposed = true;
    }

  }


  public partial class StronglyTypedIdDatabaseServiceStackMSSQLIntegrationTests001 {
    protected DatabaseFixtureServiceStackMSSQL DatabaseFixture { get; }
    protected ITestOutputHelper TestOutput { get; }

    public StronglyTypedIdDatabaseServiceStackMSSQLIntegrationTests001(ITestOutputHelper testOutput, DatabaseFixtureServiceStackMSSQL databaseFixture) {
      DatabaseFixture = databaseFixture;
      TestOutput = testOutput;
      // ToDo: Ensure the System.StringComparison.CurrentCulture is configured properly to match the test data, for String.StartsWith used in the tests
    }
  }
}
