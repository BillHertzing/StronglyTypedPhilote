using System;
using System.Collections.Generic;
using System.Collections;
using ATAP.Utilities.Philote;
using ATAP.Utilities.StronglyTypedID;
using System.Collections.Concurrent;
using Itenso.TimePeriod;

// Tests:
//   TestClassWithIntPhilote
//   TestClassWithGuidPhilote
//   TestClassWithIntIPhilote
//   TestClassWithGuidIPhilote
//   TestRecordWithIntPhilote
//   TestRecordWithGuidPhilote
//   TestRecordWithIntIPhilote
//   TestRecordWithGuidIPhilote
//   TestClassWithPhilote<TValue>
//   TestRecordWithPhilote<TValue>
//   TestClassWithIPhilote<TValue>
//   TestRecordWithIPhilote<TValue>
//   IntPhilote<T>
//   IntIPhilote<T>
//   GuidPhilote<T>
//   GuidIPhilote<T>
//   Philote<T, TValue>
//   IPhilote<T, TValue>

namespace ATAP.Utilities.Philote.UnitTests {

  public interface IPhiloteTestRecordInt : IAbstractPhilote<TestClassWithIntPhilote, int> {
  }

  public record PhiloteTestRecordInt : AbstractPhilote<TestClassWithIntPhilote, int>, IPhiloteTestRecordInt {
    public PhiloteTestRecordInt(StronglyTypedId<int> iD = default, ConcurrentDictionary<string, IStronglyTypedId<int>>? additionalIDs = default, IEnumerable<ITimeBlock>? timeBlocks = default) : base(iD, additionalIDs, timeBlocks) { }
  }

  public class TestClassWithIntPhilote {
    public TestClassWithIntPhilote(string name = null, PhiloteTestRecordInt philote = null) {
      Name = name ?? throw new ArgumentNullException(nameof(name));
      Philote = philote ?? new PhiloteTestRecordInt();
    }

    public string Name { get; set; }
    public PhiloteTestRecordInt Philote { get; set; }
  }

  public class TestClassWithIntPhiloteSerializationTestData{
    public TestClassWithIntPhilote InstanceTestData { get; set; }
    public string SerializedTestData { get; set; }

    public TestClassWithIntPhiloteSerializationTestData() {
    }

