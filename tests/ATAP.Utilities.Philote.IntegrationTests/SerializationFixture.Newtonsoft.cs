
using System;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace ATAP.Utilities.Philote.IntegrationTests {
  // The SerializationSystemTextJsonFixture can only be setup one time, before all tests are run
  //  because JsonSerializerSettings cannot be modified after any Serialization/Deserialization operations have been performed
  public class SerializationFixtureNewtonsoft {
    public JsonSerializerSettings JsonSerializerSettings { get; set; }
    public SerializationFixtureNewtonsoft() {
      JsonSerializerSettings = new JsonSerializerSettings();
      // Add Converters
      // JsonSerializerSettings.Converters.Add(new ATAP.Utilities.Philote.JsonConverterNewtonsoft.PhiloteNewtonsoftJsonConverter());
    }
  }

  public partial class PhiloteSerializationNewtonsoftUnitTests001 {
    protected SerializationFixtureNewtonsoft SerializationFixture { get; }
    protected ITestOutputHelper TestOutput { get; }

    public PhiloteSerializationNewtonsoftUnitTests001(ITestOutputHelper testOutput, SerializationFixtureNewtonsoft serializationFixture) {
      SerializationFixture = serializationFixture;
      TestOutput = testOutput;
      // ToDo: Ensure the System.StringComparison.CurrentCulture is configured properly to match the test data, for String.StartsWith used in the tests
    }
  }
}
