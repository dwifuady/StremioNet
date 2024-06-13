using System.Text.Json;

namespace StremioNet.Helper;

public static class JsonParser
{
    public static bool TryParseJson<T>(string? json, out T? result)
    {
        if (json == null)
        {
            result = default;
            return false;
        }
        try
        {
            result = JsonSerializer.Deserialize<T>(json);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }
}