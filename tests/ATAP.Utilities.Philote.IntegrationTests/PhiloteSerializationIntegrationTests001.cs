
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

namespace ATAP.Utilities.Philote.IntegrationTests {
  // Attribution: https://github.com/xunit/xunit/issues/2007, however, we only need a class fixture not a collectionfixtire, so, commentedout below
  //  [CollectionDefinition(nameof(PhiloteSerializationSystemTextJsonUnitTests001), DisableParallelization = true)]
  //  [Collection(nameof(PhiloteSerializationSystemTextJsonUnitTests001))]
  public partial class PhiloteSerializationSystemTextJsonIntegrationTests001 : IClassFixture<SerializationSystemTextJsonFixture> {

    [Theory]
    [MemberData(nameof(GCommentWithIntTestDataGenerator.TestData), MemberType = typeof(GCommentWithIntTestDataGenerator))]
    public void GCommentWithIntPhiloteSerializeToJson(GCommentWithIntTestData inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException(paramName: $"{nameof(inTestData)} argument should never be null"); }
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
    [MemberData(nameof(GCommentWithGuidTestDataGenerator.TestData), MemberType = typeof(GCommentWithGuidTestDataGenerator))]
    public void GCommentWithGuidPhiloteSerializeToJson(GCommentWithGuidTestData inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException(paramName: $"{nameof(inTestData)} argument should never be null"); }
      if (Regex.Match(inTestData.SerializedTestData, "\"ID\":(0000|0123)").Success) {
        //SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
      }
      else {
        //SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
        JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the TestClassWithIntPhilote ");
      }
    }

    // [Theory]
    // [MemberData(nameof(TestClassWithGuidPhiloteTestDataGenerator.TestData), MemberType = typeof(TestClassWithGuidPhiloteTestDataGenerator))]
    // public void TestClassWithGuidPhiloteSerializeToJson(TestClassWithGuidPhiloteTestData inTestData) {
    //   if (Regex.Match(inTestData.SerializedTestData, "\"ID\":(0000|0123)").Success ) {
    //     //SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
    //   }
    //   else {
    //     //SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the TestClassWithGuidPhilote ");
    //   }
    // }
    //
    // [Theory]
    // [MemberData(nameof(TestClassWithIntIPhiloteTestDataGenerator.TestData), MemberType = typeof(TestClassWithIntIPhiloteTestDataGenerator))]
    // public void TestClassWithIntIPhiloteSerializeToJson(TestClassWithIntIPhiloteTestData inTestData) {
    //   if (Regex.Match(inTestData.SerializedTestData, "\"ID\":(-2147483648|-1|0|1|2147483647),").Success ) {
    //     //SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
    //   }
    //   else {
    //     //SerializationSystemTextJsonFixture.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the TestClassWithIntIPhilote ");
    //   }
    // }
    //
    // [Theory]
    // [MemberData(nameof(TestClassGuidPhiloteInterfaceTestDataGenerator.TestData), MemberType = typeof(TestClassGuidPhiloteInterfaceTestDataGenerator))]
    // public void GuidPhiloteInterfaceSerializeToJson(TestClassGuidPhiloteInterfaceTestData inTestData) {
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
    //
    // [Theory]
    // [MemberData(nameof(TestClassGuidPhiloteInterfaceTestDataGenerator.TestData), MemberType = typeof(TestClassGuidPhiloteInterfaceTestDataGenerator))]
    // public void GuidPhiloteInterfaceDeserializeFromJson(TestClassGuidPhiloteInterfaceTestData inTestData) {
    //   if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
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
    //
    // [Theory]
    // [MemberData(nameof(TestClassIntPhiloteInterfaceTestDataGenerator.TestData), MemberType = typeof(TestClassIntPhiloteInterfaceTestDataGenerator))]
    // public void IntPhiloteInterfaceSerializeToJson(TestClassIntPhiloteInterfaceTestData inTestData) {
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
    //
    //
    // [Theory]
    // [MemberData(nameof(TestClassIntPhiloteInterfaceTestDataGenerator.TestData), MemberType = typeof(TestClassIntPhiloteInterfaceTestDataGenerator))]
    // public void IntPhiloteInterfaceDeserializeFromJson(TestClassIntPhiloteInterfaceTestData inTestData) {
    //
    //
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
    // [MemberData(nameof(StronglyTypedIdInterfaceTestDataGenerator<int>.TestData), MemberType = typeof(StronglyTypedIdInterfaceTestDataGenerator<int>))]
    // public void StronglyTypedIdInterfaceIntDeserializeFromJSON(StronglyTypedIdInterfaceTestData<int> inStronglyTypedIdTestData) {
    // if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
    //   Action act = () => JsonSerializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //   act.Should().Throw<System.Text.Json.JsonException>()
    //     .WithMessage("The input does not contain any JSON tokens.*");
    // } else {
    //     //SerializationSystemTextJsonFixture.Serializer.Deserialize<GuidStronglyTypedId>(inTestData.SerializedTestData).Should().BeEquivalentTo(inTestData.InstanceTestData);
    //     var stronglyTypedId = JsonSerializer.Deserialize<IntStronglyTypedId>(inStronglyTypedIdTestData.SerializedTestData, SerializationFixture.JsonSerializerOptions);
    //     stronglyTypedId.Should().Equals(inStronglyTypedIdTestData.InstanceTestData);
    //     // ToDo: validate that strings that don't match an int throw an exception
    //   }
    // }
  }
}
