using System;

namespace ATAP.Utilities.StronglyTypedID {
  public interface IStronglyTypedId<TValue>  {
    TValue Value { get; init; }
  }
  public interface IGuidStronglyTypedId : IStronglyTypedId<Guid> {  }
  public interface IIntStronglyTypedId : IStronglyTypedId<int> {  }
}
