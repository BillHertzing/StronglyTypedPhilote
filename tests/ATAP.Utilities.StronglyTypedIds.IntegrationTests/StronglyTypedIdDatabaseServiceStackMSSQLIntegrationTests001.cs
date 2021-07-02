using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using System.ComponentModel;

using ServiceStack;
using ServiceStack.OrmLite;
using System.Data;
using ATAP.Utilities.StronglyTypedIds;
using ATAP.Utilities.StronglyTypedIds.TestData;

namespace ATAP.Utilities.StronglyTypedIds.IntegrationTests {
  // Attribution: https://github.com/xunit/xunit/issues/2007, however, we only need a class fixture not a collectionfixtire, so, commented out below
  //  [CollectionDefinition(nameof(StronglyTypedIdSerializationSystemTextJsonUnitTests001), DisableParallelization = true)]
  //  [Collection(nameof(StronglyTypedIdSerializationSystemTextJsonUnitTests001))]
  public partial class StronglyTypedIdDatabaseServiceStackMSSQLIntegrationTests001 : IClassFixture<StronglyTypedIdsIntegrationTestsDatabaseFixture> {


    [Fact]
    public void GuidStronglyTypedIdTable() {
      // Arrange
      // Start a transaction
      using (IDbTransaction dbTrans = DatabaseFixture.Db.OpenTransaction()) {
        // Act
        // Create a table in the database for a GuidStronglyTypedId
        DatabaseFixture.Db.CreateTableIfNotExists<GuidStronglyTypedId>();
        // Assert
        // Assert that the table exists, and has a single column, called Id, with the Primary Key attribute
        var tablesWithRowCounts = DatabaseFixture.Db.GetTableNamesWithRowCounts(live:true);
        tablesWithRowCounts.Should().NotBeEmpty().And.HaveCount(1);
        // Rollback the transaction
        dbTrans.Rollback();
      }
    }

    [Theory]
    [MemberData(nameof(GuidStronglyTypedIdTestDataGenerator.StronglyTypedIdTestData), MemberType = typeof(GuidStronglyTypedIdTestDataGenerator))]
    public void GuidStronglyTypedIdToDb(GuidStronglyTypedIdTestData inTestData) {
      // ToDo low priority localize the unit test's exception's message
      if (inTestData == null) { throw new ArgumentNullException(nameof(inTestData)); }
      // Arrange
      // Start a transaction
      using (IDbTransaction dbTrans = DatabaseFixture.Db.OpenTransaction()) {
        // Act
        // Create a table in the database for a GuidStronglyTypedId
        // store the GuidStronglyTypedId object from the inTestData into the table
        DatabaseFixture.Db.Insert(inTestData.InstanceTestData);
        // Assert
        // Assert that the current row count for the table GuidStronglyTypedId is 1
        // Assert that the value of Id in the table's only row is the same as the object's value
        // Rollback the transaction
        dbTrans.Rollback();
      }

      // GUIDS are random, two sets of test data have fixed, non-random guids, the rest are random
      // if (inTestData.SerializedTestData.StartsWith("\"0000", System.StringComparison.InvariantCulture) || inTestData.SerializedTestData.StartsWith("\"01234", System.StringComparison.InvariantCulture)) {
      //   // DatabaseFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
      //   JsonSerializer.Serialize(inTestData.InstanceTestData, DatabaseFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
      // }
      // else {
      //   //DatabaseFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
      //   JsonSerializer.Serialize(inTestData.InstanceTestData, DatabaseFixture.JsonSerializerOptions).Should().MatchRegex("^\"[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}\"$");
      // }
    }

    // [Theory]
    // [MemberData(nameof(IntStronglyTypedIdTestDataGenerator.StronglyTypedIdTestData), MemberType = typeof(IntStronglyTypedIdTestDataGenerator))]
    // public void IntStronglyTypedIdSerializeToJSON(IntStronglyTypedIdTestData inTestData) {
    //   // ToDo low priority localize the unit test's exception's message
    //   if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
    //   // new AbstractStronglyTypedId<int>() have random Values, two sets of test data have fixed, non-random integers, the rest are random
    //   if (inTestData.SerializedTestData.Equals("-2147483648") || inTestData.SerializedTestData.Equals("-1") || inTestData.SerializedTestData.Equals("0") || inTestData.SerializedTestData.Equals("2147483647") || inTestData.SerializedTestData.Equals("1234567")) {
    //     //DatabaseFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().Be(inTestData.SerializedTestData);
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, DatabaseFixture.JsonSerializerOptions).Should().Be(inTestData.SerializedTestData);
    //   }
    //   else {
    //     //DatabaseFixtureSystemTextJson.Serializer.Serialize(inTestData.InstanceTestData).Should().MatchRegex("^[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}$");
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, DatabaseFixture.JsonSerializerOptions).Should().BeOfType(typeof(string), "the serializer should have returned a string representation of the InstanceTestData ");
    //     JsonSerializer.Serialize(inTestData.InstanceTestData, DatabaseFixture.JsonSerializerOptions).Should().MatchRegex("^-{0,1}\\d+$");
    //   }
    // }

    // [Theory]
    // [MemberData(nameof(IntStronglyTypedIdTestDataGenerator.StronglyTypedIdTestData), MemberType = typeof(IntStronglyTypedIdTestDataGenerator))]
    // public void IntStronglyTypedIdDeserializeFromJSON(IntStronglyTypedIdTestData inTestData) {
    //   // ToDo low priority localize the unit test's exception's message
    //   if (inTestData == null) { throw new ArgumentNullException($"{nameof(inTestData)} argument should never be null"); }
    //   if (String.IsNullOrEmpty(inTestData.SerializedTestData)) {
    //     Action act = () => JsonSerializer.Deserialize<IntStronglyTypedId>(inTestData.SerializedTestData, DatabaseFixture.JsonSerializerOptions);
    //     act.Should().Throw<System.Text.Json.JsonException>()
    //       .WithMessage("The input does not contain any JSON tokens.*");
    //   }
    //   else {
    //     // ToDo: validate that non-integer strings throw an exception
    //     var stronglyTypedId = JsonSerializer.Deserialize<IntStronglyTypedId>(inTestData.SerializedTestData, DatabaseFixture.JsonSerializerOptions);
    //     stronglyTypedId.Should().BeEquivalentTo(inTestData.InstanceTestData);
    //   }
    // }

  }
}

