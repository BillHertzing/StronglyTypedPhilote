using System;

using System.Collections.Concurrent;


namespace ATAP.Utilities.StronglyTypedID {
  // Attribution for IdAsStruct<T>(earlier): taken from answers provided to this question: https://stackoverflow.com/questions/53748675/strongly-typed-guid-as-generic-struct
  // Modifications:  CheckValue and all references removed, because our use case requires Guid.Empty to be a valid value
  // Attribution 1/8/2021:[Using C# 9 records as strongly-typed ids](https://thomaslevesque.com/2020/10/30/using-csharp-9-records-as-strongly-typed-ids/)


  public record GuidStronglyTypedId : StronglyTypedId<Guid>, IGuidStronglyTypedId {
    public GuidStronglyTypedId(Guid value) :base(value){}
  }
  public record IntStronglyTypedId : StronglyTypedId<int>, IIntStronglyTypedId {
        public IntStronglyTypedId(int value) :base(value){}
   }
  public abstract record StronglyTypedId<TValue> : IStronglyTypedId<TValue> where TValue : notnull {
    public TValue Value { get; init; }
    public override string ToString() => Value.ToString();
    //public StronglyTypedId() { }
    // ToDo: figure out how to generate a random value for the StronglyTypedId in the parameterless constructor
    public StronglyTypedId() {
      Value = (typeof(TValue)) switch {
        Type inttype when typeof(TValue) == typeof(int) =>  (TValue)(object)new Random().Next(),
        Type guidtype when typeof(TValue) == typeof(Guid) => (TValue)(object)Guid.NewGuid(),
        _ => throw new Exception(String.Format("Invalid TValue type {0}", typeof(TValue)))
      };
     }
    public StronglyTypedId(TValue value) {
      Value = value;
    }

    public static bool AllowedTValue() {
      return (typeof(TValue)) switch {
        Type intType when intType == typeof(int) => true,
        Type GuidType when GuidType == typeof(Guid) => true,
        _ => false,
      };
    }
    /*
    private static TValue RandomTValue() {
      if (!AllowedTValue()) {
        throw new Exception(String.Format("Invalid TValue type {0}", typeof(TValue)));
      }
      return (typeof(TValue)) switch {
        Type intType when intType == typeof(int) => new Random().Next as TValue; // Compiletime error
            Type GuidType when GuidType == typeof(Guid) => (TValue)Guid.NewGuid(), // Compiletime error
      };
    }
    */
  }

/*
  public struct IdAsStruct<T> : IEquatable<IdAsStruct<T>>, IIdAsStruct<T> {
    private readonly Guid _value;

    public IdAsStruct(string value) {
      bool success;
      string iValue;
      if (string.IsNullOrEmpty(value)) {
        _value = Guid.NewGuid();
      }
      else {
        // Hack, used because only ServiceStack Json serializers add extra enclosing ".
        //  but, neither simpleJson nor NewtonSoft will serialize this at all
        iValue = value.Trim('"');
        success = Guid.TryParse(iValue, out Guid newValue);
        if (!success) { throw new NotSupportedException($"Guid.TryParse failed, value {value} cannot be parsed as a GUID"); }
        _value = newValue;
      }
    }

    public IdAsStruct(Guid value) {
      _value = value;
    }

    public override bool Equals(object obj) {
      return obj is IdAsStruct<T> id && Equals(id);
    }

    public bool Equals(IdAsStruct<T> other) {
      return _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();

    public override string ToString() {
      return _value.ToString();
    }

    public static bool operator ==(IdAsStruct<T> left, IdAsStruct<T> right) {
      return left.Equals(right);
    }

    public static bool operator !=(IdAsStruct<T> left, IdAsStruct<T> right) {
      return !(left == right);
    }
  }
  */
}
