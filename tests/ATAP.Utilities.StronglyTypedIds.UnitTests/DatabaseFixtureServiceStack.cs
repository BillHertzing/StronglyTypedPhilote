// setup a database to be used to test concrete StronglyTypedId types storage and recall

using System.Linq;
using ServiceStack;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using ServiceStack.DataAnnotations;
using System.Data;
using ServiceStack.Text;

using ATAP.Utilities.StronglyTypedIds;

using ServiceStack.Testing;
using System.Collections.Concurrent;
using Itenso.TimePeriod;

using System;
using System.Text.Json;
using Xunit.Abstractions;

namespace ATAP.Utilities.StronglyTypedIds.UnitTests {
  // The DatabaseFixtureServiceStackMSSQL should be setup one time, before all tests are run
  public class DatabaseFixtureServiceStackMSSQL : IDisposable {
    private ServiceStackHost AppHost { get; set; }
    private string ConnectionString { get; set; }
    public IDbConnection Db { get; set; }
    // To detect redundant calls
    private bool _disposed = false;
    public DatabaseFixtureServiceStackMSSQL() {
      // Environment settings for a SQL Server host and instance and connection string parameters
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
