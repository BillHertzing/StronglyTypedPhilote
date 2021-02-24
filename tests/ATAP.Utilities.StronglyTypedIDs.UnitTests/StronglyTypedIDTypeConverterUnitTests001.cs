
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

  public partial class StronglyTypedIDTypeConverterUnitTests001 : IClassFixture<SerializationFixture>
  {

    [Fact]
    public void GuidIdCanConvertTests() {
      var converterGuid = TypeDescriptor.GetConverter(typeof(GuidStronglyTypedId));
      converterGuid.CanConvertFrom(typeof(string)).Should().Be(true);
      converterGuid.CanConvertFrom(typeof(Guid)).Should().Be(true);
      converterGuid.CanConvertFrom(typeof(int)).Should().Be(false);
    }
    [Fact]
    public void IntIdCanConvertTests() {
      var converterInt = TypeDescriptor.GetConverter(typeof(IntStronglyTypedId));
      converterInt.CanConvertFrom(typeof(string)).Should().Be(true);
      converterInt.CanConvertFrom(typeof(Guid)).Should().Be(false);
      converterInt.CanConvertFrom(typeof(int)).Should().Be(true);
    }
    [Fact]
    public void GuidIDToStringTest() {
      var guidStronglyTypedID = new GuidStronglyTypedId();
      guidStronglyTypedID.ToString().Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
    }

    [Theory]
    [MemberData(nameof(StronglyTypedIdTypeConverterTestDataGenerator<Guid>.StronglyTypedIdTypeConverterTestData), MemberType = typeof(StronglyTypedIdTypeConverterTestDataGenerator<Guid>))]
    public void GuidIdConvertFromString(StronglyTypedIdTypeConverterTestData<Guid> inStronglyTypedIdTestData) {
      var converterGuid = TypeDescriptor.GetConverter(typeof(GuidStronglyTypedId));
      if (inStronglyTypedIdTestData.StronglyTypedIdConvertedToString.StartsWith("0000", System.StringComparison.CurrentCulture) || inStronglyTypedIdTestData.StronglyTypedIdConvertedToString.StartsWith("01234", System.StringComparison.CurrentCulture))
      {
        //var stronglyTypedId = SerializationFixture.Serializer.Deserialize<GuidStronglyTypedId>(inStronglyTypedIdTestData.SerializedStronglyTypedId);
        var stronglyTypedId =  converterGuid.ConvertFrom(inStronglyTypedIdTestData.StronglyTypedIdConvertedToString);
        stronglyTypedId.Should().BeOfType(typeof(GuidStronglyTypedId));
        // GUIDS are random, two sets of test data have fixed, non-random guids, the rest are random
         stronglyTypedId.Should().Be(inStronglyTypedIdTestData.StronglyTypedId);
      }
      else
      {
        // No data for random guids
      }
    }

    [Theory]
    [MemberData(nameof(StronglyTypedIdTypeConverterTestDataGenerator<Guid>.StronglyTypedIdTypeConverterTestData), MemberType = typeof(StronglyTypedIdTypeConverterTestDataGenerator<Guid>))]
    public void GuidIdConvertToString(StronglyTypedIdTypeConverterTestData<Guid> inStronglyTypedIdTestData)
    {
      var converterGuid = TypeDescriptor.GetConverter(typeof(GuidStronglyTypedId));
      // GUIDS are random, two sets of test data have fixed, non-random guids, the rest are random
      if (inStronglyTypedIdTestData.StronglyTypedIdConvertedToString.StartsWith("0000", System.StringComparison.CurrentCulture) || inStronglyTypedIdTestData.StronglyTypedIdConvertedToString.StartsWith("01234", System.StringComparison.CurrentCulture))
      {
         converterGuid.ConvertTo(inStronglyTypedIdTestData.StronglyTypedId,typeof(string)).Should().Be(inStronglyTypedIdTestData.StronglyTypedIdConvertedToString);
      }
      else
      {
         ((string)converterGuid.ConvertTo(inStronglyTypedIdTestData.StronglyTypedId,typeof(string))).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
      }
    }

    [Fact]
    public void IntIDToStringTest() {
      var intStronglyTypedID = new IntStronglyTypedId();
      intStronglyTypedID.ToString().Should().MatchRegex("^\\d+$");
    }

    [Theory]
    [MemberData(nameof(StronglyTypedIdTypeConverterTestDataGenerator<int>.StronglyTypedIdTypeConverterTestData), MemberType = typeof(StronglyTypedIdTypeConverterTestDataGenerator<int>))]
    public void IntIdConvertFromString(StronglyTypedIdTypeConverterTestData<int> inStronglyTypedIdTestData) {
      var converterInt = TypeDescriptor.GetConverter(typeof(IntStronglyTypedId));
      if (inStronglyTypedIdTestData.StronglyTypedIdConvertedToString.StartsWith("0000", System.StringComparison.CurrentCulture) || inStronglyTypedIdTestData.StronglyTypedIdConvertedToString.StartsWith("01234", System.StringComparison.CurrentCulture))
      {
        //var stronglyTypedId = SerializationFixture.Serializer.Deserialize<IntStronglyTypedId>(inStronglyTypedIdTestData.SerializedStronglyTypedId);
        var stronglyTypedId =  converterInt.ConvertFrom(inStronglyTypedIdTestData.StronglyTypedIdConvertedToString);
        stronglyTypedId.Should().BeOfType(typeof(IntStronglyTypedId));
        // two sets of test data have fixed, non-random Integers, the rest are random
         stronglyTypedId.Should().Be(inStronglyTypedIdTestData.StronglyTypedId);
      }
      else
      {
        // No data for random Integers
      }
    }

    [Theory]
    [MemberData(nameof(StronglyTypedIdTypeConverterTestDataGenerator<int>.StronglyTypedIdTypeConverterTestData), MemberType = typeof(StronglyTypedIdTypeConverterTestDataGenerator<int>))]
    public void IntIdConvertToString(StronglyTypedIdTypeConverterTestData<int> inStronglyTypedIdTestData)
    {
      var converterInt = TypeDescriptor.GetConverter(typeof(IntStronglyTypedId));
      // two sets of test data have fixed, non-random Integers, the rest are random
      if (inStronglyTypedIdTestData.StronglyTypedIdConvertedToString.Equals("0") || inStronglyTypedIdTestData.StronglyTypedIdConvertedToString.Equals("1234567"))
      {
         converterInt.ConvertTo(inStronglyTypedIdTestData.StronglyTypedId,typeof(string)).Should().Be(inStronglyTypedIdTestData.StronglyTypedIdConvertedToString);
      }
      else
      {
        // No test available for random integer
      }
    }


  }
}
