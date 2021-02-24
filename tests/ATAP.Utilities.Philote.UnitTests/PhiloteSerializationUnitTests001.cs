
using System;
using System.Collections;
using System.Collections.Generic;
using ATAP.Utilities.StronglyTypedID;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

using System.ComponentModel;

// For the tests that use the new Serializer/Deserializer
using System.Text.Json;
// For the tests that use the old Newtonsoft Serializer/Deserializer
//using Newtonsoft.Json;

namespace ATAP.Utilities.Philote.UnitTests {
  // Attribution: https://github.com/xunit/xunit/issues/2007, however, we only need a class fixture not a collectionfixtire, so, commentedout below
  //  [CollectionDefinition(nameof(PhiloteSerializationSystemTextJsonUnitTests001), DisableParallelization = true)]
  //  [Collection(nameof(PhiloteSerializationSystemTextJsonUnitTests001))]
  public partial class PhiloteSerializationSystemTextJsonUnitTests001 : IClassFixture<SerializationFixtureSystemTextJson> {

    [Theory]
    [MemberData(nameof(GuidPhiloteSerializationTestDataGenerator.PhiloteSerializationTestData), MemberType = typeof(GuidPhiloteSerializationTestDataGenerator))]
    public void GuidIdSerializeToJSON(GuidPhiloteSerializationTestData inPhiloteTestData) {
      // GUIDS are random, two sets of test data have fixed, non-random guids, the rest are random
      if (inStronglyTypedIdTestData.SerializedStronglyTypedId.StartsWith("\"0000", System.StringComparison.InvariantCulture) || inStronglyTypedIdTestData.SerializedStronglyTypedId.StartsWith("\"01234", System.StringComparison.InvariantCulture)) {
        // SerializationFixtureSystemTextJson.Serializer.Serialize(inStronglyTypedIdTestData.StronglyTypedId).Should().Be(inStronglyTypedIdTestData.SerializedStronglyTypedId);
        // JsonSerializer.Serialize(inStronglyTypedIdTestData.StronglyTypedId, SerializationFixtureSystemTextJson.JsonSerializerSettings).Should().Be(inStronglyTypedIdTestData.SerializedStronglyTypedId);

        var x = JsonSerializer.Serialize(inStronglyTypedIdTestData.StronglyTypedId, SerializationFixture.JsonSerializerOptions);
        var y = inStronglyTypedIdTestData.SerializedStronglyTypedId;
        x.Should().Be(y);
      }
      else {
        //SerializationFixtureSystemTextJson.Serializer.Serialize(inStronglyTypedIdTestData.StronglyTypedId).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
        JsonSerializer.Serialize(inStronglyTypedIdTestData.StronglyTypedId, SerializationFixture.JsonSerializerOptions).Should().MatchRegex("^\"[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}\"$");
      }
    }

