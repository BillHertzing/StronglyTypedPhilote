
namespace ATAP.Utilities.StronglyTypedIds.UnitTests {
  public static class StronglyTypedIdsUnitTestsStringConstants {
    // ToDo: Localize the string constants

    #region Settings File Names
    public const string SettingsFileName = "StronglyTypedIdsIntegrationTestSettings";
    public const string SettingsFileNameSuffix = "json";
    #endregion
    #region File Names
    public const string TemporaryDirectoryBaseConfigRootKey = "TemporaryDirectoryBase";
    public const string TemporaryDirectoryBaseDefault = "./";
    #endregion
    #region StronglyTypedIdsIntegrationTestSettingsConfigRootKeys
    public const string configKeyMSSQLConnectionStringDefault = "Server=localhost;Integrated Security=true";
    public const string configKeyMySQLConnectionStringDefault = "MySQLConnectionString";
    public const string configKeySQLiteConnectionStringDefault = "SQLiteConnectionString";

    public const string OrmLiteDialectProviderConfigRootKey = "ORMLiteDialectProvider";
    public const string OrmLiteDialectProviderDefault = "SqlServerOrmLiteDialectProvider";
    #endregion


  }
}

