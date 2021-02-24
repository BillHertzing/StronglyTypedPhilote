using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using ATAP.Utilities.StronglyTypedId;
using Itenso.TimePeriod;

namespace ATAP.Utilities.Philote {

  public record GuidPhilote<T> : Philote<T, Guid> where T : class, IGuidPhilote<T> {
    public GuidPhilote(Guid value) : base(value) { }
    public GuidPhilote() : base() { }
    public override string ToString() => base.ToString();

  }
  
  public record IntPhilote<T> : Philote<T, int> where T : class, IIntPhilote<T> {
    public IntPhilote(int value) : base(value) { }
    public IntPhilote() : base() { }
    public override string ToString() => base.ToString();
  }

  public abstract record Philote<T, TValue> : IPhilote<T, TValue> where T : class where TValue : notnull {

    public Philote(IStronglyTypedId<TValue> iD = default, ConcurrentDictionary<string, IStronglyTypedId<TValue>> additionalIDs = default, IEnumerable<ITimeBlock> timeBlocks = default) {
      if (iD != default) { ID = iD; }
      else {
        ID = (typeof(TValue)) switch {
          Type intType when typeof(TValue) == typeof(int) => (IStronglyTypedId<TValue>)new IntStronglyTypedId() { Value = new Random().Next() },
          Type GuidType when typeof(TValue) == typeof(Guid) => (IStronglyTypedId<TValue>)new GuidStronglyTypedId() { Value = Guid.NewGuid() },
          _ => throw new Exception(String.Format("Invalid TValue type {0}", typeof(TValue))),
        };
      }
      AdditionalIDs = additionalIDs != default ? additionalIDs : new ConcurrentDictionary<string, IStronglyTypedId<TValue>>();
      TimeBlocks = timeBlocks != default ? timeBlocks : new List<ITimeBlock>();
    }

    public IStronglyTypedId<TValue> ID { get; init; }
    public ConcurrentDictionary<string, IStronglyTypedId<TValue>> AdditionalIDs { get; init; }
    public IEnumerable<ITimeBlock> TimeBlocks { get; init; }
  }
}
