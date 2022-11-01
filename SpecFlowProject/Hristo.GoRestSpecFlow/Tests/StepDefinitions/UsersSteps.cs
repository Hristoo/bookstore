using FluentAssertions.Execution;
using System.Text;
using Hristo.GoRestSpecFlow.Core.Support;
using Newtonsoft.Json;
using TechTalk.SpecFlow.Assist;

namespace Hristo.GoRestSpecFlow.Tests.StepDefinitions
{
    [Binding]
    public class UsersSteps
    {
        private HttpClient _httpClient = new HttpClient();
        private readonly BaseConfig _baseConfig;
        private GoRestUserRequest _user;
        private TestContextContainer _context;
        private HttpResponseMessage _response;
        private GoRestUser _goRestUser;
        private UserContainer _userContainer;

        public UsersSteps(BaseConfig baseConfig, TestContextContainer context)
        {
            _baseConfig = baseConfig;
            _context = context;
        }


        [Given(@"I wat to prepare a request")]
        public void GivenIWatToPrepareARequest()
        {
            //_httpClient.DefaultRequestHeaders.Add("Authorization", _baseConfig.HttpClient.Token);
        }

        [When(@"I get all users from the (.*) endpoint")]
        public void WhenIGetAllUsersFromTheUsersEndpoint(string endpoint)
        {
            _response = _httpClient.GetAsync($"{_baseConfig.HttpClient.BaseUrl}{endpoint}").Result;
        }

        [Then(@"The response staus code should be (.*)")]
        public void ThenTheResponseStausCodeShouldBeOK(string statusCode)
        {
            _response.StatusCode.ToString().Should().Be(statusCode);
        }

        [Then(@"the response should contain a list of users")]
        public void ThenTheResponseShouldContainAListOfUsers()
        {
            var content = _response.Content.ReadAsStringAsync().Result;
            var expectedResponse = JsonConvert.DeserializeObject<List<GoRestUser>>(content);
            expectedResponse.Should().NotBeEmpty();
        }

        // Create new user 

        [Given(@"I have following user data")]
        public void GivenIHaveFollowingUserData(Table table)
        {
            _user = table.CreateInstance<GoRestUserRequest>();
        }

        [When(@"I send a request to the (.*) endpoint")]
        public void WhenISendARequestToTheUsersEndpoint(string endpoint)
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

        [Then(@"The user should be created successfully")]
        public void ThenTheUserrShouldBeCreatedSuccessfully()
        {
            var actualResponse = JsonConvert.DeserializeObject<GoRestUser>(_response.Content.ReadAsStringAsync().Result);

            using (new AssertionScope())
            {
                actualResponse.Id.Should().NotBe(null);
                actualResponse.Name.Should().Be(_user.Name);
            }
        }

        // update user

        [Given(@"I have following user")]
        public void GivenIHaveFollowingUser(Table table)
        {
            _goRestUser = table.CreateInstance<GoRestUser>();
        }

        [When(@"I update a request to the (.*) endpoint")]
        public void WhenIUpdateARequestToTheUsersEndpoint(string endpoint)
        {
            var request = JsonConvert.SerializeObject(_user);
            var requestBody = new StringContent(request, Encoding.UTF8, "application/json");

            var msgBody = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{_baseConfig.HttpClient.BaseUrl}{endpoint}/{_goRestUser.Id}"),
                Content = requestBody,
            };

            _response = _httpClient.SendAsync(msgBody).Result;
        }



    }
}
