using System.Collections.Generic;
using System.Collections;
using ATAP.Utilities.StronglyTypedIds;
using System;


namespace ATAP.Utilities.StronglyTypedIds.UnitTests {

  //ToDo add validation tests to ensure illegal values are not allowed.  This applies to all XxTestDataGenerator classes
  public class StronglyTypedIdInterfaceSerializationTestData<TValue> where TValue : notnull {
    public IStronglyTypedId<TValue> InstanceTestData { get; set; }
    public string SerializedTestData { get; set; }

    public StronglyTypedIdInterfaceSerializationTestData() {
    }

    public StronglyTypedIdInterfaceSerializationTestData(IStronglyTypedId<TValue> instanceTestData, string serializedTestData) {
      InstanceTestData = instanceTestData;
      SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
    }
  }

  public class StronglyTypedIdInterfaceSerializationTestDataGenerator<TValue> : IEnumerable<object[]> where TValue : notnull  {
    public static IEnumerable<object[]> StronglyTypedIdSerializationTestData() {
      switch (typeof(TValue)) {
        case Type guidType when typeof(TValue) == typeof(Guid): {
            yield return new StronglyTypedIdInterfaceSerializationTestData<TValue>[] { new StronglyTypedIdInterfaceSerializationTestData<TValue> { InstanceTestData = (IStronglyTypedId<TValue>)new GuidStronglyTypedId(Guid.Empty), SerializedTestData = "\"00000000-0000-0000-0000-000000000000\"" } };
            yield return new StronglyTypedIdInterfaceSerializationTestData<TValue>[] { new StronglyTypedIdInterfaceSerializationTestData<TValue> { InstanceTestData = (IStronglyTypedId<TValue>)new GuidStronglyTypedId(new Guid("01234567-abcd-9876-cdef-456789abcdef")), SerializedTestData = "\"01234567-abcd-9876-cdef-456789abcdef\"" } };
            yield return new StronglyTypedIdInterfaceSerializationTestData<TValue>[] { new StronglyTypedIdInterfaceSerializationTestData<TValue> { InstanceTestData = (IStronglyTypedId<TValue>)new GuidStronglyTypedId(Guid.NewGuid()), SerializedTestData = "" } };
          }
          break;
        case Type intType when typeof(TValue) == typeof(int): {
            yield return new StronglyTypedIdInterfaceSerializationTestData<TValue>[] { new StronglyTypedIdInterfaceSerializationTestData<TValue> { InstanceTestData = (IStronglyTypedId<TValue>)new IntStronglyTypedId(0), SerializedTestData = "0" } };
            yield return new StronglyTypedIdInterfaceSerializationTestData<TValue>[] { new StronglyTypedIdInterfaceSerializationTestData<TValue> { InstanceTestData = (IStronglyTypedId<TValue>)new IntStronglyTypedId(1234567), SerializedTestData = "1234567" } };
            yield return new StronglyTypedIdInterfaceSerializationTestData<TValue>[] { new StronglyTypedIdInterfaceSerializationTestData<TValue> { InstanceTestData = (IStronglyTypedId<TValue>)new IntStronglyTypedId(new Random().Next()), SerializedTestData = "" } };
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
    public GuidStronglyTypedId InstanceTestData { get; set; }
    public string SerializedTestData { get; set; }

    public GuidStronglyTypedIdSerializationTestData() {
    }

    public GuidStronglyTypedIdSerializationTestData(GuidStronglyTypedId instanceTestData, string serializedTestData) {
      InstanceTestData = instanceTestData;
      SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
    }
  }

  public class GuidStronglyTypedIdSerializationTestDataGenerator : IEnumerable<object[]> {
    public static IEnumerable<object[]> StronglyTypedIdSerializationTestData() {
      yield return new GuidStronglyTypedIdSerializationTestData[] { new GuidStronglyTypedIdSerializationTestData { InstanceTestData = new GuidStronglyTypedId(Guid.Empty), SerializedTestData = "\"00000000-0000-0000-0000-000000000000\"" } };
      yield return new GuidStronglyTypedIdSerializationTestData[] { new GuidStronglyTypedIdSerializationTestData { InstanceTestData = new GuidStronglyTypedId(new Guid("01234567-abcd-9876-cdef-456789abcdef")), SerializedTestData = "\"01234567-abcd-9876-cdef-456789abcdef\"" } };
      yield return new GuidStronglyTypedIdSerializationTestData[] { new GuidStronglyTypedIdSerializationTestData { InstanceTestData = new GuidStronglyTypedId(new Guid("A1234567-abcd-9876-cdef-456789abcdef")), SerializedTestData = "\"A1234567-abcd-9876-cdef-456789abcdef\"" } };
      yield return new GuidStronglyTypedIdSerializationTestData[] { new GuidStronglyTypedIdSerializationTestData { InstanceTestData = new GuidStronglyTypedId(Guid.NewGuid()), SerializedTestData = "" } };
    }

    public IEnumerator<object[]> GetEnumerator() { return StronglyTypedIdSerializationTestData().GetEnumerator(); }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  }

  public class IntStronglyTypedIdSerializationTestData {
    public IntStronglyTypedId InstanceTestData { get; set; }
    public string SerializedTestData { get; set; }

    public IntStronglyTypedIdSerializationTestData() {
    }

    public IntStronglyTypedIdSerializationTestData(IntStronglyTypedId instanceTestData, string serializedTestData) {
      InstanceTestData = instanceTestData;
      SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
    }
  }

  public class IntStronglyTypedIdSerializationTestDataGenerator : IEnumerable<object[]> {
    public static IEnumerable<object[]> StronglyTypedIdSerializationTestData() {
      yield return new IntStronglyTypedIdSerializationTestData[] { new IntStronglyTypedIdSerializationTestData { InstanceTestData = new IntStronglyTypedId(0), SerializedTestData = "0" } };
      yield return new IntStronglyTypedIdSerializationTestData[] { new IntStronglyTypedIdSerializationTestData { InstanceTestData = new IntStronglyTypedId(-1), SerializedTestData = "-1" } };
      yield return new IntStronglyTypedIdSerializationTestData[] { new IntStronglyTypedIdSerializationTestData { InstanceTestData = new IntStronglyTypedId(Int32.MinValue), SerializedTestData = "-2147483648" } };
      yield return new IntStronglyTypedIdSerializationTestData[] { new IntStronglyTypedIdSerializationTestData { InstanceTestData = new IntStronglyTypedId(Int32.MaxValue), SerializedTestData = "2147483647" } };
      yield return new IntStronglyTypedIdSerializationTestData[] { new IntStronglyTypedIdSerializationTestData { InstanceTestData = new IntStronglyTypedId(1234567), SerializedTestData = "1234567" } };
      yield return new IntStronglyTypedIdSerializationTestData[] { new IntStronglyTypedIdSerializationTestData { InstanceTestData = new IntStronglyTypedId(new Random().Next()), SerializedTestData = "" } };
    }

    public IEnumerator<object[]> GetEnumerator() { return StronglyTypedIdSerializationTestData().GetEnumerator(); }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  }
}
