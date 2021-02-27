using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using ATAP.Utilities.StronglyTypedID;
using Itenso.TimePeriod;

namespace ATAP.Utilities.Philote {

  // public abstract record GuidPhilote<T> : AbstractPhilote<T, Guid> where T : class, IGuidPhilote<T> {
  //   public GuidPhilote(Guid value) : base(value) {
  //   }
  //   public GuidPhilote() : base() { }
  //   public override string ToString() => base.ToString();

  // }

  // public abstract record IntPhilote<T> : AbstractPhilote<T, int> where T : class, IIntPhilote<T> {
  //   public IntPhilote(int value) : base(value) { }
  //   public IntPhilote() : base() { }
  //   public override string ToString() => base.ToString();
  // }

  public abstract record AbstractPhilote<T, TValue> : IAbstractPhilote<T, TValue> where T : class where TValue : notnull {

    public AbstractPhilote(IStronglyTypedId<TValue> iD = default, ConcurrentDictionary<string, IStronglyTypedId<TValue>>? additionalIDs = default, IEnumerable<ITimeBlock>? timeBlocks = default) {
      if (iD != default) { ID = iD; }
      else {
        ID = (typeof(TValue)) switch {
          Type intType when typeof(TValue) == typeof(int) => (IStronglyTypedId<TValue>)new IntStronglyTypedId() { Value = new Random().Next() },
          Type GuidType when typeof(TValue) == typeof(Guid) => (IStronglyTypedId<TValue>)new GuidStronglyTypedId() { Value = Guid.NewGuid() },
          // ToDo: replace with new custom exception and localization of exception message
          _ => throw new Exception(FormattableString.Invariant($"Invalid TValue type {typeof(TValue)}")),

        };
      }
      AdditionalIDs = additionalIDs != default ? additionalIDs : new ConcurrentDictionary<string, IStronglyTypedId<TValue>>();
      TimeBlocks = timeBlocks != default ? timeBlocks : new List<ITimeBlock>();
    }

    public IStronglyTypedId<TValue> ID { get; init; }
    public ConcurrentDictionary<string, IStronglyTypedId<TValue>>? AdditionalIDs { get; init; }
    public IEnumerable<ITimeBlock>? TimeBlocks { get; init; }
  }
}
