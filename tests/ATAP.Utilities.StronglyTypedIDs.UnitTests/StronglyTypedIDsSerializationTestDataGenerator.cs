using System.Collections.Generic;
using System.Collections;
using ATAP.Utilities.StronglyTypedID;
using System;


namespace ATAP.Utilities.StronglyTypedId.UnitTests {

  //ToDo add validation tests to ensure illegal values are not allowed.  This applies to all XxTestDataGenerator classes
  public class StronglyTypedIdInterfaceSerializationTestData<TValue> {
    public IStronglyTypedId<TValue> StronglyTypedId { get; set; }
    public string SerializedStronglyTypedId { get; set; }

    public StronglyTypedIdInterfaceSerializationTestData() {
    }

    public StronglyTypedIdInterfaceSerializationTestData(IStronglyTypedId<TValue> stronglyTypedId, string serializedStronglyTypedId) {
      StronglyTypedId = stronglyTypedId;
      SerializedStronglyTypedId = serializedStronglyTypedId ?? throw new ArgumentNullException(nameof(serializedStronglyTypedId));
    }
  }

  public class StronglyTypedIdInterfaceSerializationTestDataGenerator<TValue> : IEnumerable<object[]> {
    public static IEnumerable<object[]> StronglyTypedIdSerializationTestData() {
      switch (typeof(TValue)) {
        case Type guidType when typeof(TValue) == typeof(Guid): {
            yield return new StronglyTypedIdInterfaceSerializationTestData<TValue>[] { new StronglyTypedIdInterfaceSerializationTestData<TValue> { StronglyTypedId = (ATAP.Utilities.StronglyTypedID.IStronglyTypedId<TValue>)new GuidStronglyTypedId(Guid.Empty), SerializedStronglyTypedId = "\"00000000-0000-0000-0000-000000000000\"" } };
            yield return new StronglyTypedIdInterfaceSerializationTestData<TValue>[] { new StronglyTypedIdInterfaceSerializationTestData<TValue> { StronglyTypedId = (ATAP.Utilities.StronglyTypedID.IStronglyTypedId<TValue>)new GuidStronglyTypedId(new Guid("01234567-abcd-9876-cdef-456789abcdef")), SerializedStronglyTypedId = "\"01234567-abcd-9876-cdef-456789abcdef\"" } };
            yield return new StronglyTypedIdInterfaceSerializationTestData<TValue>[] { new StronglyTypedIdInterfaceSerializationTestData<TValue> { StronglyTypedId = (ATAP.Utilities.StronglyTypedID.IStronglyTypedId<TValue>)new GuidStronglyTypedId(Guid.NewGuid()), SerializedStronglyTypedId = "" } };
          }
          break;
        case Type intType when typeof(TValue) == typeof(int): {
            yield return new StronglyTypedIdInterfaceSerializationTestData<TValue>[] { new StronglyTypedIdInterfaceSerializationTestData<TValue> { StronglyTypedId = (ATAP.Utilities.StronglyTypedID.IStronglyTypedId<TValue>)new IntStronglyTypedId(0), SerializedStronglyTypedId = "0" } };
            yield return new StronglyTypedIdInterfaceSerializationTestData<TValue>[] { new StronglyTypedIdInterfaceSerializationTestData<TValue> { StronglyTypedId = (ATAP.Utilities.StronglyTypedID.IStronglyTypedId<TValue>)new IntStronglyTypedId(1234567), SerializedStronglyTypedId = "1234567" } };
            yield return new StronglyTypedIdInterfaceSerializationTestData<TValue>[] { new StronglyTypedIdInterfaceSerializationTestData<TValue> { StronglyTypedId = (ATAP.Utilities.StronglyTypedID.IStronglyTypedId<TValue>)new IntStronglyTypedId(new Random().Next()), SerializedStronglyTypedId = "" } };
          }
          break;
        // ToDo: replace with new custom exception and localization of exception message
        default:
          throw new Exception(FormattableString.Invariant($"Invalid TValue type {typeof(TValue)}"));
      }
    }

