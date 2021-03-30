using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using ATAP.Utilities.StronglyTypedIds;

using Itenso.TimePeriod;
namespace ATAP.Utilities.Philote.IntegrationTests {

  public record GCommentId<TValue> : AbstractStronglyTypedId<TValue>, IAbstractStronglyTypedId<TValue> where TValue : notnull {
    public GCommentId() : base() { }
    public GCommentId(TValue value) : base(value) { }
  }
  public interface IGCommentPhilote<TValue> : IAbstractPhilote<GCommentId<TValue>, TValue> where TValue : notnull { }
  public record GCommentPhilote<TValue> : AbstractPhilote<GCommentId<TValue>, TValue>, IAbstractPhilote<GCommentId<TValue>, TValue>, IGCommentPhilote<TValue>
    where TValue : notnull {
    public GCommentPhilote(GCommentId<TValue> iD = default, ConcurrentDictionary<string, IAbstractStronglyTypedId<TValue>>? additionalIDs = default, IEnumerable<ITimeBlock>? timeBlocks = default) : base(iD, additionalIDs, timeBlocks) { }
  }

  public interface IGComment<TValue> where TValue : notnull {
    IEnumerable<string> GStatements { get; init; }
    IAbstractPhilote<GCommentId<TValue> ,TValue> Philote { get; init; }
  }

  public record GComment<TValue> : IGComment<TValue> where TValue : notnull {
    public GComment(IEnumerable<string> gStatements = default, IGCommentPhilote<TValue> philote = default) {
      GStatements = gStatements == default ? new List<string>() : gStatements;
      Philote = philote == default ? new GCommentPhilote<TValue>() : philote;
    }
    public IEnumerable<string> GStatements { get; init; }
    public IAbstractPhilote<GCommentId<TValue> ,TValue> Philote { get; init; }
  }

  public record GBodyId<TValue> : AbstractStronglyTypedId<TValue>, IAbstractStronglyTypedId<TValue> where TValue : notnull {
    public GBodyId() : base() { }
    public GBodyId(TValue value) : base(value) { }
  }

  public interface IGBodyPhilote<TValue> : IAbstractPhilote<GBodyId<TValue>, TValue> where TValue : notnull { }
  public record GBodyPhilote<TValue> : AbstractPhilote<GBodyId<TValue>, TValue>, IGBodyPhilote<TValue> where TValue : notnull {
    public GBodyPhilote(GBodyId<TValue> iD = default, ConcurrentDictionary<string, IAbstractStronglyTypedId<TValue>>? additionalIDs = default, IEnumerable<ITimeBlock>? timeBlocks = default) : base(iD, additionalIDs, timeBlocks) { }
    //   if (iD != default) { ID = iD; }
    //   else {
    //     ID = (typeof(TValue)) switch {
    //       Type intType when typeof(TValue) == typeof(int) => new GBodyId<int>() { Value = new Random().Next() } as GBodyId<TValue>,
    //       Type GuidType when typeof(TValue) == typeof(Guid) => new GBodyId<Guid>() as GBodyId<TValue>,
    //       // ToDo: replace with new custom exception and localization of exception message
    //       _ => throw new Exception(FormattableString.Invariant($"Invalid TValue type {typeof(TValue)}")),

    //     };
    //   }
    //   if (additionalIDs != default) {
    //     AdditionalIDs = new ConcurrentDictionary<string, IAbstractStronglyTypedId<TValue>>(additionalIDs.Select(kvp => new KeyValuePair<string, IAbstractStronglyTypedId<TValue>>(kvp.Key, (IAbstractStronglyTypedId<TValue>)kvp.Value)));
    //   }
    //   else {
    //     AdditionalIDs = new ConcurrentDictionary<string, IAbstractStronglyTypedId<TValue>>();
    //   }
    //   TimeBlocks = timeBlocks != default ? timeBlocks : new List<ITimeBlock>();
    // }
  }

  public interface IGBody<TValue> where TValue : notnull {
    IEnumerable<string> GStatements { get; init; }
    IGComment<TValue> GComment { get; init; }
    IAbstractPhilote<GBodyId<TValue> ,TValue> Philote { get; init; }
  }

  public class GBody<TValue> : IGBody<TValue> where TValue : notnull {
    public GBody(IEnumerable<string> gStatements = default, IGComment<TValue> gComment = default
    ) {
      GStatements = gStatements == default ? new List<string>() : gStatements;
      GComment = gComment == default ? new GComment<TValue>() : gComment;
      Philote = (IAbstractPhilote<GBodyId<TValue> ,TValue>) new GBodyPhilote<TValue>();
    }
    public IGComment<TValue> GComment { get; init; }
    public IEnumerable<string> GStatements { get; init; }
    public IAbstractPhilote<GBodyId<TValue> ,TValue> Philote { get; init; }
  }
}
