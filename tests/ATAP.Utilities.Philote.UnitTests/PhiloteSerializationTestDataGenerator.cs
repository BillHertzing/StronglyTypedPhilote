using System;
using System.Collections.Generic;
using System.Collections;
using ATAP.Utilities.Philote;
using System.Linq;
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

namespace ATAP.Utilities.Philote.UnitTests {

  public record TestClassWithPhiloteId<TValue> : AbstractStronglyTypedId<TValue>, IAbstractStronglyTypedId<TValue> where TValue : notnull {
    public TestClassWithPhiloteId() : base() { }
    public TestClassWithPhiloteId(TValue value) : base(value) { }
  }
  public interface ITestClassWithPhilote<TValue> : IAbstractPhilote<TestClassWithPhiloteId<TValue>, TValue> where TValue : notnull { }

  public record TestClassWithPhilote<TValue> : AbstractPhilote<TestClassWithPhiloteId<TValue>, TValue>, IAbstractPhilote<TestClassWithPhiloteId<TValue>, TValue>, ITestClassWithPhilote<TValue>
      where TValue : notnull {
    public TestClassWithPhilote(TestClassWithPhiloteId<TValue> iD = default, ConcurrentDictionary<string, IAbstractStronglyTypedId<TValue>>? additionalIds = default, IEnumerable<ITimeBlock>? timeBlocks = default) {
      if (iD != default) { Id = iD; }
      else {
        Id = (typeof(TValue)) switch {
          Type intType when typeof(TValue) == typeof(int) => new TestClassWithPhiloteId<int>() { Value = new Random().Next() } as TestClassWithPhiloteId<TValue>,
          Type GuidType when typeof(TValue) == typeof(Guid) => new TestClassWithPhiloteId<Guid>() as TestClassWithPhiloteId<TValue>,
          // ToDo: replace with new custom exception and localization of exception message
          _ => throw new Exception(FormattableString.Invariant($"Invalid TValue type {typeof(TValue)}")),

        };
      }
      // Attribution [Linq ToDictionary will not implicitly convert class to interface](https://stackoverflow.com/questions/25136049/linq-todictionary-will-not-implicitly-convert-class-to-interface) Educational but ultimately fails
      // The ToDictionary extension method available in LINQ for generic Dictionaries is NOT availabe for ConcurrentDictionaries, the following won't work...
      //  additionalIds.ToDictionary(kvp => kvp.Key, kvp => (IAbstractStronglyTypedId<TValue>) kvp.Value)
      // A this is a concurrent operation we will need to put a semaphore around the argument passed in
      // attribution [How do you convert a dictionary to a ConcurrentDictionary?](https://stackoverflow.com/questions/27063889/how-do-you-convert-a-dictionary-to-a-concurrentdictionary) from a comment on a question, contributed by Panagiotis Kanavos
      // we have to convert the parameter's value to a cast to a less derived interface
      if (additionalIds != default) {
        // ToDo : add write semaphore around the parameter before enumerating the Dictionary
        AdditionalIds = new ConcurrentDictionary<string, IAbstractStronglyTypedId<TValue>>(additionalIds.Select(kvp => new KeyValuePair<string, IAbstractStronglyTypedId<TValue>>(kvp.Key, (IAbstractStronglyTypedId<TValue>)kvp.Value)));
      }
      else {
        AdditionalIds = new ConcurrentDictionary<string, IAbstractStronglyTypedId<TValue>>();
      }
      TimeBlocks = timeBlocks != default ? timeBlocks : new List<ITimeBlock>();
    }
  }

  public class TestClassWithIntPhiloteTestData {
    public IAbstractPhilote<TestClassWithPhiloteId<int>, int> InstanceTestData { get; set; }
    public string SerializedTestData { get; set; }
    public TestClassWithIntPhiloteTestData() {
    }
    public TestClassWithIntPhiloteTestData(TestClassWithPhilote<int> instanceTestData, string serializedTestData) {
      InstanceTestData = instanceTestData;
      SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
    }
  }

  public class TestClassWithGuidPhiloteTestData {
    public IAbstractPhilote<TestClassWithPhiloteId<Guid>, Guid> InstanceTestData { get; set; }
    public string SerializedTestData { get; set; }
    public TestClassWithGuidPhiloteTestData() {
    }
    public TestClassWithGuidPhiloteTestData(TestClassWithPhilote<Guid> instanceTestData, string serializedTestData) {
      InstanceTestData = instanceTestData;
      SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
    }
  }

