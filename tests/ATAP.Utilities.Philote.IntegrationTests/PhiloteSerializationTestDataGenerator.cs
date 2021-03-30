using System;
using System.Collections.Generic;
using System.Collections;
using ATAP.Utilities.Philote;
using ATAP.Utilities.StronglyTypedIds;
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

namespace ATAP.Utilities.Philote.IntegrationTests {

  public class GCommentWithIntSerializationTestData {
    public GComment<int> InstanceTestData { get; set; }
    public string SerializedTestData { get; set; }

    public GCommentWithIntSerializationTestData() {
    }

    public GCommentWithIntSerializationTestData(GComment<int> instanceTestData, string serializedTestData) {
      InstanceTestData = instanceTestData;
      SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
    }
  }

  public class GCommentWithIntSerializationTestDataGenerator : IEnumerable<object[]> {
    public static IEnumerable<object[]> TestData() {
      yield return new GCommentWithIntSerializationTestData[] { new GCommentWithIntSerializationTestData { InstanceTestData = new GComment<int>(gStatements: new List<string>() { "GCommentWithIntPhiloteTestDataInt32Min" }, philote: new GCommentPhilote<int>(iD: new GCommentId<int>(Int32.MinValue))), SerializedTestData = "{\"GStatements\":[\"GCommentWithIntPhiloteTestDataInt32Min\"],\"Philote\":{\"ID\":-2147483648,\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
      yield return new GCommentWithIntSerializationTestData[] { new GCommentWithIntSerializationTestData { InstanceTestData = new GComment<int>(gStatements: new List<string>() { "GCommentWithIntPhiloteTestDataNegativeOne" }, philote: new GCommentPhilote<int>(iD: new GCommentId<int>(-1))), SerializedTestData = "{\"GStatements\":[\"GCommentWithIntPhiloteTestDataNegativeOne\"],\"Philote\":{\"ID\":-1,\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
      yield return new GCommentWithIntSerializationTestData[] { new GCommentWithIntSerializationTestData { InstanceTestData = new GComment<int>(gStatements: new List<string>() { "GCommentWithIntPhiloteTestDataZero" }, philote: new GCommentPhilote<int>(iD: new GCommentId<int>(0))), SerializedTestData = "{\"GStatements\":[\"GCommentWithIntPhiloteTestDataZero\"],\"Philote\":{\"ID\":0,\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
      yield return new GCommentWithIntSerializationTestData[] { new GCommentWithIntSerializationTestData { InstanceTestData = new GComment<int>(gStatements: new List<string>() { "GCommentWithIntPhiloteTestDataPositiveOne" }, philote: new GCommentPhilote<int>(iD: new GCommentId<int>(1))), SerializedTestData = "{\"GStatements\":[\"GCommentWithIntPhiloteTestDataPositiveOne\"],\"Philote\":{\"ID\":1,\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
      yield return new GCommentWithIntSerializationTestData[] { new GCommentWithIntSerializationTestData { InstanceTestData = new GComment<int>(gStatements: new List<string>() { "GCommentWithIntPhiloteTestDataInt32Max" }, philote: new GCommentPhilote<int>(iD: new GCommentId<int>(Int32.MaxValue))), SerializedTestData = "{\"GStatements\":[\"GCommentWithIntPhiloteTestDataInt32Max\"],\"Philote\":{\"ID\":2147483647,\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
    }

    public IEnumerator<object[]> GetEnumerator() { return TestData().GetEnumerator(); }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  }

  public class GCommentWithGuidSerializationTestData {
    public GComment<Guid> InstanceTestData { get; set; }
    public string SerializedTestData { get; set; }

    public GCommentWithGuidSerializationTestData() {
    }

    public GCommentWithGuidSerializationTestData(GComment<Guid> instanceTestData, string serializedTestData) {
      InstanceTestData = instanceTestData;
      SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
    }
  }

  public class GCommentWithGuidSerializationTestDataGenerator : IEnumerable<object[]> {
    public static IEnumerable<object[]> TestData() {
      yield return new GCommentWithGuidSerializationTestData[] { new GCommentWithGuidSerializationTestData { InstanceTestData = new GComment<Guid>(gStatements: new List<string>() { "GCommentWithGuidPhiloteTestDataGuidEmpty" }, philote: new GCommentPhilote<Guid>(iD: new GCommentId<Guid>(Guid.Empty))), SerializedTestData = "{\"GStatements\":[\"GCommentWithGuidPhiloteTestDataEmpty\"],\"Philote\":{\"ID\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
      yield return new GCommentWithGuidSerializationTestData[] { new GCommentWithGuidSerializationTestData { InstanceTestData = new GComment<Guid>(gStatements: new List<string>() { "GCommentWithGuidPhiloteTestDataGuidFixed" }, philote: new GCommentPhilote<Guid>(iD: new GCommentId<Guid>(new Guid("01234567-abcd-9876-cdef-456789abcdef")))), SerializedTestData = "{\"GStatements\":[\"GCommentWithGuidPhiloteTestDataFixed\"],\"Philote\":{\"ID\":\"01234567-abcd-9876-cdef-456789abcdef\",\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
    }

    public IEnumerator<object[]> GetEnumerator() { return TestData().GetEnumerator(); }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  }

  //   public record PhiloteTestClassGuid : AbstractPhilote<TestClassWithGuidPhilote, Guid> {
  //     public PhiloteTestClassGuid(AbstractStronglyTypedId<Guid> iD = default, ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>? additionalIDs = default, IEnumerable<ITimeBlock>? timeBlocks = default) : base(iD, additionalIDs, timeBlocks) { }
  //   }
  //
  //   public class TestClassWithGuidPhilote {
  //     public TestClassWithGuidPhilote(string name = null, PhiloteTestClassGuid philote = null) {
  //       Name = name ?? throw new ArgumentNullException(nameof(name));
  //       Philote = philote ?? new PhiloteTestClassGuid();
  //     }
  //
  //     public string Name { get; set; }
  //     public PhiloteTestClassGuid Philote { get; set; }
  //   }
  //
  //   public class TestClassWithGuidPhiloteSerializationTestData{
  //     public TestClassWithGuidPhilote InstanceTestData { get; set; }
  //     public string SerializedTestData { get; set; }
  //
  //     public TestClassWithGuidPhiloteSerializationTestData() {
  //     }
  //
  //     public TestClassWithGuidPhiloteSerializationTestData(TestClassWithGuidPhilote instanceTestData, string serializedTestData) {
  //       InstanceTestData = instanceTestData;
  //       SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
  //     }
  //   }
  //
  // public class TestClassWithGuidPhiloteSerializationTestDataGenerator : IEnumerable<object[]> {
  //   public static IEnumerable<object[]> TestData() {
  //     yield return new TestClassWithGuidPhiloteSerializationTestData[] {new TestClassWithGuidPhiloteSerializationTestData {InstanceTestData = new TestClassWithGuidPhilote(name: "TestClassWithGuidPhiloteTestDataGuidEmpty", philote: new PhiloteTestClassGuid(new GuidStronglyTypedId(Guid.Empty))), SerializedTestData = "{\"Name\":\"TestClassWithGuidPhiloteTestDataGuidEmpty\",\"Philote\":{\"ID\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
  //     yield return new TestClassWithGuidPhiloteSerializationTestData[] {new TestClassWithGuidPhiloteSerializationTestData {InstanceTestData = new TestClassWithGuidPhilote(name: "TestClassWithGuidPhiloteTestDataGuidEmpty", philote: new PhiloteTestClassGuid(new GuidStronglyTypedId(new Guid("01234567-abcd-9876-cdef-456789abcdef")))), SerializedTestData = "{\"Name\":\"TestClassWithGuidPhiloteTestDataGuidEmpty\",\"Philote\":{\"ID\":\"01234567-abcd-9876-cdef-456789abcdef\",\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
  //   }
  //
  //   public IEnumerator<object[]> GetEnumerator() { return TestData().GetEnumerator(); }
  //   IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  // }
  //
  // public class TestClassWithIntIPhilote {
  //   public TestClassWithIntIPhilote(string name = null, IPhiloteTestRecordInt philote = null) {
  //     Name = name ?? throw new ArgumentNullException(nameof(name));
  //     Philote = philote ?? (IPhiloteTestRecordInt) new PhiloteTestRecordInt();
  //   }
  //
  //   public string Name { get; set; }
  //   public IPhiloteTestRecordInt Philote { get; set; }
  // }
  //
  // public class TestClassWithIntIPhiloteSerializationTestData{
  //   public TestClassWithIntIPhilote InstanceTestData { get; set; }
  //   public string SerializedTestData { get; set; }
  //
  //   public TestClassWithIntIPhiloteSerializationTestData() {
  //   }
  //
  //   public TestClassWithIntIPhiloteSerializationTestData(TestClassWithIntIPhilote instanceTestData, string serializedTestData) {
  //     InstanceTestData = instanceTestData;
  //     SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
  //   }
  // }
  //
  // public class TestClassWithIntIPhiloteSerializationTestDataGenerator : IEnumerable<object[]> {
  //   public static IEnumerable<object[]> TestData() {
  //     yield return new TestClassWithIntIPhiloteSerializationTestData[] {new TestClassWithIntIPhiloteSerializationTestData {InstanceTestData = new TestClassWithIntIPhilote(name: "TestClassWithIntIPhiloteTestDataInt32Min", philote:  new PhiloteTestRecordInt(new IntStronglyTypedId(Int32.MinValue))), SerializedTestData = "{\"Name\":\"TestClassWithIntIPhiloteTestDataInt32Min\",\"Philote\":{\"ID\":-2147483648,\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
  //     yield return new TestClassWithIntIPhiloteSerializationTestData[] {new TestClassWithIntIPhiloteSerializationTestData {InstanceTestData = new TestClassWithIntIPhilote(name: "TestClassWithIntIPhiloteTestDataNegativeOne", philote: new PhiloteTestRecordInt(new IntStronglyTypedId(-1))), SerializedTestData = "{\"Name\":\"TestClassWithIntIPhiloteTestDataNegativeOne\",\"Philote\":{\"ID\":-1,\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
  //     yield return new TestClassWithIntIPhiloteSerializationTestData[] {new TestClassWithIntIPhiloteSerializationTestData {InstanceTestData = new TestClassWithIntIPhilote(name: "TestClassWithIntIPhiloteTestDataZero", philote: new PhiloteTestRecordInt(new IntStronglyTypedId(0))), SerializedTestData = "{\"Name\":\"TestClassWithIntIPhiloteTestDataZero\",\"Philote\":{\"ID\":0,\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
  //     yield return new TestClassWithIntIPhiloteSerializationTestData[] {new TestClassWithIntIPhiloteSerializationTestData {InstanceTestData = new TestClassWithIntIPhilote(name: "TestClassWithIntIPhiloteTestDataPositiveOne", philote: new PhiloteTestRecordInt(new IntStronglyTypedId(1))), SerializedTestData = "{\"Name\":\"TestClassWithIntIPhiloteTestDataPositiveOne\",\"Philote\":{\"ID\":1,\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
  //     yield return new TestClassWithIntIPhiloteSerializationTestData[] {new TestClassWithIntIPhiloteSerializationTestData {InstanceTestData = new TestClassWithIntIPhilote(name: "TestClassWithIntIPhiloteTestDataInt32Max", philote: new PhiloteTestRecordInt(new IntStronglyTypedId(Int32.MaxValue))), SerializedTestData = "{\"Name\":\"TestClassWithIntIPhiloteTestDataInt32Max\",\"Philote\":{\"ID\":2147483647,\"AdditionalIDs\":{},\"TimeBlocks\":[]}}" } };
  //   }
  //
  //   public IEnumerator<object[]> GetEnumerator() { return TestData().GetEnumerator(); }
  //   IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  // }
  //
  //   //ToDo add validation tests to ensure illegal values are not allowed.  This applies to all XxTestDataGenerator classes
  //   public abstract class AbstractPhiloteInterfaceSerializationTestData<T, TValue> where T : class where TValue : notnull {
  //     public virtual IAbstractPhilote<T, TValue> InstanceTestData { get; set; }
  //     public string SerializedTestData { get; set; }
  //
  //     public AbstractPhiloteInterfaceSerializationTestData() {
  //     }
  //
  //     public AbstractPhiloteInterfaceSerializationTestData(IAbstractPhilote<T, TValue> instanceTestData, string serializedTestData) {
  //       InstanceTestData = instanceTestData;
  //       SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
  //     }
  //   }
  //
  //   public record PT1Guid : AbstractPhilote<PT1Guid, Guid> {
  //     public PT1Guid(IAbstractStronglyTypedId<Guid> iD = default,
  //       ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>? additionalIDs = default,
  //       IEnumerable<ITimeBlock>? timeBlocks = default) : base(iD, additionalIDs, timeBlocks) {
  //     }
  //
  //     public AbstractPhilote<PT1Guid, Guid> Philote { get; set; }
  //   }
  //   public record PT1Int : AbstractPhilote<PT1Int, int> {
  //     public PT1Int(IAbstractStronglyTypedId<int> iD = default,
  //       ConcurrentDictionary<string, IAbstractStronglyTypedId<int>>? additionalIDs = default,
  //       IEnumerable<ITimeBlock>? timeBlocks = default) : base(iD, additionalIDs, timeBlocks) {
  //     }
  //
  //     public AbstractPhilote<PT1Int, int> Philote { get; set; }
  //   }
  //   public abstract class AbstractPhiloteSerializationTestData<T, TValue> where T : class where TValue : notnull {
  //     public virtual AbstractPhilote<T, TValue> InstanceTestData { get; set; }
  //     public string SerializedTestData { get; set; }
  //
  //     public AbstractPhiloteSerializationTestData() {
  //     }
  //
  //     public AbstractPhiloteSerializationTestData(AbstractPhilote<T, TValue> instanceTestData, string serializedTestData) {
  //       InstanceTestData = instanceTestData;
  //       SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
  //     }
  //   }
  //   public class PT1IntSerializationTestData : AbstractPhiloteSerializationTestData<PT1Int, int> {
  //
  //   }
  //
  //   public class PT1IntSerializationTestDataGenerator : IEnumerable<object[]> {
  //     public static IEnumerable<object[]> TestData() {
  //       yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(0)), SerializedTestData = "{\"ID\":0,\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
  //       yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(-1)), SerializedTestData = "{\"ID\":-1,\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
  //       yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(Int32.MinValue)), SerializedTestData = "{\"ID\":-2147483648,\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
  //       yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(Int32.MaxValue)), SerializedTestData = "{\"ID\":2147483647,\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
  //       yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(1234567)), SerializedTestData = "{\"ID\":1234567,\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
  //       yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(new Random().Next())), SerializedTestData = "" } };
  //
  //
  //       // yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(0), null, null), SerializedTestData = "{\"ID\":0,\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
  //       // yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(0), null, null), SerializedTestData = "{\"ID\":0,\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
  //       // yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(new Guid("01234567-abcd-9876-cdef-456789abcdef")), null, null), SerializedTestData = "{\"ID\":\"01234567-abcd-9876-cdef-456789abcdef\",\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
  //       // yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(Guid.NewGuid()), null, null), SerializedTestData = "" } };
  //       // yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(), null), SerializedTestData = "{\"ID\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
  //       // yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(new Guid("01234567-abcd-9876-cdef-456789abcdef")), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(), null), SerializedTestData = "{\"ID\":\"01234567-abcd-9876-cdef-456789abcdef\",\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
  //       // yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(Guid.NewGuid()), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(), null), SerializedTestData = "" } };
  //       // yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(new List<KeyValuePair<string, IAbstractStronglyTypedId<Guid>>>() { new KeyValuePair<string, IAbstractStronglyTypedId<Guid>>("key1", new GuidStronglyTypedId(Guid.Empty)) }), null), SerializedTestData = "{\"ID\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIDs\":{\"key1\":\"00000000-0000-0000-0000-000000000000\"},\"TimeBlocks\":[]}" } };
  //       // yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(new List<KeyValuePair<string, IAbstractStronglyTypedId<Guid>>>() { new KeyValuePair<string, IAbstractStronglyTypedId<Guid>>("key1", new GuidStronglyTypedId(Guid.Empty)) }), null), SerializedTestData = "{\"ID\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIDs\":{\"key1\":\"00000000-0000-0000-0000-000000000000\"},\"TimeBlocks\":[]}" } };
  //       // yield return new PT1IntSerializationTestData[] { new PT1IntSerializationTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(new List<KeyValuePair<string, IAbstractStronglyTypedId<Guid>>>() { new KeyValuePair<string, IAbstractStronglyTypedId<Guid>>("key1", new GuidStronglyTypedId(Guid.Empty)) }), null), SerializedTestData = "{\"ID\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIDs\":{\"key1\":\"00000000-0000-0000-0000-000000000000\"},\"TimeBlocks\":[]}" } };
  //     }
  //
  //     public IEnumerator<object[]> GetEnumerator() { return TestData().GetEnumerator(); }
  //     IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  //   }
  //
  //   public class TestClassWithPhilote<TValue> where TValue : notnull {
  //     public TestClassWithPhilote(AbstractPhilote<TestClassWithPhilote<TValue>, TValue>  philote = default) {
  //       Philote = philote ?? throw new ArgumentNullException(nameof(philote));
  //     }
  //
  //     public string Name { get; set; }
  //     public AbstractPhilote<TestClassWithPhilote<TValue>, TValue> Philote { get; init; }
  //   }
  //   public class TestClassWithPhiloteSerializationTestData<TValue>  {
  //     public TestClassWithPhiloteSerializationTestData(TestClassWithPhilote<TValue> instanceTestData = null,
  //       string serializedTestData = null) {
  //       InstanceTestData = instanceTestData ?? throw new ArgumentNullException(nameof(instanceTestData));
  //       SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
  //     }
  //
  //     public TestClassWithPhilote<TValue> InstanceTestData { get; set; }
  //     public string SerializedTestData { get; set; }
  //
  //
  //   }
  //   public class TestClass { }
  //   public record TestClassGuidPhilote : AbstractPhilote<TestClass, Guid> {
  //     // public TestClassGuidPhilote(AbstractPhilote<TestClass, Guid> original) : base(original) {
  //     // }
  //
  //     public TestClassGuidPhilote(IAbstractStronglyTypedId<Guid> iD = null, ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>? additionalIDs = null, IEnumerable<ITimeBlock>? timeBlocks = null) : base(iD, additionalIDs, timeBlocks) {
  //     }
  //   }
  //
  //   public class TestClassGuidPhiloteInterfaceSerializationTestData : AbstractPhiloteInterfaceSerializationTestData<TestClass, Guid> {
  //   }
  //
  //   public class TestClassGuidPhiloteInterfaceSerializationTestDataGenerator : IEnumerable<object[]> {
  //     public static IEnumerable<object[]> TestData() {
  //       yield return new TestClassGuidPhiloteInterfaceSerializationTestData[] { new TestClassGuidPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(Guid.Empty), null, null), SerializedTestData = "{\"ID\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
  //       yield return new TestClassGuidPhiloteInterfaceSerializationTestData[] { new TestClassGuidPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(new Guid("01234567-abcd-9876-cdef-456789abcdef")), null, null), SerializedTestData = "{\"ID\":\"01234567-abcd-9876-cdef-456789abcdef\",\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
  //       yield return new TestClassGuidPhiloteInterfaceSerializationTestData[] { new TestClassGuidPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(Guid.NewGuid()), null, null), SerializedTestData = "" } };
  //       yield return new TestClassGuidPhiloteInterfaceSerializationTestData[] { new TestClassGuidPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(), null), SerializedTestData = "{\"ID\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
  //       yield return new TestClassGuidPhiloteInterfaceSerializationTestData[] { new TestClassGuidPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(new Guid("01234567-abcd-9876-cdef-456789abcdef")), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(), null), SerializedTestData = "{\"ID\":\"01234567-abcd-9876-cdef-456789abcdef\",\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
  //       yield return new TestClassGuidPhiloteInterfaceSerializationTestData[] { new TestClassGuidPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(Guid.NewGuid()), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(), null), SerializedTestData = "" } };
  //       yield return new TestClassGuidPhiloteInterfaceSerializationTestData[] { new TestClassGuidPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(new List<KeyValuePair<string, IAbstractStronglyTypedId<Guid>>>() { new KeyValuePair<string, IAbstractStronglyTypedId<Guid>>("key1", new GuidStronglyTypedId(Guid.Empty)) }), null), SerializedTestData = "{\"ID\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIDs\":{\"key1\":\"00000000-0000-0000-0000-000000000000\"},\"TimeBlocks\":[]}" } };
  //       yield return new TestClassGuidPhiloteInterfaceSerializationTestData[] { new TestClassGuidPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(new List<KeyValuePair<string, IAbstractStronglyTypedId<Guid>>>() { new KeyValuePair<string, IAbstractStronglyTypedId<Guid>>("key1", new GuidStronglyTypedId(Guid.Empty)) }), null), SerializedTestData = "{\"ID\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIDs\":{\"key1\":\"00000000-0000-0000-0000-000000000000\"},\"TimeBlocks\":[]}" } };
  //       yield return new TestClassGuidPhiloteInterfaceSerializationTestData[] { new TestClassGuidPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(new List<KeyValuePair<string, IAbstractStronglyTypedId<Guid>>>() { new KeyValuePair<string, IAbstractStronglyTypedId<Guid>>("key1", new GuidStronglyTypedId(Guid.Empty)) }), null), SerializedTestData = "{\"ID\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIDs\":{\"key1\":\"00000000-0000-0000-0000-000000000000\"},\"TimeBlocks\":[]}" } };
  //     }
  //
  //     public IEnumerator<object[]> GetEnumerator() { return TestData().GetEnumerator(); }
  //     IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  //   }
  //
  //   public record TestClassIntPhilote : AbstractPhilote<TestClass, int> {
  //     // public TestClassIntPhilote(AbstractPhilote<TestClass, int> original) : base(original) {
  //     // }
  //
  //     public TestClassIntPhilote(IAbstractStronglyTypedId<int> iD = null, ConcurrentDictionary<string, IAbstractStronglyTypedId<int>>? additionalIDs = null, IEnumerable<ITimeBlock>? timeBlocks = null) : base(iD, additionalIDs, timeBlocks) {
  //     }
  //   }
  //
  //   public class TestClassIntPhiloteInterfaceSerializationTestData : AbstractPhiloteInterfaceSerializationTestData<TestClass, int> {
  //   }
  //
  //   public class TestClassIntPhiloteInterfaceSerializationTestDataGenerator : IEnumerable<object[]> {
  //     public static IEnumerable<object[]> TestData() {
  //       yield return new TestClassIntPhiloteInterfaceSerializationTestData[] { new TestClassIntPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, int>)new TestClassIntPhilote(new IntStronglyTypedId(0), null, null), SerializedTestData = "{\"ID\":0,\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
  //       yield return new TestClassIntPhiloteInterfaceSerializationTestData[] { new TestClassIntPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, int>)new TestClassIntPhilote(new IntStronglyTypedId(1234567), null, null), SerializedTestData = "{\"ID\":1234567,\"AdditionalIDs\":{},\"TimeBlocks\":[]}" } };
  //       yield return new TestClassIntPhiloteInterfaceSerializationTestData[] { new TestClassIntPhiloteInterfaceSerializationTestData { InstanceTestData = (IAbstractPhilote<TestClass, int>)new TestClassIntPhilote(new IntStronglyTypedId(new Random().Next()), null, null), SerializedTestData = "" } };
  //     }
  //
  //     public IEnumerator<object[]> GetEnumerator() { return TestData().GetEnumerator(); }
  //     IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  //   }

}
