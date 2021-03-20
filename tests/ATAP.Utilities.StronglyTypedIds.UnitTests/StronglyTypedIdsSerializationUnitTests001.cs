
using System;
using System.Collections;
using System.Collections.Generic;
using ATAP.Utilities.StronglyTypedIds;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

using System.ComponentModel;

// For the tests that use the new Serializer/Deserializer
using System.Text.Json;
// For the tests that use the old Newtonsoft Serializer/Deserializer
//using Newtonsoft.Json;

namespace ATAP.Utilities.StronglyTypedIds.UnitTests {
  // Attribution: https://github.com/xunit/xunit/issues/2007, however, we only need a class fixture not a collectionfixtire, so, commentedout below
  //  [CollectionDefinition(nameof(StronglyTypedIdSerializationSystemTextJsonUnitTests001), DisableParallelization = true)]
  //  [Collection(nameof(StronglyTypedIdSerializationSystemTextJsonUnitTests001))]
  public partial class StronglyTypedIdSerializationSystemTextJsonUnitTests001 : IClassFixture<SerializationFixtureSystemTextJson> {

    [Theory]
    [MemberData(nameof(GuidStronglyTypedIdSerializationTestDataGenerator.StronglyTypedIdSerializationTestData), MemberType = typeof(GuidStronglyTypedIdSerializationTestDataGenerator))]
    public void GuidIdSerializeToJSON(GuidStronglyTypedIdSerializationTestData inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
      // GUIDS are random, two sets of test data have fixed, non-random guids, the rest are random
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
    [MemberData(nameof(GuidStronglyTypedIdSerializationTestDataGenerator.StronglyTypedIdSerializationTestData), MemberType = typeof(GuidStronglyTypedIdSerializationTestDataGenerator))]
    public void GuidIdDeserializeFromJSON(GuidStronglyTypedIdSerializationTestData inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
      if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
        Action act = () => JsonSerializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        act.Should().Throw<System.Text.Json.JsonException>()
          .WithMessage("The input does not contain any JSON tokens.*");
      }
      else if (inTestData.SerializedTestData.StartsWith("\"0000", System.StringComparison.InvariantCulture) || inTestData.SerializedTestData.StartsWith("\"01234", System.StringComparison.InvariantCulture)) {
        //SerializationFixtureSystemTextJson.Serializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
        var stronglyTypedId = JsonSerializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        stronglyTypedId.Should().BeEquivalentTo(inTestData.InstanceTestData);
      }
      else {
        // ToDo: validate that strings that don't match a Guid throw an exception
      }
    }

    [Theory]
    [MemberData(nameof(StronglyTypedIdInterfaceSerializationTestDataGenerator<Guid>.StronglyTypedIdSerializationTestData), MemberType = typeof(StronglyTypedIdInterfaceSerializationTestDataGenerator<Guid>))]
    public void GuidStronglyTypedIdInterfaceSerializeToJSON(StronglyTypedIdInterfaceSerializationTestData<Guid> inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
      // GUIDS are random, two sets of test data have fixed, non-random guids, the rest are random
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
    [MemberData(nameof(StronglyTypedIdInterfaceSerializationTestDataGenerator<Guid>.StronglyTypedIdSerializationTestData), MemberType = typeof(StronglyTypedIdInterfaceSerializationTestDataGenerator<Guid>))]
    public void GuidStronglyTypedIdInterfaceDeserializeFromJSON(StronglyTypedIdInterfaceSerializationTestData<Guid> inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
      if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
        Action act = () => JsonSerializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        act.Should().Throw<System.Text.Json.JsonException>()
          .WithMessage("The input does not contain any JSON tokens.*");
      }
      else if (inTestData.SerializedTestData.StartsWith("\"0000", System.StringComparison.InvariantCulture) || inTestData.SerializedTestData.StartsWith("\"01234", System.StringComparison.InvariantCulture)) {
        //SerializationFixtureSystemTextJson.Serializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
        var stronglyTypedId = JsonSerializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        stronglyTypedId.Should().BeEquivalentTo(inTestData.InstanceTestData);
      }
      else {
        // ToDo: validate that strings that don't match a Guid throw an exception
      }
    }

    [Theory]
    [MemberData(nameof(IntStronglyTypedIdSerializationTestDataGenerator.StronglyTypedIdSerializationTestData), MemberType = typeof(IntStronglyTypedIdSerializationTestDataGenerator))]
    public void IntIdSerializeToJSON(IntStronglyTypedIdSerializationTestData inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
      // new StronglyTypedId<int>() have random Values, two sets of test data have fixed, non-random integers, the rest are random
      if (inTestData.SerializedTestData.Equals("-2147483648") || inTestData.SerializedTestData.Equals("-1") || inTestData.SerializedTestData.Equals("0") || inTestData.SerializedTestData.Equals("2147483647") || inTestData.SerializedTestData.Equals("1234567")) {
        //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
      }
      else {
        //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the InstanceTestData ");
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().MatchRegex("^-{0,1}\\d+$");
      }
    }

    [Theory]
    [MemberData(nameof(IntStronglyTypedIdSerializationTestDataGenerator.StronglyTypedIdSerializationTestData), MemberType = typeof(IntStronglyTypedIdSerializationTestDataGenerator))]
    public void IntIdDeserializeFromJSON(IntStronglyTypedIdSerializationTestData inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
      if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
        Action act = () => JsonSerializer.Deserialize<IntStronglyTypedId>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        act.Should().Throw<System.Text.Json.JsonException>()
          .WithMessage("The input does not contain any JSON tokens.*");
      }
      else {
        // ToDo: validate that non-integer strings throw an exception
        var stronglyTypedId = JsonSerializer.Deserialize<IntStronglyTypedId>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        stronglyTypedId.Should().BeEquivalentTo(inTestData.InstanceTestData);
      }
    }

    [Theory]
    [MemberData(nameof(StronglyTypedIdInterfaceSerializationTestDataGenerator<int>.StronglyTypedIdSerializationTestData), MemberType = typeof(StronglyTypedIdInterfaceSerializationTestDataGenerator<int>))]
    public void IntStronglyTypedIdInterfaceSerializeToJSON(StronglyTypedIdInterfaceSerializationTestData<int> inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
      if (inTestData.SerializedTestData.Equals("-2147483648") || inTestData.SerializedTestData.Equals("-1") || inTestData.SerializedTestData.Equals("0") || inTestData.SerializedTestData.Equals("2147483647") || inTestData.SerializedTestData.Equals("1234567")) {
        // SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
      }
      else {
        //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the InstanceTestData ");
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().MatchRegex("^-{0,1}\\d+$");
      }
    }

    [Theory]
    [MemberData(nameof(StronglyTypedIdInterfaceSerializationTestDataGenerator<int>.StronglyTypedIdSerializationTestData), MemberType = typeof(StronglyTypedIdInterfaceSerializationTestDataGenerator<int>))]
    public void IntStronglyTypedIdInterfaceDeserializeFromJSON(StronglyTypedIdInterfaceSerializationTestData<int> inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
      if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
        Action act = () => JsonSerializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        act.Should().Throw<System.Text.Json.JsonException>()
          .WithMessage("The input does not contain any JSON tokens.*");
      }
      else {
        //SerializationFixtureSystemTextJson.Serializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
        var stronglyTypedId = JsonSerializer.Deserialize<IntStronglyTypedId>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        stronglyTypedId.Should().Equals(inTestData.InstanceTestData);
        // ToDo: validate that strings that don't match an int throw an exception
      }
    }
  }
}