    [Theory]
    [MemberData(nameof(IntStronglyTypedIdSerializationTestDataGenerator.StronglyTypedIdSerializationTestData), MemberType = typeof(IntStronglyTypedIdSerializationTestDataGenerator))]
    public void IntIdSerializeToJSON(IntStronglyTypedIdSerializationTestData inStronglyTypedIdTestData) {
      // new StronglyTypedID<int>() have random Values, two sets of test data have fixed, non-random integers, the rest are random
      if (inStronglyTypedIdTestData.SerializedStronglyTypedId.StartsWith("1234", System.StringComparison.InvariantCulture) || inStronglyTypedIdTestData.SerializedStronglyTypedId.Equals("0")) {
        //SerializationFixtureSystemTextJson.Serializer.Serialize(inStronglyTypedIdTestData.StronglyTypedId).Should().Be(inStronglyTypedIdTestData.SerializedStronglyTypedId);
        JsonSerializer.Serialize(inStronglyTypedIdTestData.StronglyTypedId, SerializationFixture.JsonSerializerOptions).Should().Be(inStronglyTypedIdTestData.SerializedStronglyTypedId);
        // var x = JsonSerializer.Serialize(inStronglyTypedIdTestData.StronglyTypedId, SerializationFixtureSystemTextJson.JsonSerializerSettings);
        // var y = inStronglyTypedIdTestData.SerializedStronglyTypedId;
        // x.Should().Be(y);
      }
      else {
        //SerializationFixtureSystemTextJson.Serializer.Serialize(inStronglyTypedIdTestData.StronglyTypedId).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
        JsonSerializer.Serialize(inStronglyTypedIdTestData.StronglyTypedId, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the StronglyTypedId ");
      }
    }

    [Theory]
    [MemberData(nameof(IntStronglyTypedIdSerializationTestDataGenerator.StronglyTypedIdSerializationTestData), MemberType = typeof(IntStronglyTypedIdSerializationTestDataGenerator))]
    public void IntIdDeserializeFromJSON(IntStronglyTypedIdSerializationTestData inStronglyTypedIdTestData) {
      if (String.IsNullOrEmpty(inStronglyTypedIdTestData.SerializedStronglyTypedId)) {
        // ToDo: Verify that deserialization of empty null or whitespace raises an exception?
        var t = true;
        t.Should().BeTrue();
      }
      else {
        // ToDo: validate that non-integer strings throw an exception
        var stronglyTypedId = JsonSerializer.Deserialize<IntStronglyTypedId>(inStronglyTypedIdTestData.SerializedStronglyTypedId, SerializationFixture.JsonSerializerOptions);
        stronglyTypedId.Should().BeEquivalentTo(inStronglyTypedIdTestData.StronglyTypedId);
      }
    }

    [Theory]
    [MemberData(nameof(GuidStronglyTypedIdSerializationTestDataGenerator.StronglyTypedIdSerializationTestData), MemberType = typeof(GuidStronglyTypedIdSerializationTestDataGenerator))]
    public void GuidIdDeserializeFromJSON(GuidStronglyTypedIdSerializationTestData inStronglyTypedIdTestData) {
      if (String.IsNullOrEmpty(inStronglyTypedIdTestData.SerializedStronglyTypedId)) {
        // ToDo: Verify that deserialization of empty null or whitespace raises an exception?
        var t = true;
        t.Should().BeTrue();
      }
      else if (inStronglyTypedIdTestData.StronglyTypedId.ToString().StartsWith("\"0000", System.StringComparison.InvariantCulture) || inStronglyTypedIdTestData.StronglyTypedId.ToString().StartsWith("\"01234", System.StringComparison.InvariantCulture)) {
        //SerializationFixtureSystemTextJson.Serializer.Deserialize<GuidStronglyTypedId>(inStronglyTypedIdTestData.SerializedStronglyTypedId).Should().BeEquivalentTo(inStronglyTypedIdTestData.StronglyTypedId);
        var stronglyTypedId = JsonSerializer.Deserialize<GuidStronglyTypedId>(inStronglyTypedIdTestData.SerializedStronglyTypedId, SerializationFixture.JsonSerializerOptions);
        stronglyTypedId.Should().BeEquivalentTo(inStronglyTypedIdTestData.StronglyTypedId);
        // GUIDS are random, two sets of test data have fixed, non-random guids, the rest are random
      }
      else {
        // ToDo: validate that strings that don't match a Guid throw an exception
      }
    }

    [Theory]
    [MemberData(nameof(StronglyTypedIdInterfaceSerializationTestDataGenerator<Guid>.StronglyTypedIdSerializationTestData), MemberType = typeof(StronglyTypedIdInterfaceSerializationTestDataGenerator<Guid>))]
    public void StronglyTypedIdInterfaceGuidDeserializeFromJSON(StronglyTypedIdInterfaceSerializationTestData<Guid> inStronglyTypedIdTestData) {
      if (String.IsNullOrEmpty(inStronglyTypedIdTestData.SerializedStronglyTypedId)) {
        // ToDo: Verify that deserialization of empty null or whitespace raises an exception?
        var t = true;
        t.Should().BeTrue();
      }
      else if (inStronglyTypedIdTestData.StronglyTypedId.ToString().StartsWith("\"0000", System.StringComparison.InvariantCulture) || inStronglyTypedIdTestData.StronglyTypedId.ToString().StartsWith("\"01234", System.StringComparison.InvariantCulture)) {
        //SerializationFixtureSystemTextJson.Serializer.Deserialize<GuidStronglyTypedId>(inStronglyTypedIdTestData.SerializedStronglyTypedId).Should().BeEquivalentTo(inStronglyTypedIdTestData.StronglyTypedId);
        var stronglyTypedId = JsonSerializer.Deserialize<GuidStronglyTypedId>(inStronglyTypedIdTestData.SerializedStronglyTypedId, SerializationFixture.JsonSerializerOptions);
        stronglyTypedId.Should().BeEquivalentTo(inStronglyTypedIdTestData.StronglyTypedId);
      }
      else {
        // ToDo: validate that strings that don't match a Guid throw an exception
      }
    }

    [Theory]
    [MemberData(nameof(StronglyTypedIdInterfaceSerializationTestDataGenerator<int>.StronglyTypedIdSerializationTestData), MemberType = typeof(StronglyTypedIdInterfaceSerializationTestDataGenerator<int>))]
    public void StronglyTypedIdInterfaceIntDeserializeFromJSON(StronglyTypedIdInterfaceSerializationTestData<int> inStronglyTypedIdTestData) {
      if (String.IsNullOrEmpty(inStronglyTypedIdTestData.SerializedStronglyTypedId)) {
        // ToDo: Verify that deserialization of empty null or whitespace raises an exception?
        var t = true;
        t.Should().BeTrue();
      }
      else {
        //SerializationFixtureSystemTextJson.Serializer.Deserialize<GuidStronglyTypedId>(inStronglyTypedIdTestData.SerializedStronglyTypedId).Should().BeEquivalentTo(inStronglyTypedIdTestData.StronglyTypedId);
        var stronglyTypedId = JsonSerializer.Deserialize<IntStronglyTypedId>(inStronglyTypedIdTestData.SerializedStronglyTypedId, SerializationFixture.JsonSerializerOptions);
        stronglyTypedId.Should().Equals(inStronglyTypedIdTestData.StronglyTypedId);
        // ToDo: validate that strings that don't match an int throw an exception
      }
    }
  }
}
