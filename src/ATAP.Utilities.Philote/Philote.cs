using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using ATAP.Utilities.StronglyTypedIds;
using Itenso.TimePeriod;

namespace ATAP.Utilities.Philote {

  public abstract record AbstractGuidPhilote<TId> : AbstractPhilote<TId, Guid> where TId : AbstractStronglyTypedId<Guid>, new() {
    public AbstractGuidPhilote(Guid value) : base(value) { }
    public AbstractGuidPhilote(TId iD = default, ConcurrentDictionary<string, IStronglyTypedId<Guid>>? additionalIDs = default, IEnumerable<ITimeBlock>? timeBlocks = default) : base(iD, additionalIDs, timeBlocks) { }
    public override string ToString() => base.ToString();
  }

  public abstract record AbstractIntPhilote<TId> : AbstractPhilote<TId, int> where TId : AbstractStronglyTypedId<int>, new() {
    public AbstractIntPhilote(TId iD = default, ConcurrentDictionary<string, IStronglyTypedId<int>>? additionalIDs = default, IEnumerable<ITimeBlock>? timeBlocks = default) : base(iD, additionalIDs, timeBlocks) { }
    public AbstractIntPhilote(int value) : base(value) { }
    public override string ToString() => base.ToString();
  }

  public abstract record AbstractPhilote<TId, TValue> : IAbstractPhilote<TId, TValue> where TId : AbstractStronglyTypedId<TValue>, new() where TValue : notnull {

    public AbstractPhilote(int? iD = default, ConcurrentDictionary<string, IStronglyTypedId<TValue>>? additionalIDs = default, IEnumerable<ITimeBlock>? timeBlocks = default) {
      if (iD != null) {
        ID = Activator.CreateInstance(typeof(TId), new object[] { iD }) as TId;
      }
      else {
        ID = (TId)(object)(AbstractStronglyTypedId<int>)new IntStronglyTypedId() { Value = new Random().Next() };
      }
      AdditionalIDs = additionalIDs != default ? additionalIDs : new ConcurrentDictionary<string, IStronglyTypedId<TValue>>();
      TimeBlocks = timeBlocks != default ? timeBlocks : new List<ITimeBlock>();
    }
    public AbstractPhilote(Guid? iD = default, ConcurrentDictionary<string, IStronglyTypedId<TValue>>? additionalIDs = default, IEnumerable<ITimeBlock>? timeBlocks = default) {
      if (iD != null) {
        ID = Activator.CreateInstance(typeof(TId), new object[] { iD }) as TId;
      }
      else {
        ID = (TId)(object)(AbstractStronglyTypedId<Guid>)new GuidStronglyTypedId() { Value = Guid.NewGuid() };
      }
      AdditionalIDs = additionalIDs != default ? additionalIDs : new ConcurrentDictionary<string, IStronglyTypedId<TValue>>();
      TimeBlocks = timeBlocks != default ? timeBlocks : new List<ITimeBlock>();
    }

    public AbstractPhilote(TId iD = default, ConcurrentDictionary<string, IStronglyTypedId<TValue>>? additionalIDs = default, IEnumerable<ITimeBlock>? timeBlocks = default) {
      if (iD != null) { ID = iD; }
      else {
        ID = (typeof(TValue)) switch {

        Type intType when typeof(TValue) == typeof(int) => new IntStronglyTypedId() { Value = new Random().Next() } as TId,
        Type GuidType when typeof(TValue) == typeof(Guid) => Activator.CreateInstance(typeof(Guid), new object[] { Guid.NewGuid() }) as TId,
          //Type intType when typeof(TValue) == typeof(int) => (TId)(object)(AbstractStronglyTypedId<int>)new IntStronglyTypedId() { Value = new Random().Next() },
          //Type GuidType when typeof(TValue) == typeof(Guid) => (TId)(object)(AbstractStronglyTypedId<Guid>)new GuidStronglyTypedId() { Value = Guid.NewGuid() },
          // ToDo: replace with new custom exception and localization of exception message
          _ => throw new Exception(FormattableString.Invariant($"Invalid TValue type {typeof(TValue)}")),

        };
      }
      AdditionalIDs = additionalIDs != default ? additionalIDs : new ConcurrentDictionary<string, IStronglyTypedId<TValue>>();
      TimeBlocks = timeBlocks != default ? timeBlocks : new List<ITimeBlock>();
    }

    public TId ID { get; init; }
    public ConcurrentDictionary<string, IStronglyTypedId<TValue>>? AdditionalIDs { get; init; }
    public IEnumerable<ITimeBlock>? TimeBlocks { get; init; }
  }
}
