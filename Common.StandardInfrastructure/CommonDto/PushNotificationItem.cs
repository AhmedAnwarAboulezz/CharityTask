using Newtonsoft.Json;

namespace Common.StandardInfrastructure.CommonDto
{
    public class PushNotificationItem
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
    }

    public class PushToTopicOrOneDevice
    {
        [JsonProperty("to")]
        public string To { get; set; }
        [JsonProperty("notification")]
        public PushNotificationItem Notification { get; set; }
    }
    public class PushToMultipleDevice
    {
        [JsonProperty("registration_ids")]
        public string[] RegistrationIds { get; set; }
        [JsonProperty("notification")]
        public PushNotificationItem Notification { get; set; }
    }
}
