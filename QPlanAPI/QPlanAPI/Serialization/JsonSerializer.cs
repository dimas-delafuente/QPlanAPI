using System.Text.Json;
using System.Text.Encodings.Web;
namespace QPlanAPI.Serialization
{
    public sealed class QPlanJsonSerializer
    {
        private static readonly JsonSerializerOptions _settings = new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Default
        };

        public static string SerializeObject(object o)
        {
            return JsonSerializer.Serialize(o, _settings);
        }
    }
}
