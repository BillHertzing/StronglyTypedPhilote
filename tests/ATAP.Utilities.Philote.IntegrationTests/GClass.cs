using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using ATAP.Utilities.StronglyTypedIds;

using Itenso.TimePeriod;
namespace ATAP.Utilities.Philote.IntegrationTests {

  public record GCommentId<TValue> : AbstractStronglyTypedId<TValue>, IStronglyTypedId<TValue> where TValue : notnull {
    public GCommentId() : base() { }
    public GCommentId(TValue value) : base(value) { }
  }
  public interface IGCommentPhilote<TValue> : IAbstractPhilote<GCommentId<TValue>, TValue> where TValue : notnull { }
  public record GCommentPhilote<TValue> : AbstractPhilote<GCommentId<TValue>, TValue>, IAbstractPhilote<GCommentId<TValue>, TValue>, IGCommentPhilote<TValue>
    where TValue : notnull {
    // public GCommentPhilote(int iD = default, ConcurrentDictionary<string, GCommentId<TValue>>? additionalIDs = default, IEnumerable<ITimeBlock>? timeBlocks = default) {
    //   if (iD != default) { ID = new GCommentId<int>() {Value = iD}; }
    //   else {
    //     ID = new GCommentId<int>() {Value = new Random().Next()};
    //   }
    //   AdditionalIDs = additionalIDs != default ? additionalIDs : new ConcurrentDictionary<string, IStronglyTypedId<TValue>>();
    //   TimeBlocks = timeBlocks != default ? timeBlocks : new List<ITimeBlock>();
    // }
    // public GCommentPhilote(Guid? iD = default, ConcurrentDictionary<string, GCommentId<TValue>>? additionalIDs = default, IEnumerable<ITimeBlock>? timeBlocks = default) {
    //   if (iD != default) { ID = iD; }
    //   else {
    //     ID = new GCommentId<Guid>() {Value = Guid.NewGuid()};
    //   }
    //   AdditionalIDs = additionalIDs != default ? additionalIDs : new ConcurrentDictionary<string, IStronglyTypedId<TValue>>();
    //   TimeBlocks = timeBlocks != default ? timeBlocks : new List<ITimeBlock>();
    // }

    public GCommentPhilote(GCommentId<TValue> iD = default, ConcurrentDictionary<string, IStronglyTypedId<TValue>>? additionalIDs = default, IEnumerable<ITimeBlock>? timeBlocks = default) {
      if (iD != default) { ID = iD; }
      else {
        ID = (typeof(TValue)) switch {
          Type intType when typeof(TValue) == typeof(int) => new GCommentId<int>() { Value = new Random().Next() } as GCommentId<TValue>,
          Type GuidType when typeof(TValue) == typeof(Guid) => new GCommentId<Guid>() { Value = Guid.NewGuid() } as GCommentId<TValue>,
          // ToDo: replace with new custom exception and localization of exception message
          _ => throw new Exception(FormattableString.Invariant($"Invalid TValue type {typeof(TValue)}")),

        };
      }
      // Attribution [Linq ToDictionary will not implicitly convert class to interface](https://stackoverflow.com/questions/25136049/linq-todictionary-will-not-implicitly-convert-class-to-interface) Educational but ultimately fails
      // The ToDictionary extension method available in LINQ for generic Dictionaries is NOT availabe for ConcurrentDictionaries, the following won't work...
      //  additionalIDs.ToDictionary(kvp => kvp.Key, kvp => (IStronglyTypedId<TValue>) kvp.Value)
      // A this is a concurrent operation we will need to put a semaphore around the argument passed in
      // attribution [How do you convert a dictionary to a ConcurrentDictionary?](https://stackoverflow.com/questions/27063889/how-do-you-convert-a-dictionary-to-a-concurrentdictionary) from a comment on a question, contributed by Panagiotis Kanavos
      // we have to convert the parameter's value to a cast to a less derived interface
      if (additionalIDs != default) {
      // ToDo : add write semaphore around the parameter before enumerating the Dictionary
        AdditionalIDs = new ConcurrentDictionary<string, IStronglyTypedId<TValue>>(additionalIDs.Select(kvp => new KeyValuePair<string, IStronglyTypedId<TValue>>(kvp.Key, (IStronglyTypedId<TValue>)kvp.Value)));
      }
      else {
        AdditionalIDs = new ConcurrentDictionary<string, IStronglyTypedId<TValue>>();
      }
      TimeBlocks = timeBlocks != default ? timeBlocks : new List<ITimeBlock>();
    }
  }

  public interface IGComment<TValue> where TValue : notnull {
    IEnumerable<string> GStatements { get; init; }
    IAbstractPhilote<GCommentId<TValue> ,TValue> Philote { get; init; }
  }

  public record GComment<TValue> : IGComment<TValue> where TValue : notnull {
    // public GComment(IEnumerable<string> gStatements = default)  {
    //   GStatements = gStatements == default ? new List<string>() : gStatements;
    //   Philote = new GCommentPhilote<TValue>();
    // }
    public GComment(IEnumerable<string> gStatements = default, IGCommentPhilote<TValue> philote = default) {
      GStatements = gStatements == default ? new List<string>() : gStatements;
      Philote = philote == default ? new GCommentPhilote<TValue>() : philote;
    }
    public IEnumerable<string> GStatements { get; init; }
    public IAbstractPhilote<GCommentId<TValue> ,TValue> Philote { get; init; }
  }

  public record GBodyId<TValue> : AbstractStronglyTypedId<TValue>, IStronglyTypedId<TValue> where TValue : notnull {
    public GBodyId() : base() { }
    public GBodyId(TValue value) : base(value) { }
  }

  public interface IGBodyPhilote<TValue> : IAbstractPhilote<GBodyId<TValue>, TValue> where TValue : notnull { }
  public record GBodyPhilote<TValue> : AbstractPhilote<GBodyId<TValue>, TValue>, IGBodyPhilote<TValue> where TValue : notnull {
    public GBodyPhilote(GBodyId<TValue> iD = default, ConcurrentDictionary<string, GBodyId<TValue>>? additionalIDs = default, IEnumerable<ITimeBlock>? timeBlocks = default) {
      if (iD != default) { ID = iD; }
      else {
        ID = (typeof(TValue)) switch {
          Type intType when typeof(TValue) == typeof(int) => new GBodyId<int>() { Value = new Random().Next() } as GBodyId<TValue>,
          Type GuidType when typeof(TValue) == typeof(Guid) => new GBodyId<Guid>() { Value = Guid.NewGuid() } as GBodyId<TValue>,
          // ToDo: replace with new custom exception and localization of exception message
          _ => throw new Exception(FormattableString.Invariant($"Invalid TValue type {typeof(TValue)}")),

        };
      }
      if (additionalIDs != default) {
        AdditionalIDs = new ConcurrentDictionary<string, IStronglyTypedId<TValue>>(additionalIDs.Select(kvp => new KeyValuePair<string, IStronglyTypedId<TValue>>(kvp.Key, (IStronglyTypedId<TValue>)kvp.Value)));
      }
      else {
        AdditionalIDs = new ConcurrentDictionary<string, IStronglyTypedId<TValue>>();
      }
      TimeBlocks = timeBlocks != default ? timeBlocks : new List<ITimeBlock>();
    }
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
