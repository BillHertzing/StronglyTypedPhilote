using System;
using ATAP.Utilities.StronglyTypedID;
using Itenso.TimePeriod;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace ATAP.Utilities.Philote
{
    public interface IGuidPhilote<T> : IPhilote<T, Guid> where T : class{}
    public interface IIntPhilote<T> : IPhilote<T, int> where T : class{}

  public interface IPhilote<T, TValue> where T : class where TValue : notnull
  {
    IStronglyTypedId<TValue> ID { get; }
    ConcurrentDictionary<string, IStronglyTypedId<TValue>> AdditionalIDs { get; }
    IEnumerable<ITimeBlock> TimeBlocks { get; }
  }
}
