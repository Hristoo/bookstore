using Microsoft.Extensions.Configuration;

namespace Hristo.GoRestSpecFlow.Core.Support
{
    public class BaseConfig
    {
        public BaseConfig()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();

            HttpClient = config.GetSection("HttpClient").Get<HttpClientConfig>();
        }
        public HttpClientConfig HttpClient { get; set; }
    }
}