  public class TestClassWithIntPhiloteTestDataGenerator : IEnumerable<object[]> {
    public static IEnumerable<object[]> TestData() {
      yield return new[] { new TestClassWithIntPhiloteTestData { InstanceTestData = new TestClassWithPhilote<int>(iD: new TestClassWithPhiloteId<int>(Int32.MinValue)), SerializedTestData = "{\"Id\":-2147483648,\"AdditionalIds\":{},\"TimeBlocks\":[]}" } };
      yield return new[] { new TestClassWithIntPhiloteTestData { InstanceTestData = new TestClassWithPhilote<int>(iD: new TestClassWithPhiloteId<int>(-1)), SerializedTestData = "{\"Id\":-1,\"AdditionalIds\":{},\"TimeBlocks\":[]}" } };
      yield return new[] { new TestClassWithIntPhiloteTestData { InstanceTestData = new TestClassWithPhilote<int>(iD: new TestClassWithPhiloteId<int>(0)), SerializedTestData = "{\"Id\":0,\"AdditionalIds\":{},\"TimeBlocks\":[]}" } };
      yield return new[] { new TestClassWithIntPhiloteTestData { InstanceTestData = new TestClassWithPhilote<int>(iD: new TestClassWithPhiloteId<int>(1)), SerializedTestData = "{\"Id\":1,\"AdditionalIds\":{},\"TimeBlocks\":[]}" } };
      yield return new[] { new TestClassWithIntPhiloteTestData { InstanceTestData = new TestClassWithPhilote<int>(iD: new TestClassWithPhiloteId<int>(Int32.MaxValue)), SerializedTestData = "{\"Id\":2147483647,\"AdditionalIds\":{},\"TimeBlocks\":[]}" } };
      // yield return new[] { new TestClassWithIntPhiloteTestData { InstanceTestData = new TestClassWithIntPhilote(name: "TestClassWithIntPhiloteTestDataInt32Min", philote: new TestClassIntPhilote(Int32.MinValue)), SerializedTestData = "{\"Name\":\"TestClassWithIntPhiloteTestDataInt32Min\",\"Philote\":{\"Id\":-2147483648,\"AdditionalIds\":{},\"TimeBlocks\":[]}}" } };
      // yield return new[] { new TestClassWithIntPhiloteTestData { InstanceTestData = new TestClassWithIntPhilote(name: "TestClassWithIntPhiloteTestDataNegativeOne", philote: new TestClassIntPhilote(-1)), SerializedTestData = "{\"Name\":\"TestClassWithIntPhiloteTestDataNegativeOne\",\"Philote\":{\"Id\":-1,\"AdditionalIds\":{},\"TimeBlocks\":[]}}" } };
      // yield return new[] { new TestClassWithIntPhiloteTestData { InstanceTestData = new TestClassWithIntPhilote(name: "TestClassWithIntPhiloteTestDataZero", philote: new TestClassIntPhilote(0)), SerializedTestData = "{\"Name\":\"TestClassWithIntPhiloteTestDataZero\",\"Philote\":{\"Id\":0,\"AdditionalIds\":{},\"TimeBlocks\":[]}}" } };
      // yield return new[] { new TestClassWithIntPhiloteTestData { InstanceTestData = new TestClassWithIntPhilote(name: "TestClassWithIntPhiloteTestDataPositiveOne", philote: new TestClassIntPhilote(1)), SerializedTestData = "{\"Name\":\"TestClassWithIntPhiloteTestDataPositiveOne\",\"Philote\":{\"Id\":1,\"AdditionalIds\":{},\"TimeBlocks\":[]}}" } };
      // yield return new[] { new TestClassWithIntPhiloteTestData { InstanceTestData = new TestClassWithIntPhilote(name: "TestClassWithIntPhiloteTestDataInt32Max", philote: new TestClassIntPhilote(Int32.MaxValue)), SerializedTestData = "{\"Name\":\"TestClassWithIntPhiloteTestDataInt32Max\",\"Philote\":{\"Id\":2147483647,\"AdditionalIds\":{},\"TimeBlocks\":[]}}" } };
    }
    public IEnumerator<object[]> GetEnumerator() { return TestData().GetEnumerator(); }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  }

