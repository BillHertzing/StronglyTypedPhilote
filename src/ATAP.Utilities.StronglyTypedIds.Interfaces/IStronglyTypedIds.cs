using System;

namespace ATAP.Utilities.StronglyTypedID {
  public interface IStronglyTypedId<TValue>  {
    TValue Value { get; init; }
  }
}
