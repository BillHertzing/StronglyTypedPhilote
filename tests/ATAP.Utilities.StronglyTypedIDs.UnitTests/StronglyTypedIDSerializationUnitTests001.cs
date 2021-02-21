
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

namespace ATAP.Utilities.StronglyTypedId.UnitTests
{
  // Attribution: https://github.com/xunit/xunit/issues/2007, however, we only need a class fixture not a collectionfixtire, so, commentedout below
  //  [CollectionDefinition(nameof(StronglyTypedIDSerializationUnitTests001), DisableParallelization = true)]
	//  [Collection(nameof(StronglyTypedIDSerializationUnitTests001))]
  public partial class StronglyTypedIDSerializationUnitTests001 : IClassFixture<SerializationFixture>
  {

    [Theory]
    [MemberData(nameof(StronglyTypedIdSerializationTestDataGenerator<Guid>.StronglyTypedIdSerializationTestData), MemberType = typeof(StronglyTypedIdSerializationTestDataGenerator<Guid>))]
    public void GuidIdDeserializeFromJSON(StronglyTypedIdSerializationTestData<Guid> inStronglyTypedIdTestData) {

      if (inStronglyTypedIdTestData.StronglyTypedId.ToString().StartsWith("0000",System.StringComparison.InvariantCulture) | inStronglyTypedIdTestData.StronglyTypedId.ToString().StartsWith("01234",System.StringComparison.InvariantCulture))
      {
        //var stronglyTypedId = SerializationFixture.Serializer.Deserialize<GuidStronglyTypedId>(inStronglyTypedIdTestData.SerializedStronglyTypedId);
        var stronglyTypedId =  JsonSerializer.Deserialize<GuidStronglyTypedId>(inStronglyTypedIdTestData.SerializedStronglyTypedId, SerializationFixture.JsonSerializerOptions);
        stronglyTypedId.Should().BeOfType(typeof(GuidStronglyTypedId));
        // GUIDS are random, two sets of test data have fixed, non-random guids, the rest are random
        //SerializationFixture.Serializer.Deserialize<GuidStronglyTypedId>(inStronglyTypedIdTestData.SerializedStronglyTypedId).Should().Be(inStronglyTypedIdTestData.StronglyTypedId);
         JsonSerializer.Deserialize<GuidStronglyTypedId>(inStronglyTypedIdTestData.SerializedStronglyTypedId, SerializationFixture.JsonSerializerOptions).Should().Be(inStronglyTypedIdTestData.StronglyTypedId);
      }
      else
      {
        // No data for random guids
      }
    }

    [Theory]
    [MemberData(nameof(StronglyTypedIdSerializationTestDataGenerator<Guid>.StronglyTypedIdSerializationTestData), MemberType = typeof(StronglyTypedIdSerializationTestDataGenerator<Guid>))]
    public void GuidIdSerializeToJSON(StronglyTypedIdSerializationTestData<Guid> inStronglyTypedIdTestData)
    {
      // GUIDS are random, two sets of test data have fixed, non-random guids, the rest are random
      if (inStronglyTypedIdTestData.StronglyTypedId.ToString().StartsWith("0000",System.StringComparison.InvariantCulture) )
      {
        //SerializationFixture.Serializer.Serialize(inStronglyTypedIdTestData.StronglyTypedId).Should().Be(inStronglyTypedIdTestData.SerializedStronglyTypedId);
         JsonSerializer.Serialize(inStronglyTypedIdTestData.StronglyTypedId, SerializationFixture.JsonSerializerOptions).Should().Be(inStronglyTypedIdTestData.SerializedStronglyTypedId);
      }
      else
      {
        //SerializationFixture.Serializer.Serialize(inStronglyTypedIdTestData.StronglyTypedId).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
         JsonSerializer.Serialize(inStronglyTypedIdTestData.StronglyTypedId, SerializationFixture.JsonSerializerOptions).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
      }
    }

    [Theory]
    [MemberData(nameof(StronglyTypedIdSerializationTestDataGenerator<int>.StronglyTypedIdSerializationTestData), MemberType = typeof(StronglyTypedIdSerializationTestDataGenerator<int>))]
    public void IntIdSerializeToJSON(StronglyTypedIdSerializationTestData<int> inStronglyTypedIdTestData)
    {
      // new StronglyTypedID<int>() have random Values, two sets of test data have fixed, non-random integers, the rest are random
      if ( inStronglyTypedIdTestData.StronglyTypedId.ToString().StartsWith("1234",System.StringComparison.InvariantCulture) || inStronglyTypedIdTestData.StronglyTypedId.ToString().Equals("0"))
      {
        //SerializationFixture.Serializer.Serialize(inStronglyTypedIdTestData.StronglyTypedId).Should().Be(inStronglyTypedIdTestData.SerializedStronglyTypedId);
        // JsonSerializer.Serialize(inStronglyTypedIdTestData.StronglyTypedId, SerializationFixture.JsonSerializerOptions).Should().Be(inStronglyTypedIdTestData.SerializedStronglyTypedId);
        var x = JsonSerializer.Serialize(inStronglyTypedIdTestData.StronglyTypedId, SerializationFixture.JsonSerializerOptions);
        var y = inStronglyTypedIdTestData.SerializedStronglyTypedId;
        x.Should().Be(y);
      }
      else
      {
        //SerializationFixture.Serializer.Serialize(inStronglyTypedIdTestData.StronglyTypedId).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
         JsonSerializer.Serialize(inStronglyTypedIdTestData.StronglyTypedId, SerializationFixture.JsonSerializerOptions).Should().BeOfType(typeof(string),"the serializer should have returned a string representation of the StronglyTypedId ");
      }
    }


  }
}
