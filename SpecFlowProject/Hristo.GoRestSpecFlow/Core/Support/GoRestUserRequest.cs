using Newtonsoft.Json;

namespace Hristo.GoRestSpecFlow.Core.Support
{
    public class GoRestUserRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
