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

namespace ATAP.Utilities.StronglyTypedIds.IntegrationTests {
  // Attribution: https://github.com/xunit/xunit/issues/2007, however, we only need a class fixture not a collectionfixtire, so, commented out below
  //  [CollectionDefinition(nameof(StronglyTypedIdSerializationSystemTextJsonUnitTests001), DisableParallelization = true)]
  //  [Collection(nameof(StronglyTypedIdSerializationSystemTextJsonUnitTests001))]
  public partial class StronglyTypedIdDatabaseServiceStackMSSQLIntegrationTests001 : IClassFixture<DatabaseServiceStackSQLiteFixture> {

    //
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
    }
  }
}