  public class TestClassWithGuidPhiloteTestDataGenerator : IEnumerable<object[]> {
    public static IEnumerable<object[]> TestData() {
      yield return new[] { new TestClassWithGuidPhiloteTestData { InstanceTestData = new TestClassWithPhilote<Guid>(iD: new TestClassWithPhiloteId<Guid>(Guid.Empty)), SerializedTestData = "{\"Philote\":{\"Id\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIds\":{},\"TimeBlocks\":[]}}" } };
      yield return new[] { new TestClassWithGuidPhiloteTestData { InstanceTestData = new TestClassWithPhilote<Guid>(iD: new TestClassWithPhiloteId<Guid>(new Guid("01234567-abcd-9876-cdef-456789abcdef"))), SerializedTestData = "{\"Philote\":{\"Id\":\"01234567-abcd-9876-cdef-456789abcdef\",\"AdditionalIds\":{},\"TimeBlocks\":[]}}" } };
      // yield return new [] { new TestClassWithGuidPhiloteTestData { InstanceTestData = new TestClassWithPhilote<Guid>(name: "TestClassWithGuidPhiloteTestDataGuidEmpty", philote: new TestClassGuidPhilote(Guid.Empty)), SerializedTestData = "{\"Name\":\"TestClassWithGuidPhiloteTestDataGuidEmpty\",\"Philote\":{\"Id\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIds\":{},\"TimeBlocks\":[]}}" } };
      // yield return new [] { new TestClassWithGuidPhiloteTestData { InstanceTestData = new TestClassWithPhilote<Guid>(name: "TestClassWithGuidPhiloteTestDataGuid01234", philote: new TestClassGuidPhilote(new Guid("01234567-abcd-9876-cdef-456789abcdef"))), SerializedTestData = "{\"Name\":\"TestClassWithGuidPhiloteTestDataGuid01234\",\"Philote\":{\"Id\":\"01234567-abcd-9876-cdef-456789abcdef\",\"AdditionalIds\":{},\"TimeBlocks\":[]}}" } };
    }
    public IEnumerator<object[]> GetEnumerator() { return TestData().GetEnumerator(); }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  }

  // public record TestRecordIntPhiloteId : AbstractStronglyTypedId<int> {
  //   public TestRecordIntPhiloteId() : base() { }
  //   public TestRecordIntPhiloteId(int value) : base(value) { }
  // }

  // public record TestClassGuidPhiloteId : AbstractStronglyTypedId<Guid> {
  //   public TestClassGuidPhiloteId() : base() { }
  //   public TestClassGuidPhiloteId(Guid value) : base(value) { }
  // }

  // public record TestRecordGuidPhiloteId : AbstractStronglyTypedId<Guid> {
  //   public TestRecordGuidPhiloteId() : base() { }
  //   public TestRecordGuidPhiloteId(Guid value) : base(value) { }
  // }

  // public record TestClassWithIntPhiloteId :  AbstractStronglyTypedId<TValue>, IAbstractStronglyTypedId<TValue> where TValue : notnull {
  //   public TestClassWithIntPhiloteId() : base() { }
  //   public TestClassWithIntPhiloteId(int value) : base(value) { }
  // }

  // public record TestClassIntPhilote : AbstractPhilote<TestClassWithIntPhiloteId, int>  {
  //   public TestClassIntPhilote() : base() { }
  //   public TestClassIntPhilote(int value) : base(value) { }
  // }

  // public class TestClassWithIntPhilote {
  //   public TestClassWithIntPhilote(string name = null, AbstractPhilote<TestClassWithIntPhiloteId, int> philote = null) {
  //     Name = name ?? throw new ArgumentNullException(nameof(name));
  //     Philote = philote ?? new TestClassIntPhilote();
  //   }
  //   public string Name { get; set; }
  //   public IAbstractPhilote<TestClassWithIntPhiloteId<int>, int> Philote { get; set; }
  // }

  // public class TestClassWithIntPhiloteTestData {
  //   public TestClassWithIntPhilote InstanceTestData { get; set; }
  //   public string SerializedTestData { get; set; }
  //   public TestClassWithIntPhiloteTestData() {
  //   }
  //   public TestClassWithIntPhiloteTestData(TestClassWithIntPhilote instanceTestData, string serializedTestData) {
  //     InstanceTestData = instanceTestData;
  //     SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
  //   }
  // }

  // public record TestClassGuidPhilote : AbstractPhilote<TestClassGuidPhiloteId, Guid> {
  //   public TestClassGuidPhilote() : base() { }
  //   public TestClassGuidPhilote(Guid value) : base(value) { }
  // }


  // public class TestClassWithGuidPhilote {
  //   public TestClassWithGuidPhilote(string name = null, TestClassGuidPhilote philote = null) {
  //     Name = name ?? throw new ArgumentNullException(nameof(name));
  //     Philote = philote ?? new TestClassGuidPhilote();
  //   }
  //   public string Name { get; set; }
  //   public TestClassGuidPhilote Philote { get; set; }
  // }

