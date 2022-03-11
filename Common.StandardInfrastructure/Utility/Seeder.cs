using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Common.StandardInfrastructure.Utility
{
    public static class Seeder<T> where T : class
    {
        public static T Seedit(string jsonData)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterContractResolver()
            };
            return JsonConvert.DeserializeObject<T>(jsonData, settings);
        }
    }
}
