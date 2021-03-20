using System;
using ATAP.Utilities.StronglyTypedIds;
using Itenso.TimePeriod;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace ATAP.Utilities.Philote
{
    public interface IGuidPhilote<T> : IAbstractPhilote<T, Guid> where T : class{}
    public interface IIntPhilote<T> : IAbstractPhilote<T, int> where T : class{}

  public interface IAbstractPhilote<T, TValue> where T : class where TValue : notnull
  {
    IStronglyTypedId<TValue> ID { get; }
    ConcurrentDictionary<string, IStronglyTypedId<TValue>>? AdditionalIDs { get; }
    IEnumerable<ITimeBlock>? TimeBlocks { get; }
  }
}