    public IEnumerator<object[]> GetEnumerator() { return StronglyTypedIdSerializationTestData().GetEnumerator(); }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  }

  public class GuidStronglyTypedIdSerializationTestData {
    public GuidStronglyTypedId StronglyTypedId { get; set; }
    public string SerializedStronglyTypedId { get; set; }

    public GuidStronglyTypedIdSerializationTestData() {
    }

    public GuidStronglyTypedIdSerializationTestData(GuidStronglyTypedId stronglyTypedId, string serializedStronglyTypedId) {
      StronglyTypedId = stronglyTypedId;
      SerializedStronglyTypedId = serializedStronglyTypedId ?? throw new ArgumentNullException(nameof(serializedStronglyTypedId));
    }
  }

  public class GuidStronglyTypedIdSerializationTestDataGenerator : IEnumerable<object[]> {
    public static IEnumerable<object[]> StronglyTypedIdSerializationTestData() {
      yield return new GuidStronglyTypedIdSerializationTestData[] { new GuidStronglyTypedIdSerializationTestData { StronglyTypedId = new GuidStronglyTypedId(Guid.Empty), SerializedStronglyTypedId = "\"00000000-0000-0000-0000-000000000000\"" } };
      yield return new GuidStronglyTypedIdSerializationTestData[] { new GuidStronglyTypedIdSerializationTestData { StronglyTypedId = new GuidStronglyTypedId(new Guid("01234567-abcd-9876-cdef-456789abcdef")), SerializedStronglyTypedId = "\"01234567-abcd-9876-cdef-456789abcdef\"" } };
      yield return new GuidStronglyTypedIdSerializationTestData[] { new GuidStronglyTypedIdSerializationTestData { StronglyTypedId = new GuidStronglyTypedId(new Guid("A1234567-abcd-9876-cdef-456789abcdef")), SerializedStronglyTypedId = "\"A1234567-abcd-9876-cdef-456789abcdef\"" } };
      yield return new GuidStronglyTypedIdSerializationTestData[] { new GuidStronglyTypedIdSerializationTestData { StronglyTypedId = new GuidStronglyTypedId(Guid.NewGuid()), SerializedStronglyTypedId = "" } };
    }

    public IEnumerator<object[]> GetEnumerator() { return StronglyTypedIdSerializationTestData().GetEnumerator(); }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  }

  public class IntStronglyTypedIdSerializationTestData {
    public IntStronglyTypedId StronglyTypedId { get; set; }
    public string SerializedStronglyTypedId { get; set; }

    public IntStronglyTypedIdSerializationTestData() {
    }

    public IntStronglyTypedIdSerializationTestData(IntStronglyTypedId stronglyTypedId, string serializedStronglyTypedId) {
      StronglyTypedId = stronglyTypedId;
      SerializedStronglyTypedId = serializedStronglyTypedId ?? throw new ArgumentNullException(nameof(serializedStronglyTypedId));
    }
  }

  public class IntStronglyTypedIdSerializationTestDataGenerator : IEnumerable<object[]> {
    public static IEnumerable<object[]> StronglyTypedIdSerializationTestData() {
      yield return new IntStronglyTypedIdSerializationTestData[] { new IntStronglyTypedIdSerializationTestData { StronglyTypedId = new IntStronglyTypedId(0), SerializedStronglyTypedId = "0" } };
      yield return new IntStronglyTypedIdSerializationTestData[] { new IntStronglyTypedIdSerializationTestData { StronglyTypedId = new IntStronglyTypedId(1234567), SerializedStronglyTypedId = "1234567" } };
      yield return new IntStronglyTypedIdSerializationTestData[] { new IntStronglyTypedIdSerializationTestData { StronglyTypedId = new IntStronglyTypedId(new Random().Next()), SerializedStronglyTypedId = "" } };
    }

    public IEnumerator<object[]> GetEnumerator() { return StronglyTypedIdSerializationTestData().GetEnumerator(); }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  }
}
