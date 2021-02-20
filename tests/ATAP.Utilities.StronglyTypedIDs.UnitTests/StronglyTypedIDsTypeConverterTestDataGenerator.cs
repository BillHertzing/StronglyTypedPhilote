using System.Collections.Generic;
using System.Collections;
using ATAP.Utilities.StronglyTypedID;
using System;


namespace ATAP.Utilities.StronglyTypedId.UnitTests
{

  //ToDo add validation tests to ensure illegal values are not allowed.  This applies to all XxTestDataGenerator classes
  public class StronglyTypedIdTypeConverterTestData<TValue>
  {
    public IStronglyTypedId<TValue> StronglyTypedId;
    public string StronglyTypedIdConvertedToString;

    public StronglyTypedIdTypeConverterTestData()
    {
    }

    public StronglyTypedIdTypeConverterTestData(IStronglyTypedId<TValue> stronglyTypedId, string stronglyTypedIdConvertedToString)
    {
      StronglyTypedId = stronglyTypedId;
      StronglyTypedIdConvertedToString = stronglyTypedIdConvertedToString ?? throw new ArgumentNullException(nameof(stronglyTypedIdConvertedToString));
    }
  }

  public class StronglyTypedIdTypeConverterTestDataGenerator<TValue>  : IEnumerable<object[]> {

    public static IEnumerable<object[]> StronglyTypedIdTypeConverterTestData() {
      switch (typeof(TValue)) {
        case Type guidType when typeof(TValue) == typeof(Guid): {
            yield return new StronglyTypedIdTypeConverterTestData<TValue>[] { new StronglyTypedIdTypeConverterTestData<TValue> { StronglyTypedId = (ATAP.Utilities.StronglyTypedID.IStronglyTypedId<TValue>)new GuidStronglyTypedId(Guid.Empty), StronglyTypedIdConvertedToString = "00000000-0000-0000-0000-000000000000" } };
            yield return new StronglyTypedIdTypeConverterTestData<TValue>[] { new StronglyTypedIdTypeConverterTestData<TValue> { StronglyTypedId = (ATAP.Utilities.StronglyTypedID.IStronglyTypedId<TValue>)new GuidStronglyTypedId(new Guid("01234567-abcd-9876-cdef-456789abcdef")), StronglyTypedIdConvertedToString = "01234567-abcd-9876-cdef-456789abcdef" } };
            yield return new StronglyTypedIdTypeConverterTestData<TValue>[] { new StronglyTypedIdTypeConverterTestData<TValue> { StronglyTypedId = (ATAP.Utilities.StronglyTypedID.IStronglyTypedId<TValue>)new GuidStronglyTypedId(Guid.NewGuid()), StronglyTypedIdConvertedToString = "Random, so ignore this property of the test data" } };
          }
          break;
        case Type intType when typeof(TValue) == typeof(int): {
            yield return new StronglyTypedIdTypeConverterTestData<TValue>[] { new StronglyTypedIdTypeConverterTestData<TValue> { StronglyTypedId = (ATAP.Utilities.StronglyTypedID.IStronglyTypedId<TValue>)new IntStronglyTypedId(0), StronglyTypedIdConvertedToString = "0" } };
            yield return new StronglyTypedIdTypeConverterTestData<TValue>[] { new StronglyTypedIdTypeConverterTestData<TValue> { StronglyTypedId = (ATAP.Utilities.StronglyTypedID.IStronglyTypedId<TValue>)new IntStronglyTypedId(1234567), StronglyTypedIdConvertedToString = "1234567" } };
            yield return new StronglyTypedIdTypeConverterTestData<TValue>[] { new StronglyTypedIdTypeConverterTestData<TValue> { StronglyTypedId = (ATAP.Utilities.StronglyTypedID.IStronglyTypedId<TValue>)new IntStronglyTypedId(new Random().Next()), StronglyTypedIdConvertedToString = "Random, so ignore this property of the test data" } };
          }
          break;
        // ToDo: replace with new custom exception and localization of exception message
        default:
          throw new Exception(String.Format("Invalid TValue type {0}", typeof(TValue)));
      }
    }


    public IEnumerator<object[]> GetEnumerator() { return StronglyTypedIdTypeConverterTestData().GetEnumerator(); }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  }
}
