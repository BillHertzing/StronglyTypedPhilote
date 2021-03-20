using System;
using System.Collections.Concurrent;

using ATAP.Utilities.StronglyTypedIds;

using Newtonsoft.Json;

namespace ATAP.Utilities.StronglyTypedIds.JsonConverter.Newtonsoft.Shim {
  // Attribution https://thomaslevesque.com/2020/12/07/csharp-9-records-as-strongly-typed-ids-part-3-json-serialization/

  public class StronglyTypedIdJsonConverter : JsonConverter {
    private static readonly ConcurrentDictionary<Type, global::Newtonsoft.Json.JsonConverter> Cache = new();

    public override bool CanConvert(Type objectType) {
      return StronglyTypedIdHelper.IsStronglyTypedId(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
      var converter = GetConverter(objectType);
      return converter.ReadJson(reader, objectType, existingValue, serializer);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
      if (value is null) {
        writer.WriteNull();
      }
      else {
        var converter = GetConverter(value.GetType());
        converter.WriteJson(writer, value, serializer);
      }
    }

    private static Newtonsoft.Json.JsonConverter GetConverter(Type objectType) {
      return Cache.GetOrAdd(objectType, CreateConverter);
    }

    private static Newtonsoft.Json.JsonConverter CreateConverter(Type objectType) {
      if (!StronglyTypedIdHelper.IsStronglyTypedId(objectType, out var valueType)) {
        throw new InvalidOperationException($"Cannot create converter for '{objectType}'");
      }

      var type = typeof(StronglyTypedIdJsonConverter<,>).MakeGenericType(objectType, valueType);
      return (Newtonsoft.Json.JsonConverter)Activator.CreateInstance(type);
    }
  }

  public class StronglyTypedIdJsonConverter<TStronglyTypedId, TValue> : JsonConverter<TStronglyTypedId>
      where TStronglyTypedId : StronglyTypedId<TValue>
      where TValue : notnull {
    public override TStronglyTypedId ReadJson(JsonReader reader, Type objectType, TStronglyTypedId existingValue, bool hasExistingValue, JsonSerializer serializer) {
      if (reader.TokenType is JsonToken.Null) {
        return null;
      }

      var value = serializer.Deserialize<TValue>(reader);
      var factory = StronglyTypedIdHelper.GetFactory<TValue>(objectType);
      return (TStronglyTypedId)factory(value);
    }

    public override void WriteJson(JsonWriter writer, TStronglyTypedId value, JsonSerializer serializer) {
      if (value is null) {
        writer.WriteNull();
      }
      else {
        writer.WriteValue(value.Value);
      }
    }
  }
}
