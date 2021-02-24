using System.Collections.Generic;
using System.Collections;
using ATAP.Utilities.Philote;
using System;


namespace ATAP.Utilities.Philote.UnitTests {

  //ToDo add validation tests to ensure illegal values are not allowed.  This applies to all XxTestDataGenerator classes
  public class PhiloteInterfaceSerializationTestData<TValue> {
    public IPhilote<TValue> Philote { get; set; }
    public string SerializedPhilote { get; set; }

    public PhiloteInterfaceSerializationTestData() {
    }

    public PhiloteInterfaceSerializationTestData(IPhilote<TValue> philote, string serializedPhilote) {
      Philote = philote;
      SerializedPhilote = serializedPhilote ?? throw new ArgumentNullException(nameof(serializedPhilote));
    }
  }

  public class PhiloteInterfaceSerializationTestDataGenerator<TValue> : IEnumerable<object[]> {
    public static IEnumerable<object[]> PhiloteSerializationTestData() {
      switch (typeof(TValue)) {
        case Type guidType when typeof(TValue) == typeof(Guid): {
            yield return new PhiloteInterfaceSerializationTestData<TValue>[] { new PhiloteInterfaceSerializationTestData<TValue> { Philote = (ATAP.Utilities.StronglyTypedID.IPhilote<TValue>)new GuidPhilote(Guid.Empty), SerializedPhilote = "\"00000000-0000-0000-0000-000000000000\"" } };
            yield return new PhiloteInterfaceSerializationTestData<TValue>[] { new PhiloteInterfaceSerializationTestData<TValue> { Philote = (ATAP.Utilities.StronglyTypedID.IPhilote<TValue>)new GuidPhilote(new Guid("01234567-abcd-9876-cdef-456789abcdef")), SerializedPhilote = "\"01234567-abcd-9876-cdef-456789abcdef\"" } };
            yield return new PhiloteInterfaceSerializationTestData<TValue>[] { new PhiloteInterfaceSerializationTestData<TValue> { Philote = (ATAP.Utilities.StronglyTypedID.IPhilote<TValue>)new GuidPhilote(Guid.NewGuid()), SerializedPhilote = "" } };
          }
          break;
        case Type intType when typeof(TValue) == typeof(int): {
            yield return new PhiloteInterfaceSerializationTestData<TValue>[] { new PhiloteInterfaceSerializationTestData<TValue> { Philote = (ATAP.Utilities.StronglyTypedID.IPhilote<TValue>)new IntPhilote(0), SerializedPhilote = "0" } };
            yield return new PhiloteInterfaceSerializationTestData<TValue>[] { new PhiloteInterfaceSerializationTestData<TValue> { Philote = (ATAP.Utilities.StronglyTypedID.IPhilote<TValue>)new IntPhilote(1234567), SerializedPhilote = "1234567" } };
            yield return new PhiloteInterfaceSerializationTestData<TValue>[] { new PhiloteInterfaceSerializationTestData<TValue> { Philote = (ATAP.Utilities.StronglyTypedID.IPhilote<TValue>)new IntPhilote(new Random().Next()), SerializedPhilote = "" } };
          }
          break;
        // ToDo: replace with new custom exception and localization of exception message
        default:
          throw new Exception(FormattableString.Invariant($"Invalid TValue type {typeof(TValue)}"));
      }
    }

    public IEnumerator<object[]> GetEnumerator() { return PhiloteSerializationTestData().GetEnumerator(); }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  }

  public class GuidPhiloteSerializationTestData {
    public GuidPhilote Philote { get; set; }
    public string SerializedPhilote { get; set; }

    public GuidPhiloteSerializationTestData() {
    }

    public GuidPhiloteSerializationTestData(GuidPhilote philote, string serializedPhilote) {
      Philote = philote;
      SerializedPhilote = serializedPhilote ?? throw new ArgumentNullException(nameof(serializedPhilote));
    }
  }

  public class GuidPhiloteSerializationTestDataGenerator : IEnumerable<object[]> {
    public static IEnumerable<object[]> PhiloteSerializationTestData() {
      yield return new GuidPhiloteSerializationTestData[] { new GuidPhiloteSerializationTestData { Philote = new GuidPhilote(Guid.Empty), SerializedPhilote = "\"00000000-0000-0000-0000-000000000000\"" } };
      yield return new GuidPhiloteSerializationTestData[] { new GuidPhiloteSerializationTestData { Philote = new GuidPhilote(new Guid("01234567-abcd-9876-cdef-456789abcdef")), SerializedPhilote = "\"01234567-abcd-9876-cdef-456789abcdef\"" } };
      yield return new GuidPhiloteSerializationTestData[] { new GuidPhiloteSerializationTestData { Philote = new GuidPhilote(new Guid("A1234567-abcd-9876-cdef-456789abcdef")), SerializedPhilote = "\"A1234567-abcd-9876-cdef-456789abcdef\"" } };
      yield return new GuidPhiloteSerializationTestData[] { new GuidPhiloteSerializationTestData { Philote = new GuidPhilote(Guid.NewGuid()), SerializedPhilote = "" } };
    }

    public IEnumerator<object[]> GetEnumerator() { return PhiloteSerializationTestData().GetEnumerator(); }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  }

  public class IntPhiloteSerializationTestData {
    public IntPhilote Philote { get; set; }
    public string SerializedPhilote { get; set; }

    public IntPhiloteSerializationTestData() {
    }

    public IntPhiloteSerializationTestData(IntPhilote philote, string serializedPhilote) {
      Philote = philote;
      SerializedPhilote = serializedPhilote ?? throw new ArgumentNullException(nameof(serializedPhilote));
    }
  }

  public class IntPhiloteSerializationTestDataGenerator : IEnumerable<object[]> {
    public static IEnumerable<object[]> PhiloteSerializationTestData() {
      yield return new IntPhiloteSerializationTestData[] { new IntPhiloteSerializationTestData { Philote = new IntPhilote(0), SerializedPhilote = "0" } };
      yield return new IntPhiloteSerializationTestData[] { new IntPhiloteSerializationTestData { Philote = new IntPhilote(1234567), SerializedPhilote = "1234567" } };
      yield return new IntPhiloteSerializationTestData[] { new IntPhiloteSerializationTestData { Philote = new IntPhilote(new Random().Next()), SerializedPhilote = "" } };
    }

    public IEnumerator<object[]> GetEnumerator() { return PhiloteSerializationTestData().GetEnumerator(); }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  }
}