    public TestClassWithIntPhiloteSerializationTestData(TestClassWithIntPhilote instanceTestData, string serializedTestData) {
      InstanceTestData = instanceTestData;
      SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
    }
  }

public class TestClassWithIntPhiloteSerializationTestDataGenerator : IEnumerable<object[]> {
  public static IEnumerable<object[]> TestData() {
    yield return new [] {new TestClassWithIntPhiloteSerializationTestData {InstanceTestData = new TestClassWithIntPhilote(name: "TestClassWithIntPhiloteTestDataInt32Min", philote: new PhiloteTestRecordInt(new IntStronglyTypedId(Int32.MinValue))), SerializedTestData = "{\"Name\":\"TestClassWithIntPhiloteTestDataInt32Min\",\"Philote\":{\"ID\":-2147483648,\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
    yield return new [] {new TestClassWithIntPhiloteSerializationTestData {InstanceTestData = new TestClassWithIntPhilote(name: "TestClassWithIntPhiloteTestDataNegativeOne", philote: new PhiloteTestRecordInt(new IntStronglyTypedId(-1))), SerializedTestData = "{\"Name\":\"TestClassWithIntPhiloteTestDataNegativeOne\",\"Philote\":{\"ID\":-1,\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
    yield return new [] {new TestClassWithIntPhiloteSerializationTestData {InstanceTestData = new TestClassWithIntPhilote(name: "TestClassWithIntPhiloteTestDataZero", philote: new PhiloteTestRecordInt(new IntStronglyTypedId(0))), SerializedTestData = "{\"Name\":\"TestClassWithIntPhiloteTestDataZero\",\"Philote\":{\"ID\":0,\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
    yield return new [] {new TestClassWithIntPhiloteSerializationTestData {InstanceTestData = new TestClassWithIntPhilote(name: "TestClassWithIntPhiloteTestDataPositiveOne", philote: new PhiloteTestRecordInt(new IntStronglyTypedId(1))), SerializedTestData = "{\"Name\":\"TestClassWithIntPhiloteTestDataPositiveOne\",\"Philote\":{\"ID\":1,\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
    yield return new [] {new TestClassWithIntPhiloteSerializationTestData {InstanceTestData = new TestClassWithIntPhilote(name: "TestClassWithIntPhiloteTestDataInt32Max", philote: new PhiloteTestRecordInt(new IntStronglyTypedId(Int32.MaxValue))), SerializedTestData = "{\"Name\":\"TestClassWithIntPhiloteTestDataInt32Max\",\"Philote\":{\"ID\":2147483647,\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
  }

  public IEnumerator<object[]> GetEnumerator() { return TestData().GetEnumerator(); }
  IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
}

  public record PhiloteTestClassGuid : AbstractPhilote<TestClassWithGuidPhilote, Guid> {
    public PhiloteTestClassGuid(StronglyTypedId<Guid> iD = default, ConcurrentDictionary<string, IStronglyTypedId<Guid>>? additionalIDs = default, IEnumerable<ITimeBlock>? timeBlocks = default) : base(iD, additionalIDs, timeBlocks) { }
  }

  public class TestClassWithGuidPhilote {
    public TestClassWithGuidPhilote(string name = null, PhiloteTestClassGuid philote = null) {
      Name = name ?? throw new ArgumentNullException(nameof(name));
      Philote = philote ?? new PhiloteTestClassGuid();
    }

    public string Name { get; set; }
    public PhiloteTestClassGuid Philote { get; set; }
  }

  public class TestClassWithGuidPhiloteSerializationTestData{
    public TestClassWithGuidPhilote InstanceTestData { get; set; }
    public string SerializedTestData { get; set; }

    public TestClassWithGuidPhiloteSerializationTestData() {
    }

    public TestClassWithGuidPhiloteSerializationTestData(TestClassWithGuidPhilote instanceTestData, string serializedTestData) {
      InstanceTestData = instanceTestData;
      SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
    }
  }

public class TestClassWithGuidPhiloteSerializationTestDataGenerator : IEnumerable<object[]> {
  public static IEnumerable<object[]> TestData() {
    yield return new TestClassWithGuidPhiloteSerializationTestData[] {new TestClassWithGuidPhiloteSerializationTestData {InstanceTestData = new TestClassWithGuidPhilote(name: "TestClassWithGuidPhiloteTestDataGuidEmpty", philote: new PhiloteTestClassGuid(new GuidStronglyTypedId(Guid.Empty))), SerializedTestData = "{\"Name\":\"TestClassWithGuidPhiloteTestDataGuidEmpty\",\"Philote\":{\"ID\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
    yield return new TestClassWithGuidPhiloteSerializationTestData[] {new TestClassWithGuidPhiloteSerializationTestData {InstanceTestData = new TestClassWithGuidPhilote(name: "TestClassWithGuidPhiloteTestDataGuidEmpty", philote: new PhiloteTestClassGuid(new GuidStronglyTypedId(new Guid("01234567-abcd-9876-cdef-456789abcdef")))), SerializedTestData = "{\"Name\":\"TestClassWithGuidPhiloteTestDataGuidEmpty\",\"Philote\":{\"ID\":\"01234567-abcd-9876-cdef-456789abcdef\",\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
  }

  public IEnumerator<object[]> GetEnumerator() { return TestData().GetEnumerator(); }
  IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
}

public class TestClassWithIntIPhilote {
  public TestClassWithIntIPhilote(string name = null, IPhiloteTestRecordInt philote = null) {
    Name = name ?? throw new ArgumentNullException(nameof(name));
    Philote = philote ?? (IPhiloteTestRecordInt) new PhiloteTestRecordInt();
  }

  public string Name { get; set; }
  public IPhiloteTestRecordInt Philote { get; set; }
}

public class TestClassWithIntIPhiloteSerializationTestData{
  public TestClassWithIntIPhilote InstanceTestData { get; set; }
  public string SerializedTestData { get; set; }

  public TestClassWithIntIPhiloteSerializationTestData() {
  }

  public TestClassWithIntIPhiloteSerializationTestData(TestClassWithIntIPhilote instanceTestData, string serializedTestData) {
    InstanceTestData = instanceTestData;
    SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
  }
}

public class TestClassWithIntIPhiloteSerializationTestDataGenerator : IEnumerable<object[]> {
  public static IEnumerable<object[]> TestData() {
    yield return new TestClassWithIntIPhiloteSerializationTestData[] {new TestClassWithIntIPhiloteSerializationTestData {InstanceTestData = new TestClassWithIntIPhilote(name: "TestClassWithIntIPhiloteTestDataInt32Min", philote:  new PhiloteTestRecordInt(new IntStronglyTypedId(Int32.MinValue))), SerializedTestData = "{\"Name\":\"TestClassWithIntIPhiloteTestDataInt32Min\",\"Philote\":{\"ID\":-2147483648,\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
    yield return new TestClassWithIntIPhiloteSerializationTestData[] {new TestClassWithIntIPhiloteSerializationTestData {InstanceTestData = new TestClassWithIntIPhilote(name: "TestClassWithIntIPhiloteTestDataNegativeOne", philote: new PhiloteTestRecordInt(new IntStronglyTypedId(-1))), SerializedTestData = "{\"Name\":\"TestClassWithIntIPhiloteTestDataNegativeOne\",\"Philote\":{\"ID\":-1,\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
    yield return new TestClassWithIntIPhiloteSerializationTestData[] {new TestClassWithIntIPhiloteSerializationTestData {InstanceTestData = new TestClassWithIntIPhilote(name: "TestClassWithIntIPhiloteTestDataZero", philote: new PhiloteTestRecordInt(new IntStronglyTypedId(0))), SerializedTestData = "{\"Name\":\"TestClassWithIntIPhiloteTestDataZero\",\"Philote\":{\"ID\":0,\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
    yield return new TestClassWithIntIPhiloteSerializationTestData[] {new TestClassWithIntIPhiloteSerializationTestData {InstanceTestData = new TestClassWithIntIPhilote(name: "TestClassWithIntIPhiloteTestDataPositiveOne", philote: new PhiloteTestRecordInt(new IntStronglyTypedId(1))), SerializedTestData = "{\"Name\":\"TestClassWithIntIPhiloteTestDataPositiveOne\",\"Philote\":{\"ID\":1,\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
    yield return new TestClassWithIntIPhiloteSerializationTestData[] {new TestClassWithIntIPhiloteSerializationTestData {InstanceTestData = new TestClassWithIntIPhilote(name: "TestClassWithIntIPhiloteTestDataInt32Max", philote: new PhiloteTestRecordInt(new IntStronglyTypedId(Int32.MaxValue))), SerializedTestData = "{\"Name\":\"TestClassWithIntIPhiloteTestDataInt32Max\",\"Philote\":{\"ID\":2147483647,\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
  }

  public IEnumerator<object[]> GetEnumerator() { return TestData().GetEnumerator(); }
  IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
}

  //ToDo add validation tests to ensure illegal values are not allowed.  This applies to all XxTestDataGenerator classes
  public abstract class AbstractPhiloteInterfaceSerializationTestData<T, TValue> where T : class where TValue : notnull {
    public virtual IAbstractPhilote<T, TValue> InstanceTestData { get; set; }
    public string SerializedTestData { get; set; }

    public AbstractPhiloteInterfaceSerializationTestData() {
    }

    public AbstractPhiloteInterfaceSerializationTestData(IAbstractPhilote<T, TValue> instanceTestData, string serializedTestData) {
      InstanceTestData = instanceTestData;
      SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
    }
  }

  public record PT1Guid : AbstractPhilote<PT1Guid, Guid> {
    public PT1Guid(IStronglyTypedId<Guid> iD = default,
      ConcurrentDictionary<string, IStronglyTypedId<Guid>>? additionalIDs = default,
      IEnumerable<ITimeBlock>? timeBlocks = default) : base(iD, additionalIDs, timeBlocks) {
    }

    public AbstractPhilote<PT1Guid, Guid> Philote { get; set; }
  }
  public record PT1Int : AbstractPhilote<PT1Int, int> {
    public PT1Int(IStronglyTypedId<int> iD = default,
      ConcurrentDictionary<string, IStronglyTypedId<int>>? additionalIDs = default,
      IEnumerable<ITimeBlock>? timeBlocks = default) : base(iD, additionalIDs, timeBlocks) {
    }

    public AbstractPhilote<PT1Int, int> Philote { get; set; }
  }
  public abstract class AbstractPhiloteSerializationTestData<T, TValue> where T : class where TValue : notnull {
    public virtual AbstractPhilote<T, TValue> InstanceTestData { get; set; }
    public string SerializedTestData { get; set; }

    public AbstractPhiloteSerializationTestData() {
    }

    public AbstractPhiloteSerializationTestData(AbstractPhilote<T, TValue> instanceTestData, string serializedTestData) {
      InstanceTestData = instanceTestData;
      SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
    }
  }
  public class PT1IntSerializationTestData : AbstractPhiloteSerializationTestData<PT1Int, int> {

  }

  public class PT1IntSerializationTestDataGenerator : IEnumerable<object[]> {
    public static IEnumerable<object[]> TestData() {
      yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(0)), SerializedTestData = "{\"ID\":0,\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
      yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(-1)), SerializedTestData = "{\"ID\":-1,\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
      yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(Int32.MinValue)), SerializedTestData = "{\"ID\":-2147483648,\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
      yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(Int32.MaxValue)), SerializedTestData = "{\"ID\":2147483647,\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
      yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(1234567)), SerializedTestData = "{\"ID\":1234567,\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
      yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(new Random().Next())), SerializedTestData = "" } };


      // yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(0), null, null), SerializedTestData = "{\"ID\":0,\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
      // yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(0), null, null), SerializedTestData = "{\"ID\":0,\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
      // yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(new Guid("01234567-abcd-9876-cdef-456789abcdef")), null, null), SerializedTestData = "{\"ID\":\"01234567-abcd-9876-cdef-456789abcdef\",\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
      // yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(Guid.NewGuid()), null, null), SerializedTestData = "" } };
      // yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IStronglyTypedId<Guid>>(), null), SerializedTestData = "{\"ID\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
      // yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(new Guid("01234567-abcd-9876-cdef-456789abcdef")), new ConcurrentDictionary<string, IStronglyTypedId<Guid>>(), null), SerializedTestData = "{\"ID\":\"01234567-abcd-9876-cdef-456789abcdef\",\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
      // yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(Guid.NewGuid()), new ConcurrentDictionary<string, IStronglyTypedId<Guid>>(), null), SerializedTestData = "" } };
      // yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IStronglyTypedId<Guid>>(new List<KeyValuePair<string, IStronglyTypedId<Guid>>>() { new KeyValuePair<string, IStronglyTypedId<Guid>>("key1", new GuidStronglyTypedId(Guid.Empty)) }), null), SerializedTestData = "{\"ID\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIDs\":{\"key1\":\"00000000-0000-0000-0000-000000000000\"},\"TimeBlocks\":[]}" } };
      // yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IStronglyTypedId<Guid>>(new List<KeyValuePair<string, IStronglyTypedId<Guid>>>() { new KeyValuePair<string, IStronglyTypedId<Guid>>("key1", new GuidStronglyTypedId(Guid.Empty)) }), null), SerializedTestData = "{\"ID\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIDs\":{\"key1\":\"00000000-0000-0000-0000-000000000000\"},\"TimeBlocks\":[]}" } };
      // yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IStronglyTypedId<Guid>>(new List<KeyValuePair<string, IStronglyTypedId<Guid>>>() { new KeyValuePair<string, IStronglyTypedId<Guid>>("key1", new GuidStronglyTypedId(Guid.Empty)) }), null), SerializedTestData = "{\"ID\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIDs\":{\"key1\":\"00000000-0000-0000-0000-000000000000\"},\"TimeBlocks\":[]}" } };
    }

    public IEnumerator<object[]> GetEnumerator() { return TestData().GetEnumerator(); }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  }

  public class TestClassWithPhilote<TValue> where TValue:notnull  {
    public TestClassWithPhilote(AbstractPhilote<TestClassWithPhilote<TValue>, TValue> philote = default) {
      Philote = philote ?? throw new ArgumentNullException(nameof(philote));
    }

    public string Name { get; set; }
    public AbstractPhilote<TestClassWithPhilote<TValue>, TValue> Philote { get; init; }
  }
  public class TestClassWithPhiloteSerializationTestData<TValue> where TValue : notnull {
    public TestClassWithPhiloteSerializationTestData(TestClassWithPhilote<TValue> instanceTestData = null,
      string serializedTestData = null) {
      InstanceTestData = instanceTestData ?? throw new ArgumentNullException(nameof(instanceTestData));
      SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
    }

    public TestClassWithPhilote<TValue> InstanceTestData { get; set; }
    public string SerializedTestData { get; set; }


  }
  public class TestClass { }
  public record TestClassGuidPhilote : AbstractPhilote<TestClass, Guid> {
    // public TestClassGuidPhilote(AbstractPhilote<TestClass, Guid> original) : base(original) {
    // }

    public TestClassGuidPhilote(IStronglyTypedId<Guid> iD = null, ConcurrentDictionary<string, IStronglyTypedId<Guid>>? additionalIDs = null, IEnumerable<ITimeBlock>? timeBlocks = null) : base(iD, additionalIDs, timeBlocks) {
    }
  }

  public class TestClassGuidPhiloteInterfaceSerializationTestData : AbstractPhiloteInterfaceSerializationTestData<TestClass, Guid> {
  }

  public class TestClassGuidPhiloteInterfaceSerializationTestDataGenerator : IEnumerable<object[]> {
    public static IEnumerable<object[]> TestData() {
      yield return new TestClassGuidPhiloteInterfaceSerializationTestData[] { new TestClassGuidPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(Guid.Empty), null, null), SerializedTestData = "{\"ID\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
      yield return new TestClassGuidPhiloteInterfaceSerializationTestData[] { new TestClassGuidPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(new Guid("01234567-abcd-9876-cdef-456789abcdef")), null, null), SerializedTestData = "{\"ID\":\"01234567-abcd-9876-cdef-456789abcdef\",\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
      yield return new TestClassGuidPhiloteInterfaceSerializationTestData[] { new TestClassGuidPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(Guid.NewGuid()), null, null), SerializedTestData = "" } };
      yield return new TestClassGuidPhiloteInterfaceSerializationTestData[] { new TestClassGuidPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IStronglyTypedId<Guid>>(), null), SerializedTestData = "{\"ID\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
      yield return new TestClassGuidPhiloteInterfaceSerializationTestData[] { new TestClassGuidPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(new Guid("01234567-abcd-9876-cdef-456789abcdef")), new ConcurrentDictionary<string, IStronglyTypedId<Guid>>(), null), SerializedTestData = "{\"ID\":\"01234567-abcd-9876-cdef-456789abcdef\",\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
      yield return new TestClassGuidPhiloteInterfaceSerializationTestData[] { new TestClassGuidPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(Guid.NewGuid()), new ConcurrentDictionary<string, IStronglyTypedId<Guid>>(), null), SerializedTestData = "" } };
      yield return new TestClassGuidPhiloteInterfaceSerializationTestData[] { new TestClassGuidPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IStronglyTypedId<Guid>>(new List<KeyValuePair<string, IStronglyTypedId<Guid>>>() { new KeyValuePair<string, IStronglyTypedId<Guid>>("key1", new GuidStronglyTypedId(Guid.Empty)) }), null), SerializedTestData = "{\"ID\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIDs\":{\"key1\":\"00000000-0000-0000-0000-000000000000\"},\"TimeBlocks\":[]}" } };
      yield return new TestClassGuidPhiloteInterfaceSerializationTestData[] { new TestClassGuidPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IStronglyTypedId<Guid>>(new List<KeyValuePair<string, IStronglyTypedId<Guid>>>() { new KeyValuePair<string, IStronglyTypedId<Guid>>("key1", new GuidStronglyTypedId(Guid.Empty)) }), null), SerializedTestData = "{\"ID\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIDs\":{\"key1\":\"00000000-0000-0000-0000-000000000000\"},\"TimeBlocks\":[]}" } };
      yield return new TestClassGuidPhiloteInterfaceSerializationTestData[] { new TestClassGuidPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IStronglyTypedId<Guid>>(new List<KeyValuePair<string, IStronglyTypedId<Guid>>>() { new KeyValuePair<string, IStronglyTypedId<Guid>>("key1", new GuidStronglyTypedId(Guid.Empty)) }), null), SerializedTestData = "{\"ID\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIDs\":{\"key1\":\"00000000-0000-0000-0000-000000000000\"},\"TimeBlocks\":[]}" } };
    }

    public IEnumerator<object[]> GetEnumerator() { return TestData().GetEnumerator(); }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  }

  public record TestClassIntPhilote : AbstractPhilote<TestClass, int> {
    // public TestClassIntPhilote(AbstractPhilote<TestClass, int> original) : base(original) {
    // }

    public TestClassIntPhilote(IStronglyTypedId<int> iD = null, ConcurrentDictionary<string, IStronglyTypedId<int>>? additionalIDs = null, IEnumerable<ITimeBlock>? timeBlocks = null) : base(iD, additionalIDs, timeBlocks) {
    }
  }

  public class TestClassIntPhiloteInterfaceSerializationTestData : AbstractPhiloteInterfaceSerializationTestData<TestClass, int> {
  }

  public class TestClassIntPhiloteInterfaceSerializationTestDataGenerator : IEnumerable<object[]> {
    public static IEnumerable<object[]> TestData() {
      yield return new TestClassIntPhiloteInterfaceSerializationTestData[] { new TestClassIntPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, int>)new TestClassIntPhilote(new IntStronglyTypedId(0), null, null), SerializedTestData = "{\"ID\":0,\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
      yield return new TestClassIntPhiloteInterfaceSerializationTestData[] { new TestClassIntPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, int>)new TestClassIntPhilote(new IntStronglyTypedId(1234567), null, null), SerializedTestData = "{\"ID\":1234567,\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
      yield return new TestClassIntPhiloteInterfaceSerializationTestData[] { new TestClassIntPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, int>)new TestClassIntPhilote(new IntStronglyTypedId(new Random().Next()), null, null), SerializedTestData = "" } };
    }

    public IEnumerator<object[]> GetEnumerator() { return TestData().GetEnumerator(); }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  }
  //ResourceManager rm = new ResourceManager("ATAP.Utilities.Philote.UnitTests.SerializationStrings", typeof(SerializationStrings).Assembly);
      //yield return new PhiloteTestData<T>[] {new PhiloteTestData<T>(
      //        //new Philote<T>() ,
      //         DefaultConfiguration<T>.Production["Generic"],
      //         Regex.Escape(rm.GetString("SerializedPhilotePart1"))+
      //        "00000000-0000-0000-0000-000000000000"+
      //        Regex.Escape(rm.GetString("SerializedPhilotePart2"))) };
     // yield return new PhiloteTestData<T>[] {new PhiloteTestData<T>(
     //         // new Philote<T>(new IdAsStruct<T>(new Guid("01234567-abcd-9876-cdef-456789abcdef")),new Dictionary<string, IIdAsStruct<T>>(), new List<ITimeBlock>() ) ,
     //         DefaultConfiguration<T>.Production["Contrived"],
     //         Regex.Escape(rm.GetString("SerializedPhilotePart1"))+"01234567-abcd-9876-cdef-456789abcdef" + Regex.Escape(rm.GetString("SerializedPhilotePart2")) ) };
     // yield return new PhiloteTestData<T>[] {new PhiloteTestData<T>(
     //         new Philote<T>(new IdAsStruct<T>(new Guid("01234567-abcd-9876-cdef-456789abcdef"))).Now() ,
    //          Regex.Escape(rm.GetString("SerializedPhilotePart1"))+"[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}" + Regex.Escape(rm.GetString("SerializedPhilotePart2")) ) };
   // }
    //public IEnumerator<object[]> GetEnumerator() { return TestData().GetEnumerator(); }
   // IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
 // }

}
