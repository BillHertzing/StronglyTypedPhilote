using System.Collections.Generic;
using System.Collections;
using ATAP.Utilities.StronglyTypedID;
using System;


namespace ATAP.Utilities.StronglyTypedId.UnitTests
{

  //ToDo add validation tests to ensure illegal values are not allowed.  This applies to all XxTestDataGenerator classes
  public class StronglyTypedIdSerializationTestData<TValue>
  {
    public IStronglyTypedId<TValue> StronglyTypedId;
    public string SerializedStronglyTypedId;

    public StronglyTypedIdSerializationTestData()
    {
    }

    public StronglyTypedIdSerializationTestData(IStronglyTypedId<TValue> stronglyTypedId, string serializedStronglyTypedId)
    {
      StronglyTypedId = stronglyTypedId;
      SerializedStronglyTypedId = serializedStronglyTypedId ?? throw new ArgumentNullException(nameof(serializedStronglyTypedId));
    }
  }

  public class StronglyTypedIdSerializationTestDataGenerator<TValue>  : IEnumerable<object[]> {

    public static IEnumerable<object[]> StronglyTypedIdSerializationTestData() {
      switch (typeof(TValue)) {
        case Type guidType when typeof(TValue) == typeof(Guid): {
            yield return new StronglyTypedIdSerializationTestData<TValue>[] { new StronglyTypedIdSerializationTestData<TValue> { StronglyTypedId = (ATAP.Utilities.StronglyTypedID.IStronglyTypedId<TValue>)new GuidStronglyTypedId(Guid.Empty), SerializedStronglyTypedId = "00000000-0000-0000-0000-000000000000" } };
            yield return new StronglyTypedIdSerializationTestData<TValue>[] { new StronglyTypedIdSerializationTestData<TValue> { StronglyTypedId = (ATAP.Utilities.StronglyTypedID.IStronglyTypedId<TValue>)new GuidStronglyTypedId(new Guid("01234567-abcd-9876-cdef-456789abcdef")), SerializedStronglyTypedId = "01234567-abcd-9876-cdef-456789abcdef" } };
            yield return new StronglyTypedIdSerializationTestData<TValue>[] { new StronglyTypedIdSerializationTestData<TValue> { StronglyTypedId = (ATAP.Utilities.StronglyTypedID.IStronglyTypedId<TValue>)new GuidStronglyTypedId(Guid.NewGuid()), SerializedStronglyTypedId = "Random, so ignore this property of the test data" } };
          }
          break;
        case Type intType when typeof(TValue) == typeof(int): {
            yield return new StronglyTypedIdSerializationTestData<TValue>[] { new StronglyTypedIdSerializationTestData<TValue> { StronglyTypedId = (ATAP.Utilities.StronglyTypedID.IStronglyTypedId<TValue>)new IntStronglyTypedId(0), SerializedStronglyTypedId = "0" } };
            yield return new StronglyTypedIdSerializationTestData<TValue>[] { new StronglyTypedIdSerializationTestData<TValue> { StronglyTypedId = (ATAP.Utilities.StronglyTypedID.IStronglyTypedId<TValue>)new IntStronglyTypedId(1234567), SerializedStronglyTypedId = "1234567" } };
            yield return new StronglyTypedIdSerializationTestData<TValue>[] { new StronglyTypedIdSerializationTestData<TValue> { StronglyTypedId = (ATAP.Utilities.StronglyTypedID.IStronglyTypedId<TValue>)new IntStronglyTypedId(new Random().Next()), SerializedStronglyTypedId = "Random, so ignore this property of the test data" } };
          }
          break;
        // ToDo: replace with new custom exception and localization of exception message
        default:
          throw new Exception(String.Format("Invalid TValue type {0}", typeof(TValue)));
      }
    }


    public IEnumerator<object[]> GetEnumerator() { return StronglyTypedIdSerializationTestData().GetEnumerator(); }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  }
}
