using System;
using System.Collections.Generic;
using System.Collections;
using ATAP.Utilities.Philote;
using ATAP.Utilities.StronglyTypedID;
using System.Collections.Concurrent;
using Itenso.TimePeriod;

namespace ATAP.Utilities.Philote.UnitTests {

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
  public class TestClass { }
  public record TestClassGuidPhilote : AbstractPhilote<TestClass, Guid> {
    public TestClassGuidPhilote(AbstractPhilote<TestClass, Guid> original) : base(original) {
    }

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
    }

    public IEnumerator<object[]> GetEnumerator() { return TestData().GetEnumerator(); }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  }

  public record TestClassIntPhilote : AbstractPhilote<TestClass, int> {
    public TestClassIntPhilote(AbstractPhilote<TestClass, int> original) : base(original) {
    }

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

  // public abstract class AbstractClassContiningJustPhilote<TValue> where TValue : notnull {
  //   public abstract AbstractPhilote<AbstractClassContiningJustPhilote<TValue>, TValue> InstanceTestData { get; set; }

  // }

  // public class ClassContiningJustGuidPhilote : AbstractClassContiningJustPhilote<Guid> {
  //   public override AbstractPhilote<AbstractClassContiningJustPhilote<Guid>, Guid> InstanceTestData { get; set; }

  //   public ClassContiningJustGuidPhilote(Guid guid) {
  //     this.InstanceTestData = new GuidPhilote<ClassContiningJustGuidPhilote>(guid);
  //   }

  // }

  // public class ClassContiningJustIntPhilote : AbstractClassContiningJustPhilote<int> {
  //   public override AbstractPhilote<AbstractClassContiningJustPhilote<int>, int> InstanceTestData { get; set; }

  // }

  // public class ClassContiningJustGuidPhiloteSerializationTestData {
  //   public ClassContiningJustGuidPhilote InstanceTestData { get; set; }
  //   public string SerializedTestData { get; set; }

  //   public ClassContiningJustGuidPhiloteSerializationTestData() {
  //   }

  //   public ClassContiningJustGuidPhiloteSerializationTestData(ClassContiningJustGuidPhilote instanceTestData, string serializedTestData) {
  //     InstanceTestData = instanceTestData;
  //     SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
  //   }
  // }

  // public class ClassContiningJustGuidPhiloteSerializationTestDataGenerator : IEnumerable<object[]> {
  //   public static IEnumerable<object[]> PhiloteSerializationTestData() {
  //     yield return new ClassContiningJustGuidPhiloteSerializationTestData[] { new ClassContiningJustGuidPhiloteSerializationTestData { InstanceTestData = new ClassContiningJustGuidPhilote(Guid.Empty), SerializedTestData = "\"00000000-0000-0000-0000-000000000000\"" } };
  //     yield return new ClassContiningJustGuidPhiloteSerializationTestData[] { new ClassContiningJustGuidPhiloteSerializationTestData { InstanceTestData = new ClassContiningJustGuidPhilote(new Guid("01234567-abcd-9876-cdef-456789abcdef")), SerializedTestData = "\"01234567-abcd-9876-cdef-456789abcdef\"" } };
  //     yield return new ClassContiningJustGuidPhiloteSerializationTestData[] { new ClassContiningJustGuidPhiloteSerializationTestData { InstanceTestData = new ClassContiningJustGuidPhilote(new Guid("A1234567-abcd-9876-cdef-456789abcdef")), SerializedTestData = "\"A1234567-abcd-9876-cdef-456789abcdef\"" } };
  //     yield return new ClassContiningJustGuidPhiloteSerializationTestData[] { new ClassContiningJustGuidPhiloteSerializationTestData { InstanceTestData = new ClassContiningJustGuidPhilote(Guid.NewGuid()), SerializedTestData = "" } };
  //   }

  //   public IEnumerator<object[]> GetEnumerator() { return PhiloteSerializationTestData().GetEnumerator(); }
  //   IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  // }

  // public class IntPhiloteSerializationTestData {
  //   public IntPhilote InstanceTestData { get; set; }
  //   public string SerializedTestData { get; set; }

  //   public IntPhiloteSerializationTestData() {
  //   }

  //   public IntPhiloteSerializationTestData(IntPhilote instanceTestData, string serializedTestData) {
  //     InstanceTestData = instanceTestData;
  //     SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
  //   }
  // }

  // public class IntPhiloteSerializationTestDataGenerator : IEnumerable<object[]> {
  //   public static IEnumerable<object[]> PhiloteSerializationTestData() {
  //     yield return new IntPhiloteSerializationTestData[] { new IntPhiloteSerializationTestData { InstanceTestData = new IntPhilote(0), SerializedTestData = "0" } };
  //     yield return new IntPhiloteSerializationTestData[] { new IntPhiloteSerializationTestData { InstanceTestData = new IntPhilote(1234567), SerializedTestData = "1234567" } };
  //     yield return new IntPhiloteSerializationTestData[] { new IntPhiloteSerializationTestData { InstanceTestData = new IntPhilote(new Random().Next()), SerializedTestData = "" } };
  //   }

  //   public IEnumerator<object[]> GetEnumerator() { return PhiloteSerializationTestData().GetEnumerator(); }
  //   IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  // }
}
