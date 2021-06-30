using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ATAP.Utilities.Collection;


namespace ATAP.Utilities.Collection.UnitTests {

  public interface IHierDataClass {

    string Name {get;set;}
    IHierDataClass? ChildHierData {get;set;}
    IEnumerable<IHierDataClass>? ChildHierListData {get;set;}
  }

  public class HierDataClass :IHierDataClass {
    public string Name {get;set;}
    public IHierDataClass? ChildHierData {get;set;}
    public IEnumerable<IHierDataClass>? ChildHierListData {get;set;}
  }

  //ToDo add validation tests to ensure illegal values are not allowed.  This applies to all XxTestDataGenerator classes
  public class CollectionExtensionTestData<T> {
    public T InstanceTestData { get; set; }
    public string SerializedTestData { get; set; }

    public CollectionExtensionTestData() {
    }

    public CollectionExtensionTestData(T instanceTestData, string serializedTestData) {
      InstanceTestData = instanceTestData;
      SerializedTestData = serializedTestData ?? throw new ArgumentNullException(nameof(serializedTestData));
    }
  }

  public class CollectionExtensionTestDataGenerator<T> : IEnumerable<object[]> {
    public static IEnumerable<object[]> CollectionExtensionTestData() {
      switch (typeof(T)) {
        case Type heirDataType when typeof(T) == typeof(HierDataClass): {
            yield return new CollectionExtensionTestData<HierDataClass>[] { new CollectionExtensionTestData<HierDataClass> {
              InstanceTestData = new HierDataClass(){Name="HierDataInstance001"}, SerializedTestData = "{\"Name\":\"HierDataInstance001\",\"ChildHierData\":null,\"ChildHierListData\":null}" } };
            yield return new CollectionExtensionTestData<HierDataClass>[] { new CollectionExtensionTestData<HierDataClass> {
              InstanceTestData = new HierDataClass(){Name="HierDataInstance002", ChildHierListData = Enumerable.Empty<HierDataClass>()}, SerializedTestData = "{\"Name\":\"HierDataInstance002\",\"ChildHierData\":null,\"ChildHierListData\":[]}" } };
          }
          break;
        case Type iHeirDataType when typeof(T) == typeof(IHierDataClass): {
            yield return new CollectionExtensionTestData<IHierDataClass>[] { new CollectionExtensionTestData<IHierDataClass> {
              InstanceTestData = (IHierDataClass)new HierDataClass(){Name="IHierDataInstance001"}, SerializedTestData = "{\"Name\":\"IHierDataInstance001\",\"ChildHierData\":null,\"ChildHierListData\":null}" } };
            yield return new CollectionExtensionTestData<IHierDataClass>[] { new CollectionExtensionTestData<IHierDataClass> {
              InstanceTestData = (IHierDataClass)new HierDataClass(){Name="IHierDataInstance002", ChildHierListData = Enumerable.Empty<IHierDataClass>()}, SerializedTestData = "{\"Name\":\"IHierDataInstance002\",\"ChildHierData\":null,\"ChildHierListData\":[]}" } };
          }
          break;
        // ToDo: replace with new custom exception and localization of exception message
        default:
          throw new Exception(FormattableString.Invariant($"Invalid T type {typeof(T)}"));
      }
    }

    public IEnumerator<object[]> GetEnumerator() { return CollectionExtensionTestData().GetEnumerator(); }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  }

}