  // public class TestClassWithGuidPhiloteTestData {
  //   public IAbstractPhilote<TestClassWithPhiloteId<Guid>, Guid> InstanceTestData { get; set; }
  //   public string SerializedTestData { get; set; }
  //   public TestClassWithGuidPhiloteTestData() {
  //   }
  //   public TestClassWithGuidPhiloteTestData(TestClassWithPhilote<Guid> instanceTestData, string serializedTestData) {
  //     InstanceTestData = instanceTestData;
  //     SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
  //   }
  // }

  // public class TestClassWithGuidPhiloteTestData {
  //   public TestClassWithGuidPhilote InstanceTestData { get; set; }
  //   public string SerializedTestData { get; set; }
  //   public TestClassWithGuidPhiloteTestData() {
  //   }
  // }

  // public record TestRecordIntPhilote : AbstractIntPhilote<TestRecordIntPhiloteId> {
  //   public TestRecordIntPhilote() : base() { }
  //   public TestRecordIntPhilote(int value) : base(value) { }
  // }

  // public record TestRecordWithIntPhilote {
  //   public TestRecordWithIntPhilote(string name = null, TestRecordIntPhilote philote = null) {
  //     Name = name ?? throw new ArgumentNullException(nameof(name));
  //     Philote = philote ?? new TestRecordIntPhilote();
  //   }
  //   public string Name { get; set; }
  //   public TestRecordIntPhilote Philote { get; set; }
  // }

  // public class TestRecordWithIntPhiloteTestData {
  //   public TestRecordWithIntPhilote InstanceTestData { get; set; }
  //   public string SerializedTestData { get; set; }
  //   public TestRecordWithIntPhiloteTestData() {
  //   }
  //   public TestRecordWithIntPhiloteTestData(TestRecordWithIntPhilote instanceTestData, string serializedTestData) {
  //     InstanceTestData = instanceTestData;
  //     SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
  //   }
  // }

  // public class TestRecordWithIntPhiloteTestDataGenerator : IEnumerable<object[]> {
  //   public static IEnumerable<object[]> TestData() {
  //     yield return new[] { new TestRecordWithIntPhiloteTestData { InstanceTestData = new TestRecordWithIntPhilote(name: "TestRecordWithIntPhiloteTestDataInt32Min", philote: new TestRecordIntPhilote(Int32.MinValue)), SerializedTestData = "{\"Name\":\"TestRecordWithIntPhiloteTestDataInt32Min\",\"Philote\":{\"Id\":-2147483648,\"AdditionalIds\":{},\"TimeBlocks\":[]}}" } };
  //     yield return new[] { new TestRecordWithIntPhiloteTestData { InstanceTestData = new TestRecordWithIntPhilote(name: "TestRecordWithIntPhiloteTestDataNegativeOne", philote: new TestRecordIntPhilote(-1)), SerializedTestData = "{\"Name\":\"TestRecordWithIntPhiloteTestDataNegativeOne\",\"Philote\":{\"Id\":-1,\"AdditionalIds\":{},\"TimeBlocks\":[]}}" } };
  //     yield return new[] { new TestRecordWithIntPhiloteTestData { InstanceTestData = new TestRecordWithIntPhilote(name: "TestRecordWithIntPhiloteTestDataZero", philote: new TestRecordIntPhilote(0)), SerializedTestData = "{\"Name\":\"TestRecordWithIntPhiloteTestDataZero\",\"Philote\":{\"Id\":0,\"AdditionalIds\":{},\"TimeBlocks\":[]}}" } };
  //     yield return new[] { new TestRecordWithIntPhiloteTestData { InstanceTestData = new TestRecordWithIntPhilote(name: "TestRecordWithIntPhiloteTestDataPositiveOne", philote: new TestRecordIntPhilote(1)), SerializedTestData = "{\"Name\":\"TestRecordWithIntPhiloteTestDataPositiveOne\",\"Philote\":{\"Id\":1,\"AdditionalIds\":{},\"TimeBlocks\":[]}}" } };
  //     yield return new[] { new TestRecordWithIntPhiloteTestData { InstanceTestData = new TestRecordWithIntPhilote(name: "TestRecordWithIntPhiloteTestDataInt32Max", philote: new TestRecordIntPhilote(Int32.MaxValue)), SerializedTestData = "{\"Name\":\"TestRecordWithIntPhiloteTestDataInt32Max\",\"Philote\":{\"Id\":2147483647,\"AdditionalIds\":{},\"TimeBlocks\":[]}}" } };
  //   }
  //   public IEnumerator<object[]> GetEnumerator() { return TestData().GetEnumerator(); }
  //   IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  // }

