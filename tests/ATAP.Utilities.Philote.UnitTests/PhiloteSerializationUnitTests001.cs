
using System;
using System.Collections;
using System.Collections.Generic;
using ATAP.Utilities.StronglyTypedIds;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using System.Text.RegularExpressions;

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
    [MemberData(nameof(TestClassWithIntPhiloteSerializationTestDataGenerator.TestData), MemberType = typeof(TestClassWithIntPhiloteSerializationTestDataGenerator))]
    public void TestClassWithIntPhiloteSerializeToJson(TestClassWithIntPhiloteSerializationTestData inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
      if (Regex.Match(inTestData.SerializedTestData, "\"ID\":(-2147483648|-1|0|1|2147483647),").Success) {
        //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
      }
      else {
        //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the TestClassWithIntPhilote ");
      }
    }

    [Theory]
    [MemberData(nameof(TestClassWithIntPhiloteSerializationTestDataGenerator.TestData), MemberType = typeof(TestClassWithIntPhiloteSerializationTestDataGenerator))]
    public void TestClassWithIntPhiloteDeserializeFromJson(TestClassWithIntPhiloteSerializationTestData inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
      if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
        Action act = () => JsonSerializer.Deserialize<TestClassWithPhilote<int>>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        act.Should().Throw<System.Text.Json.JsonException>()
          .WithMessage("The input does not contain any JSON tokens.*");
      }
      else if (Regex.Match(inTestData.SerializedTestData, "\"ID\":(-2147483648|-1|0|1|2147483647)").Success) {
        //SerializationFixtureSystemTextJson.Serializer.Deserialize<IntStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
        var deserialized = JsonSerializer.Deserialize<TestClassWithPhilote<int>>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        deserialized.Should().BeEquivalentTo(inTestData.InstanceTestData);
      }
      else {
        // ToDo: validate that strings that don't match an int throw an exception
      }
    }

    [Theory]
    [MemberData(nameof(TestClassWithGuidPhiloteSerializationTestDataGenerator.TestData), MemberType = typeof(TestClassWithGuidPhiloteSerializationTestDataGenerator))]
    public void TestClassWithGuidPhiloteSerializeToJson(TestClassWithGuidPhiloteSerializationTestData inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
      if (Regex.Match(inTestData.SerializedTestData, "\"ID\":(0000|0123)").Success) {
        //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
      }
      else {
        //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the TestClassWithGuidPhilote ");
      }
    }

    [Theory]
    [MemberData(nameof(TestClassWithGuidPhiloteSerializationTestDataGenerator.TestData), MemberType = typeof(TestClassWithGuidPhiloteSerializationTestDataGenerator))]
    public void TestClassWithGuidPhiloteDeserializeFromJson(TestClassWithGuidPhiloteSerializationTestData inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
      if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
        Action act = () => JsonSerializer.Deserialize<TestClassWithPhilote<Guid>>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        act.Should().Throw<System.Text.Json.JsonException>()
          .WithMessage("The input does not contain any JSON tokens.*");
      }
      else if (Regex.Match(inTestData.SerializedTestData, "\"ID\":(0000|0123)").Success) {
        //SerializationFixtureSystemTextJson.Serializer.Deserialize<TestClassWithGuidPhilote>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
        var deserialized = JsonSerializer.Deserialize<TestClassWithPhilote<Guid>>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        deserialized.Should().BeEquivalentTo(inTestData.InstanceTestData);
      }
      else {
        // ToDo: validate that strings that don't match an int throw an exception
      }
    }

    // [Theory]
    // [MemberData(nameof(TestRecordWithIntPhiloteSerializationTestDataGenerator.TestData), MemberType = typeof(TestRecordWithIntPhiloteSerializationTestDataGenerator))]
    // public void TestRecordWithIntPhiloteSerializeToJson(TestRecordWithIntPhiloteSerializationTestData inTestData) {
    //   // ToDo low priority localize the unit test's exception's message
    //   if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
    //   if (Regex.Match(inTestData.SerializedTestData, "\"ID\":(-2147483648|-1|0|1|2147483647),").Success) {
    //     //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
    //   }
    //   else {
    //     //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the TestRecordWithIntPhilote ");
    //   }
    // }

    // [Theory]
    // [MemberData(nameof(TestRecordWithIntPhiloteSerializationTestDataGenerator.TestData), MemberType = typeof(TestRecordWithIntPhiloteSerializationTestDataGenerator))]
    // public void TestRecordWithIntPhiloteDeserializeFromJson(TestRecordWithIntPhiloteSerializationTestData inTestData) {
    //   // ToDo low priority localize the unit test's exception's message
    //   if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
    //   if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
    //     Action act = () => JsonSerializer.Deserialize<TestRecordWithIntPhilote>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     act.Should().Throw<System.Text.Json.JsonException>()
    //       .WithMessage("The input does not contain any JSON tokens.*");
    //   }
    //   else if (Regex.Match(inTestData.SerializedTestData, "\"ID\":(-2147483648|-1|0|1|2147483647)").Success) {
    //     //SerializationFixtureSystemTextJson.Serializer.Deserialize<TestRecordWithIntPhilote>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
    //     var deserialized = JsonSerializer.Deserialize<TestRecordWithIntPhilote>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     deserialized.Should().BeEquivalentTo(inTestData.InstanceTestData);
    //   }
    //   else {
    //     // ToDo: validate that strings that don't match an int throw an exception
    //   }
    // }

    // [Theory]
    // [MemberData(nameof(TestRecordWithGuidPhiloteSerializationTestDataGenerator.TestData), MemberType = typeof(TestRecordWithGuidPhiloteSerializationTestDataGenerator))]
    // public void TestRecordWithGuidPhiloteSerializeToJson(TestRecordWithGuidPhiloteSerializationTestData inTestData) {
    //   // ToDo low priority localize the unit test's exception's message
    //   if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
    //   if (Regex.Match(inTestData.SerializedTestData, "\"ID\":(0000|0123)").Success) {
    //     //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
    //   }
    //   else {
    //     //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the TestRecordWithGuidPhilote ");
    //   }
    // }

    // [Theory]
    // [MemberData(nameof(TestRecordWithGuidPhiloteSerializationTestDataGenerator.TestData), MemberType = typeof(TestRecordWithGuidPhiloteSerializationTestDataGenerator))]
    // public void TestRecordWithGuidPhiloteDeserializeFromJson(TestRecordWithGuidPhiloteSerializationTestData inTestData) {
    //   // ToDo low priority localize the unit test's exception's message
    //   if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
    //   if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
    //     Action act = () => JsonSerializer.Deserialize<TestRecordWithGuidPhilote>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     act.Should().Throw<System.Text.Json.JsonException>()
    //       .WithMessage("The input does not contain any JSON tokens.*");
    //   }
    //   else if (Regex.Match(inTestData.SerializedTestData, "\"ID\":(0000|0123)").Success) {
    //     //SerializationFixtureSystemTextJson.Serializer.Deserialize<TestRecordWithGuidPhilote>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
    //     var deserialized = JsonSerializer.Deserialize<TestRecordWithGuidPhilote>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     deserialized.Should().BeEquivalentTo(inTestData.InstanceTestData);
    //   }
    //   else {
    //     // ToDo: validate that strings that don't match an int throw an exception
    //   }
    // }

    // [Theory]
    // [MemberData(nameof(TestClassWithIntIPhiloteSerializationTestDataGenerator.TestData), MemberType = typeof(TestClassWithIntIPhiloteSerializationTestDataGenerator))]
    // public void TestClassWithIntIPhiloteSerializeToJson(TestClassWithIntIPhiloteSerializationTestData inTestData) {
    //   // ToDo low priority localize the unit test's exception's message
    //   if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
    //   if (Regex.Match(inTestData.SerializedTestData, "\"ID\":(-2147483648|-1|0|1|2147483647),").Success ) {
    //     //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
    //   }
    //   else {
    //     //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the TestClassWithIntIPhilote ");
    //   }
    // }

    // [Theory]
    // [MemberData(nameof(TestClassGuidPhiloteInterfaceSerializationTestDataGenerator.TestData), MemberType = typeof(TestClassGuidPhiloteInterfaceSerializationTestDataGenerator))]
    // public void GuidPhiloteInterfaceSerializeToJson(TestClassGuidPhiloteInterfaceSerializationTestData inTestData) {
    //   // ToDo low priority localize the unit test's exception's message
    //   if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
    //   // new AbstractStronglyTypedId<int>() have random Values, two sets of test data have fixed, non-random integers, the rest are random
    //   if (inTestData.SerializedTestData.StartsWith("{\"ID\":\"0000", System.StringComparison.InvariantCulture) || inTestData.SerializedTestData.StartsWith("{\"ID\":\"01234", System.StringComparison.InvariantCulture)) {
    //     //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
    //   }
    //   else {
    //     //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the Philote ");
    //   }
    // }

    // [Theory]
    // [MemberData(nameof(TestClassGuidPhiloteInterfaceSerializationTestDataGenerator.TestData),
    //   MemberType = typeof(TestClassGuidPhiloteInterfaceSerializationTestDataGenerator))]
    // public void GuidPhiloteInterfaceDeserializeFromJson(TestClassGuidPhiloteInterfaceSerializationTestData inTestData) {
    //   // ToDo low priority localize the unit test's exception's message
    //   if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
    // if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
    //     Action act = () => JsonSerializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     act.Should().Throw<System.Text.Json.JsonException>()
    //       .WithMessage("The input does not contain any JSON tokens.*");
    //   }
    //   else if (inTestData.SerializedTestData.StartsWith("{\"ID\":\"0000", System.StringComparison.InvariantCulture) || inTestData.SerializedTestData.StartsWith("{\"ID\":\"01234", System.StringComparison.InvariantCulture)) {
    //     //SerializationFixtureSystemTextJson.Serializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
    //     var philote = JsonSerializer.Deserialize<TestClassGuidPhilote>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     philote.Should().BeEquivalentTo(inTestData.InstanceTestData);
    //   }
    //   else {
    //     // ToDo: validate that strings that don't match a Guid throw an exception
    //   }
    // }

    // [Theory]
    // [MemberData(nameof(TestClassIntPhiloteInterfaceSerializationTestDataGenerator.TestData), MemberType = typeof(TestClassIntPhiloteInterfaceSerializationTestDataGenerator))]
    // public void IntPhiloteInterfaceSerializeToJson(TestClassIntPhiloteInterfaceSerializationTestData inTestData) {
    //   // ToDo low priority localize the unit test's exception's message
    //   if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
    //   // new AbstractStronglyTypedId<int>() have random Values, two sets of test data have fixed, non-random integers, the rest are random
    //   if (inTestData.SerializedTestData.StartsWith("1234", System.StringComparison.InvariantCulture) || inTestData.SerializedTestData.Equals("0")) {
    //     //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
    //   }
    //   else {
    //     //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the Philote ");
    //   }
    // }

    // [Theory]
    // [MemberData(nameof(TestClassIntPhiloteInterfaceSerializationTestDataGenerator.TestData), MemberType = typeof(TestClassIntPhiloteInterfaceSerializationTestDataGenerator))]
    // public void IntPhiloteInterfaceDeserializeFromJson(TestClassIntPhiloteInterfaceSerializationTestData inTestData) {
    //   // ToDo low priority localize the unit test's exception's message
    //   if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
    //   if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
    //     Action act = () => JsonSerializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     act.Should().Throw<System.Text.Json.JsonException>()
    //       .WithMessage("The input does not contain any JSON tokens.*");
    //   }
    //   else if (inTestData.SerializedTestData.StartsWith("{\"ID\":0,", System.StringComparison.InvariantCulture) || inTestData.SerializedTestData.StartsWith("{\"ID\":0,", System.StringComparison.InvariantCulture)) {
    //     //SerializationFixtureSystemTextJson.Serializer.Deserialize<IntStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
    //     var philote = JsonSerializer.Deserialize<TestClassIntPhilote>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     philote.Should().BeEquivalentTo(inTestData.InstanceTestData);
    //   }
    //   else {
    //     // ToDo: validate that strings that don't match an int throw an exception
    //   }
    // }

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
    // [MemberData(nameof(IntTestDataGenerator.TestData), MemberType = typeof(IntTestDataGenerator))]
    // public void IntIdSerializeToJSON(IntTestData inTestData) {
    //   // new AbstractStronglyTypedId<int>() have random Values, two sets of test data have fixed, non-random integers, the rest are random
    //   if (inTestData.SerializedStronglyTypedId.StartsWith("1234", System.StringComparison.InvariantCulture) || inTestData.SerializedStronglyTypedId.Equals("0")) {
    //     //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
    //     // var x = JsonSerializer.Serialize(inTestData.AbstractStronglyTypedId, SerializationFixtureSystemTextJson.JsonSerializerSettings);
    //     // var y = inTestData.SerializedTestData;
    //     // x.Should().Be(y);
    //   }
    //   else {
    //     //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the AbstractStronglyTypedId ");
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
    //     philote.Should().BeEquivalentTo(inTestData.InstanceTestData);
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
    //     //SerializationFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().MatchRegex("^\"[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}\"$");
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
    //   else if (inTestData.AbstractStronglyTypedId.ToString().StartsWith("\"0000", System.StringComparison.InvariantCulture) || inTestData.InstanceTestData.ToString().StartsWith("\"01234", System.StringComparison.InvariantCulture)) {
    //     //SerializationFixtureSystemTextJson.Serializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.AbstractStronglyTypedId);
    //     var philote = JsonSerializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     philote.Should().BeEquivalentTo(inTestData.InstanceTestData);
    //     // GUIDS are random, two sets of test data have fixed, non-random guids, the rest are random
    //   }
    //   else {
    //     // ToDo: validate that strings that don't match a Guid throw an exception
    //   }
    // }
  }
}
