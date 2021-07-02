

namespace ATAP.Utilities.StronglyTypedIds.IntegrationTests {
  public static class StronglyTypedIdsIntegrationTestsStringConstants {
    // ToDo: Localize the string constants

    #region string constants: File Names
    public const string TestClassSettingsFileName = "Settings";
    public const string TestClassSettingsFileNameSuffix = ".json";
    #endregion

    #region string constants: Exception Messages
    #endregion

    #region string constants: ConfigKeys and default values for string-based Configkeys
    public const string TestConfigRootKey = "StronglyTypedIdsIntegrationTest";
    #endregion

    #region Connection Strings and API configuration keys
    public const string configKeyRedisConnectionString = "RedisConnectionString";
    public const string configKeyMSSQLConnectionString = "Server=::1;Integrated Security=True;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False";
    public const string configKeyMySQLConnectionString = "MySQLConnectionString";
    public const string configKeySQLiteConnectionString = "SQLiteConnectionString";
    public const string configKeyEFCoreConnectionString = "EFCoreConnectionString";
    public const string DbConnectionStringConfigRootKey = "DbConnectionString";
    #endregion

    #region string constants: EnvironmentVariablePrefixs
    public const string CustomEnvironmentVariablePrefix = "GenericTest_";
    #endregion
    
  }
}