  // public record TestRecordGuidPhilote : AbstractGuidPhilote<TestRecordGuidPhiloteId> {
  //   public TestRecordGuidPhilote() : base() { }
  //   public TestRecordGuidPhilote(Guid value) : base(value) { }
  // }

  // public record TestRecordWithGuidPhilote {
  //   public TestRecordWithGuidPhilote(string name = null, TestRecordGuidPhilote philote = null) {
  //     Name = name ?? throw new ArgumentNullException(nameof(name));
  //     Philote = philote ?? new TestRecordGuidPhilote();
  //   }
  //   public string Name { get; set; }
  //   public TestRecordGuidPhilote Philote { get; set; }
  // }

  // public class TestRecordWithGuidPhiloteTestData {
  //   public TestRecordWithGuidPhilote InstanceTestData { get; set; }
  //   public string SerializedTestData { get; set; }
  //   public TestRecordWithGuidPhiloteTestData() {
  //   }
  // }

  // public class TestRecordWithGuidPhiloteTestDataGenerator : IEnumerable<object[]> {
  //   public static IEnumerable<object[]> TestData() {
  //     yield return new TestRecordWithGuidPhiloteTestData[] { new TestRecordWithGuidPhiloteTestData { InstanceTestData = new TestRecordWithGuidPhilote(name: "TestRecordWithGuidPhiloteTestDataGuidEmpty", philote: new TestRecordGuidPhilote(Guid.Empty)), SerializedTestData = "{\"Name\":\"TestRecordWithGuidPhiloteTestDataGuidEmpty\",\"Philote\":{\"Id\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIds\":{},\"TimeBlocks\":[]}}" } };
  //     yield return new TestRecordWithGuidPhiloteTestData[] { new TestRecordWithGuidPhiloteTestData { InstanceTestData = new TestRecordWithGuidPhilote(name: "TestRecordWithGuidPhiloteTestDataGuid01234", philote: new TestRecordGuidPhilote(new Guid("01234567-abcd-9876-cdef-456789abcdef"))), SerializedTestData = "{\"Name\":\"TestRecordWithGuidPhiloteTestDataGuid01234\",\"Philote\":{\"Id\":\"01234567-abcd-9876-cdef-456789abcdef\",\"AdditionalIds\":{},\"TimeBlocks\":[]}}" } };
  //   }
  //   public IEnumerator<object[]> GetEnumerator() { return TestData().GetEnumerator(); }
  //   IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  // }

  // public class TestClassWithIntIPhilote {
  //   public TestClassWithIntIPhilote(string name = null, IPhiloteTestRecordInt philote = null) {
  //     Name = name ?? throw new ArgumentNullException(nameof(name));
  //     Philote = philote ?? (IPhiloteTestRecordInt)new PhiloteTestRecordInt();
  //   }

  //   public string Name { get; set; }
  //   public IPhiloteTestRecordInt Philote { get; set; }
  // }

  // public class TestClassWithIntIPhiloteTestData {
  //   public TestClassWithIntIPhilote InstanceTestData { get; set; }
  //   public string SerializedTestData { get; set; }

  //   public TestClassWithIntIPhiloteTestData() {
  //   }

  //   public TestClassWithIntIPhiloteTestData(TestClassWithIntIPhilote instanceTestData, string serializedTestData) {
  //     InstanceTestData = instanceTestData;
  //     SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
  //   }
  // }

