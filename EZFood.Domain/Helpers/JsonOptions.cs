using System.Text.Json;
using System.Text.Json.Serialization;

namespace EZFood.Domain.Helpers;
public static class JsonOptions
{
    public static readonly JsonSerializerOptions DefaultSerializerOptions = new(JsonSerializerDefaults.Web)
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new JsonStringEnumConverter() }
    };

    public static readonly JsonSerializerOptions PropertyNameCaseInsensitiveOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNameCaseInsensitive = true,
        IncludeFields = true,
    };

    public static List<string>? ListData(string? JsonData)
    {
        return JsonData != null ? JsonSerializer.Deserialize<List<string>>(JsonData, JsonOptions.DefaultSerializerOptions) : new();
    }

    public static string ListDataObject<T>(T ListDetails)
    {
        return JsonSerializer.Serialize<T>(ListDetails, JsonOptions.DefaultSerializerOptions);
    }
}
