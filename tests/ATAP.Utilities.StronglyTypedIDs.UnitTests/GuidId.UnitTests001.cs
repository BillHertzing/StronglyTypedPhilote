
using System;
using System.Collections;
using System.Collections.Generic;
using ATAP.Utilities.StronglyTypedID;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

// For the tests that use the new Serializer/Deserializer
using System.Text.Json;
// For the tests that use the old Newtonsoft Serializer/Deserializer
//using Newtonsoft.Json;

namespace ATAP.Utilities.StronglyTypedId.UnitTests
{

  public partial class StronglyTypedIDSerializationUnitTests001 : IClassFixture<Fixture>
  {

    [Theory]
    [MemberData(nameof(StronglyTypedIdSerializationTestDataGenerator<Guid>.StronglyTypedIdSerializationTestData), MemberType = typeof(StronglyTypedIdSerializationTestDataGenerator<Guid>))]
    public void GuidIdDeserializeFromJSON(StronglyTypedIdSerializationTestData<Guid> inStronglyTypedIdTestData)
    {
      if (inStronglyTypedIdTestData.StronglyTypedId.ToString().StartsWith("0000") | inStronglyTypedIdTestData.StronglyTypedId.ToString().StartsWith("01234"))
      {
        //var stronglyTypedId = Fixture.Serializer.Deserialize<GuidStronglyTypedId>(inStronglyTypedIdTestData.SerializedStronglyTypedId);
        var stronglyTypedId =  JsonSerializer.Deserialize<GuidStronglyTypedId>(inStronglyTypedIdTestData.SerializedStronglyTypedId);
        stronglyTypedId.Should().BeOfType(typeof(GuidStronglyTypedId));
        // GUIDS are random, two sets of test data have fixed, non-random guids, the rest are random
        //Fixture.Serializer.Deserialize<GuidStronglyTypedId>(inStronglyTypedIdTestData.SerializedStronglyTypedId).Should().Be(inStronglyTypedIdTestData.StronglyTypedId);
         JsonSerializer.Deserialize<GuidStronglyTypedId>(inStronglyTypedIdTestData.SerializedStronglyTypedId).Should().Be(inStronglyTypedIdTestData.StronglyTypedId);
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
      if (inStronglyTypedIdTestData.StronglyTypedId.ToString().StartsWith("0000") | inStronglyTypedIdTestData.StronglyTypedId.ToString().StartsWith("01234"))
      {
        //Fixture.Serializer.Serialize(inStronglyTypedIdTestData.StronglyTypedId).Should().Be(inStronglyTypedIdTestData.SerializedStronglyTypedId);
         JsonSerializer.Serialize(inStronglyTypedIdTestData.StronglyTypedId).Should().Be(inStronglyTypedIdTestData.SerializedStronglyTypedId);
      }
      else
      {
        //Fixture.Serializer.Serialize(inStronglyTypedIdTestData.StronglyTypedId).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
         JsonSerializer.Serialize(inStronglyTypedIdTestData.StronglyTypedId).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
      }
    }


  }
}
