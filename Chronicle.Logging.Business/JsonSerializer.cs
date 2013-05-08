using Newtonsoft.Json;

namespace Chronicle.Logging.Business
{
    public static class JsonSerializer
    {
        public static string Serialize(object o)
        {
            var settings = new JsonSerializerSettings
                {PreserveReferencesHandling = PreserveReferencesHandling.Objects};
            return JsonConvert.SerializeObject(o, Formatting.Indented, settings);
        }
    }
}