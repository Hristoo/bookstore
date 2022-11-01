using Newtonsoft.Json;

namespace Hristo.GoRestSpecFlow.Core.Support
{
    public class GoRestUser
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public string Email { get; set; }
        [JsonProperty]
        public string Gender { get; set; }
        [JsonProperty]
        public string status { get; set; }
    }
}
