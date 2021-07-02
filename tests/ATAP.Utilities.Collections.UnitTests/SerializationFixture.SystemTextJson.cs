
using System;
using System.Text.Json;
using Xunit.Abstractions;
using ATAP.Utilities.Testing;


namespace ATAP.Utilities.Collection.IntegrationTests {

   public partial class CollectionExtensionSerializationSystemTextJsonUnitTests001  {
    protected SerializationSystemTextJsonFixture SerializationFixture { get; }
    protected ITestOutputHelper TestOutput { get; }

    public CollectionExtensionSerializationSystemTextJsonUnitTests001(ITestOutputHelper testOutput, SerializationSystemTextJsonFixture serializationFixture) {
      SerializationFixture = serializationFixture;
      TestOutput = testOutput;
      // ToDo: Ensure the System.StringComparison.CurrentCulture is configured properly to match the test data, for String.StartsWith used in the tests
    }
   }
}
