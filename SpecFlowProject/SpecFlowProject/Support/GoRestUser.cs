using Newtonsoft.Json;
using static Bogus.DataSets.Name;

namespace GoRestTests.Support
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

    public class GoRestUserRequest
    {
  
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("gender")]
        public Gender Gender { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
