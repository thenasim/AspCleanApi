using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Common.Converters;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (DateOnly.TryParse(reader.GetString(), out var dateOnly))
        {
            return dateOnly;
        }

        throw new JsonException("Failed to serialize date");
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("O"));
    }
}