  // public class TestClassWithIntIPhiloteTestDataGenerator : IEnumerable<object[]> {
  //   public static IEnumerable<object[]> TestData() {
  //     yield return new TestClassWithIntIPhiloteTestData[] { new TestClassWithIntIPhiloteTestData { InstanceTestData = new TestClassWithIntIPhilote(name: "TestClassWithIntIPhiloteTestDataInt32Min", philote: new PhiloteTestRecordInt(new IntStronglyTypedId(Int32.MinValue))), SerializedTestData = "{\"Name\":\"TestClassWithIntIPhiloteTestDataInt32Min\",\"Philote\":{\"Id\":-2147483648,\"AdditionalIds\":{},\"TimeBlocks\":[]}}" } };
  //     yield return new TestClassWithIntIPhiloteTestData[] { new TestClassWithIntIPhiloteTestData { InstanceTestData = new TestClassWithIntIPhilote(name: "TestClassWithIntIPhiloteTestDataNegativeOne", philote: new PhiloteTestRecordInt(new IntStronglyTypedId(-1))), SerializedTestData = "{\"Name\":\"TestClassWithIntIPhiloteTestDataNegativeOne\",\"Philote\":{\"Id\":-1,\"AdditionalIds\":{},\"TimeBlocks\":[]}}" } };
  //     yield return new TestClassWithIntIPhiloteTestData[] { new TestClassWithIntIPhiloteTestData { InstanceTestData = new TestClassWithIntIPhilote(name: "TestClassWithIntIPhiloteTestDataZero", philote: new PhiloteTestRecordInt(new IntStronglyTypedId(0))), SerializedTestData = "{\"Name\":\"TestClassWithIntIPhiloteTestDataZero\",\"Philote\":{\"Id\":0,\"AdditionalIds\":{},\"TimeBlocks\":[]}}" } };
  //     yield return new TestClassWithIntIPhiloteTestData[] { new TestClassWithIntIPhiloteTestData { InstanceTestData = new TestClassWithIntIPhilote(name: "TestClassWithIntIPhiloteTestDataPositiveOne", philote: new PhiloteTestRecordInt(new IntStronglyTypedId(1))), SerializedTestData = "{\"Name\":\"TestClassWithIntIPhiloteTestDataPositiveOne\",\"Philote\":{\"Id\":1,\"AdditionalIds\":{},\"TimeBlocks\":[]}}" } };
  //     yield return new TestClassWithIntIPhiloteTestData[] { new TestClassWithIntIPhiloteTestData { InstanceTestData = new TestClassWithIntIPhilote(name: "TestClassWithIntIPhiloteTestDataInt32Max", philote: new PhiloteTestRecordInt(new IntStronglyTypedId(Int32.MaxValue))), SerializedTestData = "{\"Name\":\"TestClassWithIntIPhiloteTestDataInt32Max\",\"Philote\":{\"Id\":2147483647,\"AdditionalIds\":{},\"TimeBlocks\":[]}}" } };
  //   }

  //   public IEnumerator<object[]> GetEnumerator() { return TestData().GetEnumerator(); }
  //   IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  // }

  // //ToDo add validation tests to ensure illegal values are not allowed.  This applies to all XxTestDataGenerator classes
  // public abstract class AbstractPhiloteInterfaceTestData<T, TValue> where T : class where TValue : notnull {
  //   public virtual IAbstractPhilote<T, TValue> InstanceTestData { get; set; }
  //   public string SerializedTestData { get; set; }

  //   public AbstractPhiloteInterfaceTestData() {
  //   }

  //   public AbstractPhiloteInterfaceTestData(IAbstractPhilote<T, TValue> instanceTestData, string serializedTestData) {
  //     InstanceTestData = instanceTestData;
  //     SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
  //   }
  // }

  // public class PT1IntTestDataGenerator : IEnumerable<object[]> {
  //   public static IEnumerable<object[]> TestData() {
  //     yield return new PT1IntTestData[] { new PT1IntTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(0)), SerializedTestData = "{\"Id\":0,\"AdditionalIds\":{},\"TimeBlocks\":[]}" } };
  //     yield return new PT1IntTestData[] { new PT1IntTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(-1)), SerializedTestData = "{\"Id\":-1,\"AdditionalIds\":{},\"TimeBlocks\":[]}" } };
  //     yield return new PT1IntTestData[] { new PT1IntTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(Int32.MinValue)), SerializedTestData = "{\"Id\":-2147483648,\"AdditionalIds\":{},\"TimeBlocks\":[]}" } };
  //     yield return new PT1IntTestData[] { new PT1IntTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(Int32.MaxValue)), SerializedTestData = "{\"Id\":2147483647,\"AdditionalIds\":{},\"TimeBlocks\":[]}" } };
  //     yield return new PT1IntTestData[] { new PT1IntTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(1234567)), SerializedTestData = "{\"Id\":1234567,\"AdditionalIds\":{},\"TimeBlocks\":[]}" } };
  //     yield return new PT1IntTestData[] { new PT1IntTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(new Random().Next())), SerializedTestData = "" } };


