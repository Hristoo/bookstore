using Microsoft.Extensions.Configuration;

namespace GoRestTests.Support
{
    public class BaseConfig
    {
        public BaseConfig()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("testConfig.json")
                .Build();

            HttpClientConfig = config.GetSection("HttpClient").Get<HttpClientConfig>();
            HttpClientConfig2 = config.GetSection("HttpClient2").Get<HttpClientConfig>();
        }
        public HttpClientConfig HttpClientConfig { get; set; }
        public HttpClientConfig HttpClientConfig2 { get; set; }

    }
}
