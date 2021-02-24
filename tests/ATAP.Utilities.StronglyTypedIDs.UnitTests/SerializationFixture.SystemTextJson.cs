
using System;
using System.Text.Json;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using System.Reflection;
using ATAP.Utilities.StronglyTypedID;
using ATAP.Utilities.StronglyTypedIDs.JsonConverter.SystemTextJson;

using System.ComponentModel;

namespace ATAP.Utilities.StronglyTypedId.UnitTests {
  // The SerializationFixture can only be setup one time, before all tests are run
  //  because JsonSerializerOptions cannot be modified after any Serialization/Deserialization operations have been performed
  public class SerializationFixture {
    public JsonSerializerOptions JsonSerializerOptions { get; set; }
    public SerializationFixture() {
      JsonSerializerOptions = new JsonSerializerOptions();
      // Add Converters
      JsonSerializerOptions.Converters.Add(new ATAP.Utilities.StronglyTypedIDs.JsonConverter.SystemTextJson.StronglyTypedIdJsonConverterFactory());

    }
  }


   public partial class StronglyTypedIDSerializationUnitTests001  {
    protected SerializationFixture SerializationFixture { get; }
    protected ITestOutputHelper TestOutput { get; }

    public StronglyTypedIDSerializationUnitTests001(ITestOutputHelper testOutput, SerializationFixture serializationFixture) {
      SerializationFixture = serializationFixture;
      TestOutput = testOutput;
      // ToDo: Ensure the System.StringComparison.CurrentCulture is configured properly to match the test data, for String.StartsWith used in the tests
    }

   }
}