  //     // yield return new PT1IntTestData[] { new PT1IntTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(0), null, null), SerializedTestData = "{\"Id\":0,\"AdditionalIds\":{},\"TimeBlocks\":[]}" } };
  //     // yield return new PT1IntTestData[] { new PT1IntTestData { InstanceTestData = new PT1Int(new IntStronglyTypedId(0), null, null), SerializedTestData = "{\"Id\":0,\"AdditionalIds\":{},\"TimeBlocks\":[]}" } };
  //     // yield return new PT1IntTestData[] { new PT1IntTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(new Guid("01234567-abcd-9876-cdef-456789abcdef")), null, null), SerializedTestData = "{\"Id\":\"01234567-abcd-9876-cdef-456789abcdef\",\"AdditionalIds\":{},\"TimeBlocks\":[]}" } };
  //     // yield return new PT1IntTestData[] { new PT1IntTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(Guid.NewGuid()), null, null), SerializedTestData = "" } };
  //     // yield return new PT1IntTestData[] { new PT1IntTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(), null), SerializedTestData = "{\"Id\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIds\":{},\"TimeBlocks\":[]}" } };
  //     // yield return new PT1IntTestData[] { new PT1IntTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(new Guid("01234567-abcd-9876-cdef-456789abcdef")), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(), null), SerializedTestData = "{\"Id\":\"01234567-abcd-9876-cdef-456789abcdef\",\"AdditionalIds\":{},\"TimeBlocks\":[]}" } };
  //     // yield return new PT1IntTestData[] { new PT1IntTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(Guid.NewGuid()), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(), null), SerializedTestData = "" } };
  //     // yield return new PT1IntTestData[] { new PT1IntTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(new List<KeyValuePair<string, IAbstractStronglyTypedId<Guid>>>() { new KeyValuePair<string, IAbstractStronglyTypedId<Guid>>("key1", new GuidStronglyTypedId(Guid.Empty)) }), null), SerializedTestData = "{\"Id\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIds\":{\"key1\":\"00000000-0000-0000-0000-000000000000\"},\"TimeBlocks\":[]}" } };
  //     // yield return new PT1IntTestData[] { new PT1IntTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(new List<KeyValuePair<string, IAbstractStronglyTypedId<Guid>>>() { new KeyValuePair<string, IAbstractStronglyTypedId<Guid>>("key1", new GuidStronglyTypedId(Guid.Empty)) }), null), SerializedTestData = "{\"Id\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIds\":{\"key1\":\"00000000-0000-0000-0000-000000000000\"},\"TimeBlocks\":[]}" } };
  //     // yield return new PT1IntTestData[] { new PT1IntTestData { InstanceTestData = new PT1Int(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(new List<KeyValuePair<string, IAbstractStronglyTypedId<Guid>>>() { new KeyValuePair<string, IAbstractStronglyTypedId<Guid>>("key1", new GuidStronglyTypedId(Guid.Empty)) }), null), SerializedTestData = "{\"Id\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIds\":{\"key1\":\"00000000-0000-0000-0000-000000000000\"},\"TimeBlocks\":[]}" } };
  //   }

  //   public IEnumerator<object[]> GetEnumerator() { return TestData().GetEnumerator(); }
  //   IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  // }

