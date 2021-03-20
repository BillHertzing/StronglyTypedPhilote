using System;
using ATAP.Utilities.StronglyTypedIds;
using Itenso.TimePeriod;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace ATAP.Utilities.Philote {
  public interface IGuidPhilote<T> : IAbstractPhilote<T, Guid> where T : IStronglyTypedId<Guid> { }
  public interface IIntPhilote<T> : IAbstractPhilote<T, int> where T : IStronglyTypedId<int> { }

  public interface IAbstractPhilote<T, TValue> where T : IStronglyTypedId<TValue> where TValue : notnull {
    IStronglyTypedId<TValue> ID { get; }
    ConcurrentDictionary<string, IStronglyTypedId<TValue>>? AdditionalIDs { get; }
    IEnumerable<ITimeBlock>? TimeBlocks { get; }
  }
}
