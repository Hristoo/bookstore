using System.Net;
using System.Text;
using FluentAssertions.Execution;
using Hristo.GoRestSpecFlow.Core.Support;
using Newtonsoft.Json;
using TechTalk.SpecFlow.Assist;

namespace Hristo.GoRestSpecFlow.Tests.StepDefinitions
{
    [Binding]
    public class UsersStepsBadPath
    {
        private HttpClient _httpClient = new HttpClient();
        private readonly BaseConfig _baseConfig;
        private GoRestUserRequest _user;
        private TestContextContainer _context;
        private HttpResponseMessage _response;

        public UsersStepsBadPath(BaseConfig baseConfig, TestContextContainer context)
        {
            _baseConfig = baseConfig;
            _context = context;
        }

        [Given(@"I wat to prepare a bad request")]
        public void GivenIWatToPrepareABadRequest()
        {
            
        }

        [When(@"I try get all users from the (.*) endpoint")]
        public void WhenITryGetAllUsersFromTheUsersEndpoint(string endpoint)
        {
            _response = _httpClient.GetAsync($"{_baseConfig.HttpClient.BaseUrl}{endpoint}hhgggg").Result;
        }

        [Then(@"The response staus should be (.*)")]
        public void ThenTheResponseStausShouldBeBadRequest(string statusCode)
        {
            _response.StatusCode.ToString().Should().Be(statusCode);
        }


        // Try to create already existing user

        [Given(@"I have already existing following user data")]
        public void GivenIHaveAlreadyExistingFollowingUserData(Table table)
        {
            _user = table.CreateInstance<GoRestUserRequest>();
        }

        [When(@"I send a wrong request to the (.*) endpoint")]
        public void WhenISendAWrongRequestToTheUsersEndpoint(string endpoint)
        {
            var request = JsonConvert.SerializeObject(_user);
            var requestBody = new StringContent(request, Encoding.UTF8, "application/json");

            var msgBody = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_baseConfig.HttpClient.BaseUrl}{endpoint}"),
                Content = requestBody,
            };

            _response = _context.HttpClient.SendAsync(msgBody).Result;
        }

        [Then(@"The user should be created (.*)")]
        public void ThenTheUserShouldBeCreatedCreated(string statusCode)
        {
            _response.StatusCode.ToString().Should().Be(statusCode);
        }


    }
}
