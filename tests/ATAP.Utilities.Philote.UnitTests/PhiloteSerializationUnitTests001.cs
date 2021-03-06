
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

    // [Theory]
    // [MemberData(nameof(GuidPhiloteSerializationTestDataGenerator.StronglyTypedIdSerializationTestData), MemberType = typeof(GuidPhiloteSerializationTestDataGenerator))]
    // public void GuidPhiloteDeserializeFromJSON(GuidStronglyTypedIdSerializationTestData inTestData) {
    //   if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
    //     Action act = () => JsonSerializer.Deserialize<GuidPhilote>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     act.Should().Throw<System.Text.Json.JsonException>()
    //       .WithMessage("The input does not contain any JSON tokens.*");
    //   }
    //   else if (inTestData.SerializedTestData.StartsWith("\"0000", System.StringComparison.InvariantCulture) || inTestData.SerializedTestData.StartsWith("\"01234", System.StringComparison.InvariantCulture)) {
    //     //SerializationFixtureSystemTextJson.Serializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
    //     var philote = JsonSerializer.Deserialize<GuidPhilote>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     philote.Should().BeEquivalentTo(inTestData.InstanceTestData);
    //   }
    //   else {
    //     // ToDo: validate that strings that don't match a Guid throw an exception
    //   }
    // }


    // [Theory]
    // [MemberData(nameof(GuidPhiloteSerializationTestDataGenerator.PhiloteSerializationTestData), MemberType = typeof(GuidPhiloteSerializationTestDataGenerator))]
    // public void GuidIdSerializeToJSON(GuidPhiloteSerializationTestData inPhiloteTestData) {
    //   // GUIDS are random, two sets of test data have fixed, non-random guids, the rest are random
    //   if (inTestData.SerializedTestData.StartsWith("\"0000", System.StringComparison.InvariantCulture) || inTestData.SerializedTestData.StartsWith("\"01234", System.StringComparison.InvariantCulture)) {
    //     // SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
    //     // JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixtureSystemTextJson.JsonSerializerSettings).Should().Be(inTestData.SerializedTestData);
    //   }
    //   else {
    //     //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.StronglyTypedId).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().MatchRegex("^\"[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}\"$");
    //   }
    // }

    // [Theory]
    // [MemberData(nameof(IntTestDataGenerator.TestData), MemberType = typeof(IntTestDataGenerator))]
    // public void IntIdSerializeToJSON(IntTestData inTestData) {
    //   // new StronglyTypedID<int>() have random Values, two sets of test data have fixed, non-random integers, the rest are random
    //   if (inTestData.SerializedStronglyTypedId.StartsWith("1234", System.StringComparison.InvariantCulture) || inTestData.SerializedStronglyTypedId.Equals("0")) {
    //     //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.StronglyTypedId).Should().Be(inTestData.SerializedTestData);
    //     JsonSerializer.Serialize(inTestData.StronglyTypedId, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
    //     // var x = JsonSerializer.Serialize(inTestData.StronglyTypedId, SerializationFixtureSystemTextJson.JsonSerializerSettings);
    //     // var y = inTestData.SerializedTestData;
    //     // x.Should().Be(y);
    //   }
    //   else {
    //     //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.StronglyTypedId).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
    //     JsonSerializer.Serialize(inTestData.StronglyTypedId, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the StronglyTypedId ");
    //   }
    // }

    // [Theory]
    // [MemberData(nameof(IntTestDataGenerator.TestData), MemberType = typeof(IntTestDataGenerator))]
    // public void IntIdDeserializeFromJSON(IntTestData inTestData) {
    //   if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
    //     // ToDo: Verify that deserialization of empty null or whitespace raises an exception?
    //     var t = true;
    //     t.Should().BeTrue();
    //   }
    //   else {
    //     // ToDo: validate that non-integer strings throw an exception
    //     var philote = JsonSerializer.Deserialize<IntStronglyTypedId>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     philote.Should().BeEquivalentTo(inTestData.StronglyTypedId);
    //   }
    // }

    // [Theory]
    // [MemberData(nameof(GuidTestDataGenerator.TestData), MemberType = typeof(GuidTestDataGenerator))]
    // public void GuidIdDeserializeFromJSON(GuidTestData inTestData) {
    //   if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
    //     // ToDo: Verify that deserialization of empty null or whitespace raises an exception?
    //     var t = true;
    //     t.Should().BeTrue();
    //   }
    //   else if (inTestData.StronglyTypedId.ToString().StartsWith("\"0000", System.StringComparison.InvariantCulture) || inTestData.StronglyTypedId.ToString().StartsWith("\"01234", System.StringComparison.InvariantCulture)) {
    //     //SerializationFixtureSystemTextJson.Serializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.StronglyTypedId);
    //     var philote = JsonSerializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     philote.Should().BeEquivalentTo(inTestData.StronglyTypedId);
    //     // GUIDS are random, two sets of test data have fixed, non-random guids, the rest are random
    //   }
    //   else {
    //     // ToDo: validate that strings that don't match a Guid throw an exception
    //   }
    // }

    [Theory]
    [MemberData(nameof(TestClassGuidPhiloteInterfaceSerializationTestDataGenerator.TestData), MemberType = typeof(TestClassGuidPhiloteInterfaceSerializationTestDataGenerator))]
    public void GuidPhiloteInterfaceSerializeToJson(TestClassGuidPhiloteInterfaceSerializationTestData inTestData) {
      // new StronglyTypedID<int>() have random Values, two sets of test data have fixed, non-random integers, the rest are random
      if (inTestData.SerializedTestData.StartsWith("{\"ID\":\"0000", System.StringComparison.InvariantCulture) || inTestData.SerializedTestData.StartsWith("{\"ID\":\"01234", System.StringComparison.InvariantCulture)) {
        //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.StronglyTypedId).Should().Be(inTestData.SerializedTestData);
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
      }
      else {
        //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.StronglyTypedId).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the Philote ");
      }
    }

    [Theory]
    [MemberData(nameof(TestClassGuidPhiloteInterfaceSerializationTestDataGenerator.TestData), MemberType = typeof(TestClassGuidPhiloteInterfaceSerializationTestDataGenerator))]
    public void GuidPhiloteInterfaceDeserializeFromJson(TestClassGuidPhiloteInterfaceSerializationTestData inTestData) {
      if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
        Action act = () => JsonSerializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        act.Should().Throw<System.Text.Json.JsonException>()
          .WithMessage("The input does not contain any JSON tokens.*");
      }
      else if (inTestData.SerializedTestData.StartsWith("{\"ID\":\"0000", System.StringComparison.InvariantCulture) || inTestData.SerializedTestData.StartsWith("{\"ID\":\"01234", System.StringComparison.InvariantCulture)) {
        //SerializationFixtureSystemTextJson.Serializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.StronglyTypedId);
        var philote = JsonSerializer.Deserialize<TestClassGuidPhilote>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        philote.Should().BeEquivalentTo(inTestData.InstanceTestData);
      }
      else {
        // ToDo: validate that strings that don't match a Guid throw an exception
      }
    }

    [Theory]
    [MemberData(nameof(TestClassIntPhiloteInterfaceSerializationTestDataGenerator.TestData), MemberType = typeof(TestClassIntPhiloteInterfaceSerializationTestDataGenerator))]
    public void IntPhiloteInterfaceSerializeToJSON(TestClassIntPhiloteInterfaceSerializationTestData inTestData) {
      // new StronglyTypedID<int>() have random Values, two sets of test data have fixed, non-random integers, the rest are random
      if (inTestData.SerializedTestData.StartsWith("1234", System.StringComparison.InvariantCulture) || inTestData.SerializedTestData.Equals("0")) {
        //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
      }
      else {
        //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the Philote ");
      }
    }


    [Theory]
    [MemberData(nameof(TestClassIntPhiloteInterfaceSerializationTestDataGenerator.TestData), MemberType = typeof(TestClassIntPhiloteInterfaceSerializationTestDataGenerator))]
    public void IntPhiloteInterfaceDeserializeFromJSON(TestClassIntPhiloteInterfaceSerializationTestData inTestData) {
      if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
        Action act = () => JsonSerializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        act.Should().Throw<System.Text.Json.JsonException>()
          .WithMessage("The input does not contain any JSON tokens.*");
      }
      else if (inTestData.SerializedTestData.StartsWith("{\"ID\":0,", System.StringComparison.InvariantCulture) || inTestData.SerializedTestData.StartsWith("{\"ID\":0,", System.StringComparison.InvariantCulture)) {
        //SerializationFixtureSystemTextJson.Serializer.Deserialize<IntStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
        var philote = JsonSerializer.Deserialize<TestClassIntPhilote>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        philote.Should().BeEquivalentTo(inTestData.InstanceTestData);
      }
      else {
        // ToDo: validate that strings that don't match an int throw an exception
      }
    }
    // [Theory]
    // [MemberData(nameof(StronglyTypedIdInterfaceSerializationTestDataGenerator<int>.TestData), MemberType = typeof(StronglyTypedIdInterfaceSerializationTestDataGenerator<int>))]
    // public void StronglyTypedIdInterfaceIntDeserializeFromJSON(StronglyTypedIdInterfaceSerializationTestData<int> inStronglyTypedIdTestData) {
    // if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
    //   Action act = () => JsonSerializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //   act.Should().Throw<System.Text.Json.JsonException>()
    //     .WithMessage("The input does not contain any JSON tokens.*");
    // } else {
    //     //SerializationFixtureSystemTextJson.Serializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.StronglyTypedId);
    //     var stronglyTypedId = JsonSerializer.Deserialize<IntStronglyTypedId>(inStronglyTypedIdTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     stronglyTypedId.Should().Equals(inStronglyTypedIdTestData.StronglyTypedId);
    //     // ToDo: validate that strings that don't match an int throw an exception
    //   }
    // }
  }
}
