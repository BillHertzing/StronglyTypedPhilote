
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
  public partial class PhiloteSerializationSystemTextJsonUnitTests001 : IClassFixture<SerializationSystemTextJsonFixture> {

    [Theory]
    [MemberData(nameof(TestClassWithIntPhiloteTestDataGenerator.TestData), MemberType = typeof(TestClassWithIntPhiloteTestDataGenerator))]
    public void TestClassWithIntPhiloteSerializeToJson(TestClassWithIntPhiloteTestData inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
      if (Regex.Match(inTestData.SerializedTestData, "\"ID\":(-2147483648|-1|0|1|2147483647),").Success) {
        //SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
      }
      else {
        //SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the TestClassWithIntPhilote ");
      }
    }

    [Theory]
    [MemberData(nameof(TestClassWithIntPhiloteTestDataGenerator.TestData), MemberType = typeof(TestClassWithIntPhiloteTestDataGenerator))]
    public void TestClassWithIntPhiloteDeserializeFromJson(TestClassWithIntPhiloteTestData inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException(nameof(inTestData)); }
      if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
        Action act = () => JsonSerializer.Deserialize<TestClassWithPhilote<int>>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        act.Should().Throw<System.Text.Json.JsonException>()
          .WithMessage("The input does not contain any JSON tokens.*");
      }
      else if (Regex.Match(inTestData.SerializedTestData, "\"ID\":(-2147483648|-1|0|1|2147483647)").Success) {
        //SerializationSystemTextJsonFixture.Serializer.Deserialize<IntStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
        var deserialized = JsonSerializer.Deserialize<TestClassWithPhilote<int>>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        deserialized.Should().BeEquivalentTo(inTestData.InstanceTestData);
      }
      else {
        // ToDo: validate that strings that don't match an int throw an exception
      }
    }

    [Theory]
    [MemberData(nameof(TestClassWithGuidPhiloteTestDataGenerator.TestData), MemberType = typeof(TestClassWithGuidPhiloteTestDataGenerator))]
    public void TestClassWithGuidPhiloteSerializeToJson(TestClassWithGuidPhiloteTestData inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
      if (Regex.Match(inTestData.SerializedTestData, "\"ID\":(0000|0123)").Success) {
        //SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
      }
      else {
        //SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the TestClassWithGuidPhilote ");
      }
    }

    [Theory]
    [MemberData(nameof(TestClassWithGuidPhiloteTestDataGenerator.TestData), MemberType = typeof(TestClassWithGuidPhiloteTestDataGenerator))]
    public void TestClassWithGuidPhiloteDeserializeFromJson(TestClassWithGuidPhiloteTestData inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
      if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
        Action act = () => JsonSerializer.Deserialize<TestClassWithPhilote<Guid>>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        act.Should().Throw<System.Text.Json.JsonException>()
          .WithMessage("The input does not contain any JSON tokens.*");
      }
      else if (Regex.Match(inTestData.SerializedTestData, "\"ID\":(0000|0123)").Success) {
        //SerializationSystemTextJsonFixture.Serializer.Deserialize<TestClassWithGuidPhilote>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
        var deserialized = JsonSerializer.Deserialize<TestClassWithPhilote<Guid>>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
        deserialized.Should().BeEquivalentTo(inTestData.InstanceTestData);
      }
      else {
        // ToDo: validate that strings that don't match an int throw an exception
      }
    }

    // [Theory]
    // [MemberData(nameof(TestRecordWithIntPhiloteTestDataGenerator.TestData), MemberType = typeof(TestRecordWithIntPhiloteTestDataGenerator))]
    // public void TestRecordWithIntPhiloteSerializeToJson(TestRecordWithIntPhiloteTestData inTestData) {
    //   // ToDo low priority localize the unit test's exception's message
    //   if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
    //   if (Regex.Match(inTestData.SerializedTestData, "\"ID\":(-2147483648|-1|0|1|2147483647),").Success) {
    //     //SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
    //   }
    //   else {
    //     //SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the TestRecordWithIntPhilote ");
    //   }
    // }

    // [Theory]
    // [MemberData(nameof(TestRecordWithIntPhiloteTestDataGenerator.TestData), MemberType = typeof(TestRecordWithIntPhiloteTestDataGenerator))]
    // public void TestRecordWithIntPhiloteDeserializeFromJson(TestRecordWithIntPhiloteTestData inTestData) {
    //   // ToDo low priority localize the unit test's exception's message
    //   if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
    //   if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
    //     Action act = () => JsonSerializer.Deserialize<TestRecordWithIntPhilote>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     act.Should().Throw<System.Text.Json.JsonException>()
    //       .WithMessage("The input does not contain any JSON tokens.*");
    //   }
    //   else if (Regex.Match(inTestData.SerializedTestData, "\"ID\":(-2147483648|-1|0|1|2147483647)").Success) {
    //     //SerializationSystemTextJsonFixture.Serializer.Deserialize<TestRecordWithIntPhilote>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
    //     var deserialized = JsonSerializer.Deserialize<TestRecordWithIntPhilote>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     deserialized.Should().BeEquivalentTo(inTestData.InstanceTestData);
    //   }
    //   else {
    //     // ToDo: validate that strings that don't match an int throw an exception
    //   }
    // }

    // [Theory]
    // [MemberData(nameof(TestRecordWithGuidPhiloteTestDataGenerator.TestData), MemberType = typeof(TestRecordWithGuidPhiloteTestDataGenerator))]
    // public void TestRecordWithGuidPhiloteSerializeToJson(TestRecordWithGuidPhiloteTestData inTestData) {
    //   // ToDo low priority localize the unit test's exception's message
    //   if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
    //   if (Regex.Match(inTestData.SerializedTestData, "\"ID\":(0000|0123)").Success) {
    //     //SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
    //   }
    //   else {
    //     //SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the TestRecordWithGuidPhilote ");
    //   }
    // }

    // [Theory]
    // [MemberData(nameof(TestRecordWithGuidPhiloteTestDataGenerator.TestData), MemberType = typeof(TestRecordWithGuidPhiloteTestDataGenerator))]
    // public void TestRecordWithGuidPhiloteDeserializeFromJson(TestRecordWithGuidPhiloteTestData inTestData) {
    //   // ToDo low priority localize the unit test's exception's message
    //   if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
    //   if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
    //     Action act = () => JsonSerializer.Deserialize<TestRecordWithGuidPhilote>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     act.Should().Throw<System.Text.Json.JsonException>()
    //       .WithMessage("The input does not contain any JSON tokens.*");
    //   }
    //   else if (Regex.Match(inTestData.SerializedTestData, "\"ID\":(0000|0123)").Success) {
    //     //SerializationSystemTextJsonFixture.Serializer.Deserialize<TestRecordWithGuidPhilote>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
    //     var deserialized = JsonSerializer.Deserialize<TestRecordWithGuidPhilote>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     deserialized.Should().BeEquivalentTo(inTestData.InstanceTestData);
    //   }
    //   else {
    //     // ToDo: validate that strings that don't match an int throw an exception
    //   }
    // }

    // [Theory]
    // [MemberData(nameof(TestClassWithIntIPhiloteTestDataGenerator.TestData), MemberType = typeof(TestClassWithIntIPhiloteTestDataGenerator))]
    // public void TestClassWithIntIPhiloteSerializeToJson(TestClassWithIntIPhiloteTestData inTestData) {
    //   // ToDo low priority localize the unit test's exception's message
    //   if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
    //   if (Regex.Match(inTestData.SerializedTestData, "\"ID\":(-2147483648|-1|0|1|2147483647),").Success ) {
    //     //SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
    //   }
    //   else {
    //     //SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the TestClassWithIntIPhilote ");
    //   }
    // }

    // [Theory]
    // [MemberData(nameof(TestClassGuidPhiloteInterfaceTestDataGenerator.TestData), MemberType = typeof(TestClassGuidPhiloteInterfaceTestDataGenerator))]
    // public void GuidPhiloteInterfaceSerializeToJson(TestClassGuidPhiloteInterfaceTestData inTestData) {
    //   // ToDo low priority localize the unit test's exception's message
    //   if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
    //   // new AbstractStronglyTypedId<int>() have random Values, two sets of test data have fixed, non-random integers, the rest are random
    //   if (inTestData.SerializedTestData.StartsWith("{\"ID\":\"0000", System.StringComparison.InvariantCulture) || inTestData.SerializedTestData.StartsWith("{\"ID\":\"01234", System.StringComparison.InvariantCulture)) {
    //     //SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
    //   }
    //   else {
    //     //SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the Philote ");
    //   }
    // }

    // [Theory]
    // [MemberData(nameof(TestClassGuidPhiloteInterfaceTestDataGenerator.TestData),
    //   MemberType = typeof(TestClassGuidPhiloteInterfaceTestDataGenerator))]
    // public void GuidPhiloteInterfaceDeserializeFromJson(TestClassGuidPhiloteInterfaceTestData inTestData) {
    //   // ToDo low priority localize the unit test's exception's message
    //   if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
    // if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
    //     Action act = () => JsonSerializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     act.Should().Throw<System.Text.Json.JsonException>()
    //       .WithMessage("The input does not contain any JSON tokens.*");
    //   }
    //   else if (inTestData.SerializedTestData.StartsWith("{\"ID\":\"0000", System.StringComparison.InvariantCulture) || inTestData.SerializedTestData.StartsWith("{\"ID\":\"01234", System.StringComparison.InvariantCulture)) {
    //     //SerializationSystemTextJsonFixture.Serializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
    //     var philote = JsonSerializer.Deserialize<TestClassGuidPhilote>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     philote.Should().BeEquivalentTo(inTestData.InstanceTestData);
    //   }
    //   else {
    //     // ToDo: validate that strings that don't match a Guid throw an exception
    //   }
    // }

    // [Theory]
    // [MemberData(nameof(TestClassIntPhiloteInterfaceTestDataGenerator.TestData), MemberType = typeof(TestClassIntPhiloteInterfaceTestDataGenerator))]
    // public void IntPhiloteInterfaceSerializeToJson(TestClassIntPhiloteInterfaceTestData inTestData) {
    //   // ToDo low priority localize the unit test's exception's message
    //   if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
    //   // new AbstractStronglyTypedId<int>() have random Values, two sets of test data have fixed, non-random integers, the rest are random
    //   if (inTestData.SerializedTestData.StartsWith("1234", System.StringComparison.InvariantCulture) || inTestData.SerializedTestData.Equals("0")) {
    //     //SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
    //   }
    //   else {
    //     //SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the Philote ");
    //   }
    // }

    // [Theory]
    // [MemberData(nameof(TestClassIntPhiloteInterfaceTestDataGenerator.TestData), MemberType = typeof(TestClassIntPhiloteInterfaceTestDataGenerator))]
    // public void IntPhiloteInterfaceDeserializeFromJson(TestClassIntPhiloteInterfaceTestData inTestData) {
    //   // ToDo low priority localize the unit test's exception's message
    //   if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
    //   if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
    //     Action act = () => JsonSerializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     act.Should().Throw<System.Text.Json.JsonException>()
    //       .WithMessage("The input does not contain any JSON tokens.*");
    //   }
    //   else if (inTestData.SerializedTestData.StartsWith("{\"ID\":0,", System.StringComparison.InvariantCulture) || inTestData.SerializedTestData.StartsWith("{\"ID\":0,", System.StringComparison.InvariantCulture)) {
    //     //SerializationSystemTextJsonFixture.Serializer.Deserialize<IntStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
    //     var philote = JsonSerializer.Deserialize<TestClassIntPhilote>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     philote.Should().BeEquivalentTo(inTestData.InstanceTestData);
    //   }
    //   else {
    //     // ToDo: validate that strings that don't match an int throw an exception
    //   }
    // }

    // [Theory]
    // [MemberData(nameof(GuidPhiloteTestDataGenerator.StronglyTypedIdTestData), MemberType = typeof(GuidPhiloteTestDataGenerator))]
    // public void GuidPhiloteDeserializeFromJSON(GuidStronglyTypedIdTestData inTestData) {
    //   if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
    //     Action act = () => JsonSerializer.Deserialize<GuidPhilote>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     act.Should().Throw<System.Text.Json.JsonException>()
    //       .WithMessage("The input does not contain any JSON tokens.*");
    //   }
    //   else if (inTestData.SerializedTestData.StartsWith("\"0000", System.StringComparison.InvariantCulture) || inTestData.SerializedTestData.StartsWith("\"01234", System.StringComparison.InvariantCulture)) {
    //     //SerializationSystemTextJsonFixture.Serializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
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
    //     //SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
    //     // var x = JsonSerializer.Serialize(inTestData.AbstractStronglyTypedId, SerializationSystemTextJsonFixture.JsonSerializerSettings);
    //     // var y = inTestData.SerializedTestData;
    //     // x.Should().Be(y);
    //   }
    //   else {
    //     //SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
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
    // [MemberData(nameof(GuidPhiloteTestDataGenerator.PhiloteTestData), MemberType = typeof(GuidPhiloteTestDataGenerator))]
    // public void GuidIdSerializeToJSON(GuidPhiloteTestData inPhiloteTestData) {
    //   // GUIDS are random, two sets of test data have fixed, non-random guids, the rest are random
    //   if (inTestData.SerializedTestData.StartsWith("\"0000", System.StringComparison.InvariantCulture) || inTestData.SerializedTestData.StartsWith("\"01234", System.StringComparison.InvariantCulture)) {
    //     // SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
    //     // JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationSystemTextJsonFixture.JsonSerializerSettings).Should().Be(inTestData.SerializedTestData);
    //   }
    //   else {
    //     //SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
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
    //     //SerializationSystemTextJsonFixture.Serializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.AbstractStronglyTypedId);
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
