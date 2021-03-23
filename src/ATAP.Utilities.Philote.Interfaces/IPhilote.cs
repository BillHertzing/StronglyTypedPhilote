using System;
using ATAP.Utilities.StronglyTypedIds;
using Itenso.TimePeriod;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace ATAP.Utilities.Philote {
  public interface IGuidPhilote<TId> : IAbstractPhilote<TId, Guid> where TId : IStronglyTypedId<Guid> { }
  public interface IIntPhilote<TId> : IAbstractPhilote<TId, int> where TId : IStronglyTypedId<int> { }

  public interface IAbstractPhilote<TId, TValue> where TId: IStronglyTypedId<TValue> where TValue : notnull {
    TId ID { get; }
    ConcurrentDictionary<string, IStronglyTypedId<TValue>>? AdditionalIDs { get; }
    IEnumerable<ITimeBlock>? TimeBlocks { get; }
  }
}
