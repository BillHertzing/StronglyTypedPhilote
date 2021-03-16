
using System;
using System.Collections;
using System.Collections.Generic;
using ATAP.Utilities.Collection;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

using System.ComponentModel;

// For the tests that use the new Serializer/Deserializer
using System.Text.Json;
// For the tests that use the old Newtonsoft Serializer/Deserializer
//using Newtonsoft.Json;

namespace ATAP.Utilities.Collection.UnitTests {
  // Attribution: https://github.com/xunit/xunit/issues/2007, however, we only need a class fixture not a collectionfixtire, so, commentedout below
  //  [CollectionDefinition(nameof(StronglyTypedIDSerializationSystemTextJsonUnitTests001), DisableParallelization = true)]
  //  [Collection(nameof(StronglyTypedIDSerializationSystemTextJsonUnitTests001))]
  public partial class CollectionExtensionSerializationSystemTextJsonUnitTests001 : IClassFixture<SerializationFixtureSystemTextJson> {

    [Theory]
    [MemberData(nameof(CollectionExtensionSerializationTestDataGenerator<HierDataClass>.CollectionExtensionSerializationTestData), MemberType = typeof(CollectionExtensionSerializationTestDataGenerator<HierDataClass>))]
    public void HierDataClassSerializeToJSON(CollectionExtensionSerializationTestData<HierDataClass> inTestData) {
      if (inTestData.SerializedTestData.StartsWith("\"0000", System.StringComparison.InvariantCulture) || inTestData.SerializedTestData.StartsWith("\"01234", System.StringComparison.InvariantCulture)) {
        // SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
      }
      else {
        //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().MatchRegex("^\"[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}\"$");
      }
    }

    [Theory]
    [MemberData(nameof(CollectionExtensionSerializationTestDataGenerator<HierDataClass>.CollectionExtensionSerializationTestData), MemberType = typeof(CollectionExtensionSerializationTestDataGenerator<HierDataClass>))]
    public void HierDataClassDeserializeFromJSON(CollectionExtensionSerializationTestData<HierDataClass> inTestData) {
      if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
        Action act = () => JsonSerializer.Deserialize<HierDataClass>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        act.Should().Throw<System.Text.Json.JsonException>()
          .WithMessage("The input does not contain any JSON tokens.*");
      }
      else if (inTestData.SerializedTestData.StartsWith("\"0000", System.StringComparison.InvariantCulture) || inTestData.SerializedTestData.StartsWith("\"01234", System.StringComparison.InvariantCulture)) {
        //SerializationFixtureSystemTextJson.Serializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
        var deserializedInstance = JsonSerializer.Deserialize<HierDataClass>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        deserializedInstance.Should().BeEquivalentTo(inTestData.InstanceTestData);
      }
      else {
        // ToDo: validate that strings that don't match a Guid throw an exception
      }
    }

    [Theory]
    [MemberData(nameof(CollectionExtensionSerializationTestDataGenerator<HierDataClass>.CollectionExtensionSerializationTestData), MemberType = typeof(CollectionExtensionSerializationTestDataGenerator<HierDataClass>))]
    public void IHierDataClassSerializeToJSON(CollectionExtensionSerializationTestData<HierDataClass> inTestData) {
      if (inTestData.SerializedTestData.StartsWith("\"0000", System.StringComparison.InvariantCulture) || inTestData.SerializedTestData.StartsWith("\"01234", System.StringComparison.InvariantCulture)) {
        // SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
      }
      else {
        //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().MatchRegex("^\"[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}\"$");
      }
    }

    [Theory]
    [MemberData(nameof(CollectionExtensionSerializationTestDataGenerator<HierDataClass>.CollectionExtensionSerializationTestData), MemberType = typeof(CollectionExtensionSerializationTestDataGenerator<HierDataClass>))]
    public void IHierDataClassDeserializeFromJSON(CollectionExtensionSerializationTestData<HierDataClass> inTestData) {
      if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
        Action act = () => JsonSerializer.Deserialize<HierDataClass>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        act.Should().Throw<System.Text.Json.JsonException>()
          .WithMessage("The input does not contain any JSON tokens.*");
      }
      else if (inTestData.SerializedTestData.StartsWith("\"0000", System.StringComparison.InvariantCulture) || inTestData.SerializedTestData.StartsWith("\"01234", System.StringComparison.InvariantCulture)) {
        //SerializationFixtureSystemTextJson.Serializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
        var deserializedInstance = JsonSerializer.Deserialize<IHierDataClass>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        deserializedInstance.Should().BeEquivalentTo(inTestData.InstanceTestData);
      }
      else {
        // ToDo: validate that strings that don't match a Guid throw an exception
      }
    }
  }
}
