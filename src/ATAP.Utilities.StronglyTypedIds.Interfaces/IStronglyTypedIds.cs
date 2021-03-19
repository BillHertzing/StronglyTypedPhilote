using System;

namespace ATAP.Utilities.StronglyTypedIds{
 public interface IIdAsStruct<T> { }
  public interface IStronglyTypedID<TValue> where TValue : notnull {
    TValue Value { get; init; }
  }
  public interface IGuidStronglyTypedID : IStronglyTypedID<Guid> {  }
  public interface IIntStronglyTypedID : IStronglyTypedID<int> {  }

  public interface IStronglyTypedId<TValue>  {
    TValue Value { get; init; }
  }
}
