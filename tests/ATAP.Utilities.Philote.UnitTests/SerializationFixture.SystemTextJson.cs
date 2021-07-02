
using System;
using System.Text.Json;

using Xunit.Abstractions;

namespace ATAP.Utilities.Philote.UnitTests {
  // The SerializationSystemTextJsonFixture can only be setup one time, before all tests are run
  //  because JsonSerializerSettings cannot be modified after any Serialization/Deserialization operations have been performed
  public class SerializationSystemTextJsonFixture {
    public JsonSerializerOptions JsonSerializerOptions { get; set; }
    public SerializationSystemTextJsonFixture() {
      JsonSerializerOptions = new JsonSerializerOptions();
      // Add Converters
      JsonSerializerOptions.Converters.Add(new ATAP.Utilities.StronglyTypedIds.JsonConverter.Shim.SystemTextJson.StronglyTypedIdJsonConverterFactory());
    }
  }

  public partial class PhiloteSerializationSystemTextJsonUnitTests001 {
    protected SerializationSystemTextJsonFixture SerializationFixture { get; }
    protected ITestOutputHelper TestOutput { get; }

    public PhiloteSerializationSystemTextJsonUnitTests001(ITestOutputHelper testOutput, SerializationSystemTextJsonFixture serializationFixture) {
      SerializationFixture = serializationFixture;
      TestOutput = testOutput;
      // ToDo: Ensure the System.StringComparison.CurrentCulture is configured properly to match the test data, for String.StartsWith used in the tests
    }
  }
}
