using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Concurrent;
using ATAP.Utilities.StronglyTypedID;

namespace ATAP.Utilities.StronglyTypedIDs.JsonConverterSystemTextJson {
  // Attribution https://thomaslevesque.com/2020/12/07/csharp-9-records-as-strongly-typed-ids-part-3-json-serialization/

  public class StronglyTypedIdJsonConverter<TStronglyTypedId, TValue> : JsonConverter<TStronglyTypedId>
      where TStronglyTypedId : IStronglyTypedId<TValue>
      where TValue : notnull {
    public override TStronglyTypedId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
      if (reader.TokenType is JsonTokenType.Null) {
        return default;
      }

      var value = JsonSerializer.Deserialize<TValue>(ref reader, options);
      var factory = StronglyTypedIdHelper.GetFactory<TValue>(typeToConvert);
      return (TStronglyTypedId)factory(value);
    }

    public override void Write(Utf8JsonWriter writer, TStronglyTypedId value, JsonSerializerOptions options) {
      if (value is null) {
        writer.WriteNullValue();
      }
      else {
        JsonSerializer.Serialize(writer, value.Value, options);
      }
    }
  }

  public class StronglyTypedIdJsonConverterFactory : JsonConverterFactory {
    private static readonly ConcurrentDictionary<Type, System.Text.Json.Serialization.JsonConverter> Cache = new();

    public override bool CanConvert(Type typeToConvert) {
      return StronglyTypedIdHelper.IsStronglyTypedId(typeToConvert);
    }

    public override System.Text.Json.Serialization.JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) {
      return Cache.GetOrAdd(typeToConvert, CreateConverter);
    }

    private static System.Text.Json.Serialization.JsonConverter CreateConverter(Type typeToConvert) {
      if (!StronglyTypedIdHelper.IsStronglyTypedId(typeToConvert, out var valueType)) {
        throw new InvalidOperationException($"Cannot create converter for '{typeToConvert}'");
      }

      var type = typeof(StronglyTypedIdJsonConverter<,>).MakeGenericType(typeToConvert, valueType);
      return (System.Text.Json.Serialization.JsonConverter)Activator.CreateInstance(type);
    }
  }
}
