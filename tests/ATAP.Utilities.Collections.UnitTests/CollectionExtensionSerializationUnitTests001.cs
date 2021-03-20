
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
  //  [CollectionDefinition(nameof(StronglyTypedIdSerializationSystemTextJsonUnitTests001), DisableParallelization = true)]
  //  [Collection(nameof(StronglyTypedIdSerializationSystemTextJsonUnitTests001))]
  public partial class CollectionExtensionSerializationSystemTextJsonUnitTests001 : IClassFixture<SerializationFixtureSystemTextJson> {

    [Theory]
    [MemberData(nameof(CollectionExtensionSerializationTestDataGenerator<HierDataClass>.CollectionExtensionSerializationTestData), MemberType = typeof(CollectionExtensionSerializationTestDataGenerator<HierDataClass>))]
    public void HierDataClassSerializeToJSON(CollectionExtensionSerializationTestData<HierDataClass> inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
      // SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
      JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
    }

    [Theory]
    [MemberData(nameof(CollectionExtensionSerializationTestDataGenerator<HierDataClass>.CollectionExtensionSerializationTestData), MemberType = typeof(CollectionExtensionSerializationTestDataGenerator<HierDataClass>))]
    public void HierDataClassDeserializeFromJSON(CollectionExtensionSerializationTestData<HierDataClass> inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
      if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
        Action act = () => JsonSerializer.Deserialize<HierDataClass>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        act.Should().Throw<System.Text.Json.JsonException>()
          .WithMessage("The input does not contain any JSON tokens.*");
      }
      else {
        //SerializationFixtureSystemTextJson.Serializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
        var deserializedInstance = JsonSerializer.Deserialize<HierDataClass>(inTestData.SerializedTestData,
          SerializationFixture.JsonSerializerOptions);
        deserializedInstance.Should().BeEquivalentTo(inTestData.InstanceTestData);
      }
    }

    [Theory]
    [MemberData(nameof(CollectionExtensionSerializationTestDataGenerator<IHierDataClass>.CollectionExtensionSerializationTestData), MemberType = typeof(CollectionExtensionSerializationTestDataGenerator<IHierDataClass>))]
    public void IHierDataClassSerializeToJSON(CollectionExtensionSerializationTestData<IHierDataClass> inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
      // SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
      JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
    }

    [Theory]
    [MemberData(nameof(CollectionExtensionSerializationTestDataGenerator<IHierDataClass>.CollectionExtensionSerializationTestData), MemberType = typeof(CollectionExtensionSerializationTestDataGenerator<IHierDataClass>))]
    public void IHierDataClassDeserializeFromJSON(CollectionExtensionSerializationTestData<IHierDataClass> inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
      if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
        Action act = () => JsonSerializer.Deserialize<IHierDataClass>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        act.Should().Throw<System.Text.Json.JsonException>()
          .WithMessage("The input does not contain any JSON tokens.*");
      }
      else {
        //SerializationFixtureSystemTextJson.Serializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
        var deserializedInstance = JsonSerializer.Deserialize<IHierDataClass>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        deserializedInstance.Should().BeEquivalentTo(inTestData.InstanceTestData);
      }
    }
  }
}
