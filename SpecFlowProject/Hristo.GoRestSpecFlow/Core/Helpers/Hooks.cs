using Hristo.GoRestSpecFlow.Core.Support;
using TechTalk.SpecFlow.Infrastructure;

namespace Hristo.GoRestSpecFlow.Core.Helpers
{
    [Binding]
    public sealed class Hooks
    {
        private readonly ISpecFlowOutputHelper _outputHelper;
        private TestContextContainer _testContextContainer;
        private BaseConfig _baseConfig;

        public Hooks(ISpecFlowOutputHelper outputHelper, TestContextContainer testContextContainer, BaseConfig baseConfig)
        {
            _outputHelper = outputHelper;
            _testContextContainer = testContextContainer;
            _baseConfig = baseConfig;
        }

        [BeforeScenario]
        public void HttpClient()
        {
            _testContextContainer.HttpClient = new HttpClient();
        }

        [BeforeScenario("Authenticate")]
        public void Authenticate()
        {
            _testContextContainer.HttpClient.DefaultRequestHeaders.Add("Authorization", _baseConfig.HttpClient.Token);
        }
    }
}
