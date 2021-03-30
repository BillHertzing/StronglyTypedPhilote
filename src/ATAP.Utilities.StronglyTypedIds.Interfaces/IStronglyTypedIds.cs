using System;

namespace ATAP.Utilities.StronglyTypedIds {

  public interface IAbstractStronglyTypedId<TValue> where TValue : notnull {
    TValue Value { get; init; }
  }
  public interface IGuidStronglyTypedId : IAbstractStronglyTypedId<Guid> {  }
  public interface IIntStronglyTypedId : IAbstractStronglyTypedId<int> {  }

}
