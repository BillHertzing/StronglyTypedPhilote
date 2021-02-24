
using System;
using Newtonsoft.Json;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using System.Reflection;
using ATAP.Utilities.StronglyTypedID;
using ATAP.Utilities.StronglyTypedIDs.JsonConverterNewtonsoft;

using System.ComponentModel;

namespace ATAP.Utilities.StronglyTypedId.UnitTests {
  // The SerializationFixture can only be setup one time, before all tests are run
  //  because JsonSerializerSettings cannot be modified after any Serialization/Deserialization operations have been performed
  public class SerializationFixtureNewtonsoft {
    public JsonSerializerSettings JsonSerializerSettings { get; set; }
    public SerializationFixtureNewtonsoft() {
      JsonSerializerSettings = new JsonSerializerSettings();
      // Add Converters
      JsonSerializerSettings.Converters.Add(new ATAP.Utilities.StronglyTypedIDs.JsonConverterNewtonsoft.StronglyTypedIdNewtonsoftJsonConverter());
    }
  }

  public partial class StronglyTypedIDSerializationNewtonsoftUnitTests001 {
    protected SerializationFixtureNewtonsoft SerializationFixture { get; }
    protected ITestOutputHelper TestOutput { get; }

    public StronglyTypedIDSerializationNewtonsoftUnitTests001(ITestOutputHelper testOutput, SerializationFixtureNewtonsoft serializationFixture) {
      SerializationFixture = serializationFixture;
      TestOutput = testOutput;
      // ToDo: Ensure the System.StringComparison.CurrentCulture is configured properly to match the test data, for String.StartsWith used in the tests
    }
  }
}
