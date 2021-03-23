using System;

using System.Collections.Generic;
using System.Collections.Concurrent;
using ATAP.Utilities.StronglyTypedIds;
using Itenso.TimePeriod;
namespace ATAP.Utilities.Philote.IntegrationTests {

  public record GCommentId<TValue> : AbstractStronglyTypedId<TValue>, IStronglyTypedId<TValue> where TValue : notnull {
    public GCommentId(TValue value ) :base (value) {}
}
  public interface IGCommentPhilote<TValue> : IAbstractPhilote<GComment<TValue>, TValue> where TValue : notnull  {}
  public record GCommentPhilote<TValue> : AbstractPhilote<GCommentId<TValue>, TValue>, IAbstractPhilote<GComment<TValue>, TValue>, IGCommentPhilote<TValue>
    where TValue : notnull {
    public GCommentPhilote(IStronglyTypedId<TValue> iD = default, ConcurrentDictionary<string, IStronglyTypedId<TValue>>? additionalIDs = default, IEnumerable<ITimeBlock>? timeBlocks = default) {
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

  }

  public interface IGComment<TValue> where TValue : notnull {
    IEnumerable<string> GStatements { get; init; }
    IGCommentPhilote<TValue> Philote { get; init; }
  }

  public record GComment<TValue> : IGComment<TValue> where TValue : notnull {
    // public GComment(IEnumerable<string> gStatements = default)  {
    //   GStatements = gStatements == default ? new List<string>() : gStatements;
    //   Philote = new GCommentPhilote<TValue>();
    // }
    public GComment(IEnumerable<string> gStatements = default, IGCommentPhilote<TValue> philote = default )  {
      GStatements = gStatements == default ? new List<string>() : gStatements;
      Philote = philote == default? new GCommentPhilote<TValue>() : philote;
    }
    public IEnumerable<string> GStatements { get; init; }
    public IGCommentPhilote<TValue>  Philote { get; init; }
  }

  public interface IGBodyPhilote<TValue> : IAbstractPhilote<GBody<TValue>, TValue> where TValue : notnull  {}
  public record GBodyPhilote<TValue> : AbstractPhilote<GBody<TValue>, TValue>, IGBodyPhilote<TValue> where TValue : notnull   {}

  public interface IGBody<TValue> where TValue : notnull {
    IEnumerable<string> GStatements { get; init; }
    IGComment<TValue> GComment { get; init; }
    GBodyPhilote<TValue>  Philote { get; init; }
  }

  public class GBody<TValue> : IGBody<TValue> where TValue : notnull {
    public GBody(IEnumerable<string> gStatements = default, IGComment<TValue> gComment = default
    ) {
      GStatements = gStatements == default ? new List<string>() : gStatements;
      GComment = gComment == default ? new GComment<TValue>() : gComment;
      Philote = new GBodyPhilote<TValue>();
    }
    public IGComment<TValue> GComment { get; init; }
    public IEnumerable<string> GStatements { get; init; }
    public GBodyPhilote<TValue> Philote { get; init; }
  }
}
