
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
  //  Because JsonSerializerOptions cannot be modified after any Serialization/Deserialization operations have been performed
  public class SerializationFixture {
    public JsonSerializerOptions JsonSerializerOptions { get; set; }
    public SerializationFixture() {
      JsonSerializerOptions = new JsonSerializerOptions();
    }
  }

  [CollectionDefinition("Serialization collection")]
public class SerializationCollection : ICollectionFixture<SerializationFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

[Collection("Serialization collection")]
  public partial class StronglyTypedIDSerializationUnitTests001 : IClassFixture<SerializationFixture> {
    protected SerializationFixture SerializationFixture { get; }
    protected ITestOutputHelper TestOutput { get; }

    public StronglyTypedIDSerializationUnitTests001(ITestOutputHelper testOutput, SerializationFixture serializationFixture) {
      SerializationFixture = serializationFixture;
      TestOutput = testOutput;
      // Add Converters
      //SerializationFixture.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverter<GuidStronglyTypedId, Guid>());
      SerializationFixture.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverterFactory());
      // ToDo: Ensure the System.StringComparison.CurrentCulture is configured properly to match the test data, for String.StartsWith used in the tests

    }

  }

}
