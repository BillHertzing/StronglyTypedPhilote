
using System;
using System.Collections;
using System.Collections.Generic;
using ATAP.Utilities.Collection;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

using System.ComponentModel;
using ATAP.Utilities.Testing;
// For the tests that use the new Serializer/Deserializer
using System.Text.Json;
// For the tests that use the old Newtonsoft Serializer/Deserializer
//using Newtonsoft.Json;

namespace ATAP.Utilities.Collection.IntegrationTests {
  // Attribution: https://github.com/xunit/xunit/issues/2007, however, we only need a class fixture not a collectionfixtire, so, commentedout below
  //  [CollectionDefinition(nameof(StronglyTypedIdSerializationSystemTextJsonUnitTests001), DisableParallelization = true)]
  //  [Collection(nameof(StronglyTypedIdSerializationSystemTextJsonUnitTests001))]
  public partial class CollectionExtensionSerializationSystemTextJsonUnitTests001 : IClassFixture<SerializationSystemTextJsonFixture> {

    [Theory]
    [MemberData(nameof(CollectionExtensionTestDataGenerator<HierDataClass>.CollectionExtensionTestData), MemberType = typeof(CollectionExtensionTestDataGenerator<HierDataClass>))]
    public void HierDataClassSerializeToJSON(CollectionExtensionTestData<HierDataClass> inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException(nameof(inTestData)); }
      // SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
      JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
    }

    [Theory]
    [MemberData(nameof(CollectionExtensionTestDataGenerator<HierDataClass>.CollectionExtensionTestData), MemberType = typeof(CollectionExtensionTestDataGenerator<HierDataClass>))]
    public void HierDataClassDeserializeFromJSON(CollectionExtensionTestData<HierDataClass> inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException(nameof(inTestData)); }
      if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
        Action act = () => JsonSerializer.Deserialize<HierDataClass>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        act.Should().Throw<System.Text.Json.JsonException>()
          .WithMessage("The input does not contain any JSON tokens.*");
      }
      else {
        //SerializationSystemTextJsonFixture.Serializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
        var deserializedInstance = JsonSerializer.Deserialize<HierDataClass>(inTestData.SerializedTestData,
          SerializationFixture.JsonSerializerOptions);
        deserializedInstance.Should().BeEquivalentTo(inTestData.InstanceTestData);
      }
    }

    [Theory]
    [MemberData(nameof(CollectionExtensionTestDataGenerator<IHierDataClass>.CollectionExtensionTestData), MemberType = typeof(CollectionExtensionTestDataGenerator<IHierDataClass>))]
    public void IHierDataClassSerializeToJSON(CollectionExtensionTestData<IHierDataClass> inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException(nameof(inTestData)); }
      // SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
      JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
    }

    [Theory]
    [MemberData(nameof(CollectionExtensionTestDataGenerator<IHierDataClass>.CollectionExtensionTestData), MemberType = typeof(CollectionExtensionTestDataGenerator<IHierDataClass>))]
    public void IHierDataClassDeserializeFromJSON(CollectionExtensionTestData<IHierDataClass> inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException(nameof(inTestData)); }
      if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
        Action act = () => JsonSerializer.Deserialize<IHierDataClass>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        act.Should().Throw<System.Text.Json.JsonException>()
          .WithMessage("The input does not contain any JSON tokens.*");
      }
      else {
        //SerializationSystemTextJsonFixture.Serializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
        var deserializedInstance = JsonSerializer.Deserialize<IHierDataClass>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        deserializedInstance.Should().BeEquivalentTo(inTestData.InstanceTestData);
      }
    }
  }
}