  // public class TestClassGuidPhiloteInterfaceTestDataGenerator : IEnumerable<object[]> {
  //   public static IEnumerable<object[]> TestData() {
  //     yield return new TestClassGuidPhiloteInterfaceTestData[] { new TestClassGuidPhiloteInterfaceTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(Guid.Empty), null, null), SerializedTestData = "{\"Id\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIds\":{},\"TimeBlocks\":[]}" } };
  //     yield return new TestClassGuidPhiloteInterfaceTestData[] { new TestClassGuidPhiloteInterfaceTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(new Guid("01234567-abcd-9876-cdef-456789abcdef")), null, null), SerializedTestData = "{\"Id\":\"01234567-abcd-9876-cdef-456789abcdef\",\"AdditionalIds\":{},\"TimeBlocks\":[]}" } };
  //     yield return new TestClassGuidPhiloteInterfaceTestData[] { new TestClassGuidPhiloteInterfaceTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(Guid.NewGuid()), null, null), SerializedTestData = "" } };
  //     yield return new TestClassGuidPhiloteInterfaceTestData[] { new TestClassGuidPhiloteInterfaceTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(), null), SerializedTestData = "{\"Id\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIds\":{},\"TimeBlocks\":[]}" } };
  //     yield return new TestClassGuidPhiloteInterfaceTestData[] { new TestClassGuidPhiloteInterfaceTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(new Guid("01234567-abcd-9876-cdef-456789abcdef")), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(), null), SerializedTestData = "{\"Id\":\"01234567-abcd-9876-cdef-456789abcdef\",\"AdditionalIds\":{},\"TimeBlocks\":[]}" } };
  //     yield return new TestClassGuidPhiloteInterfaceTestData[] { new TestClassGuidPhiloteInterfaceTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(Guid.NewGuid()), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(), null), SerializedTestData = "" } };
  //     yield return new TestClassGuidPhiloteInterfaceTestData[] { new TestClassGuidPhiloteInterfaceTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(new List<KeyValuePair<string, IAbstractStronglyTypedId<Guid>>>() { new KeyValuePair<string, IAbstractStronglyTypedId<Guid>>("key1", new GuidStronglyTypedId(Guid.Empty)) }), null), SerializedTestData = "{\"Id\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIds\":{\"key1\":\"00000000-0000-0000-0000-000000000000\"},\"TimeBlocks\":[]}" } };
  //     yield return new TestClassGuidPhiloteInterfaceTestData[] { new TestClassGuidPhiloteInterfaceTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(new List<KeyValuePair<string, IAbstractStronglyTypedId<Guid>>>() { new KeyValuePair<string, IAbstractStronglyTypedId<Guid>>("key1", new GuidStronglyTypedId(Guid.Empty)) }), null), SerializedTestData = "{\"Id\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIds\":{\"key1\":\"00000000-0000-0000-0000-000000000000\"},\"TimeBlocks\":[]}" } };
  //     yield return new TestClassGuidPhiloteInterfaceTestData[] { new TestClassGuidPhiloteInterfaceTestData { InstanceTestData = (IAbstractPhilote<TestClass, Guid>)new TestClassGuidPhilote(new GuidStronglyTypedId(Guid.Empty), new ConcurrentDictionary<string, IAbstractStronglyTypedId<Guid>>(new List<KeyValuePair<string, IAbstractStronglyTypedId<Guid>>>() { new KeyValuePair<string, IAbstractStronglyTypedId<Guid>>("key1", new GuidStronglyTypedId(Guid.Empty)) }), null), SerializedTestData = "{\"Id\":\"00000000-0000-0000-0000-000000000000\",\"AdditionalIds\":{\"key1\":\"00000000-0000-0000-0000-000000000000\"},\"TimeBlocks\":[]}" } };
  //   }

  //   public IEnumerator<object[]> GetEnumerator() { return TestData().GetEnumerator(); }
  //   IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  // }

  // public record TestClassIntPhilote : AbstractPhilote<TestClass, int> {
  //   // public TestClassIntPhilote(AbstractPhilote<TestClass, int> original) : base(original) {
  //   // }

  //   public TestClassIntPhilote(IAbstractStronglyTypedId<int> iD = null, ConcurrentDictionary<string, IAbstractStronglyTypedId<int>>? additionalIds = null, IEnumerable<ITimeBlock>? timeBlocks = null) : base(iD, additionalIds, timeBlocks) {
  //   }
  // }

  // public class TestClassIntPhiloteInterfaceTestData : AbstractPhiloteInterfaceTestData<TestClass, int> {
  // }

  // public class TestClassIntPhiloteInterfaceTestDataGenerator : IEnumerable<object[]> {
  //   public static IEnumerable<object[]> TestData() {
  //     yield return new TestClassIntPhiloteInterfaceTestData[] { new TestClassIntPhiloteInterfaceTestData { InstanceTestData = (IAbstractPhilote<TestClass, int>)new TestClassIntPhilote(new IntStronglyTypedId(0), null, null), SerializedTestData = "{\"Id\":0,\"AdditionalIds\":{},\"TimeBlocks\":[]}" } };
  //     yield return new TestClassIntPhiloteInterfaceTestData[] { new TestClassIntPhiloteInterfaceTestData { InstanceTestData = (IAbstractPhilote<TestClass, int>)new TestClassIntPhilote(new IntStronglyTypedId(1234567), null, null), SerializedTestData = "{\"Id\":1234567,\"AdditionalIds\":{},\"TimeBlocks\":[]}" } };
  //     yield return new TestClassIntPhiloteInterfaceTestData[] { new TestClassIntPhiloteInterfaceTestData { InstanceTestData = (IAbstractPhilote<TestClass, int>)new TestClassIntPhilote(new IntStronglyTypedId(new Random().Next()), null, null), SerializedTestData = "" } };
  //   }

  //   public IEnumerator<object[]> GetEnumerator() { return TestData().GetEnumerator(); }
  //   IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  // }

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
