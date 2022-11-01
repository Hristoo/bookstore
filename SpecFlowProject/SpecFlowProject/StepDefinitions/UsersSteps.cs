using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using FluentAssertions.Execution;
using GoRestTests.Support;
using Newtonsoft.Json;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowProject.StepDefinitions
{
    [Binding]
    public sealed class UsersSteps
    {
        private HttpClient _httpClient = new HttpClient();
        private readonly BaseConfig _baseConfig;
        private HttpResponseMessage _response;
        private GoRestUserRequest _user;
        private GoRestUser _goRestUser;
        private TestContextContainer _context;

        public UsersSteps(BaseConfig baseConfig, TestContextContainer context)
        {
            _baseConfig = baseConfig;
            _context = context;
        }

        //// Get all users

        //[Given(@"I wat to prepare a request")]
        //public void GivenIWatToPrepareARequest()
        //{
        //    _httpClient.DefaultRequestHeaders.Add("Authorization", _baseConfig.HttpClientConfig.Token);
        //}

        //[When(@"I get all users from the (.*) endpoint")]
        //public void WhenIGetAllUsersFromTheUsersEndpoint(string endpoint)
        //{

        //    // _response = _httpClient.GetAsync($"{_baseConfig.HttpClientConfig.BaseUrl}{endpoint}").Result;
        //}

        [Then(@"The response staus code should be (.*)")]
        public void ThenTheResponseStausCodeShouldBeOK(string statusCode)
        {
            _response.StatusCode.ToString().Should().Be(statusCode);
        }

        //[Then(@"the response should contain a list of users")]
        //public void ThenTheResponseShouldContainAListOfUsers()
        //{
        //    var content = _response.Content.ReadAsStringAsync().Result;
        //    var expectedResponse = JsonConvert.DeserializeObject<List<GoRestUser>>(content);
        //    expectedResponse.Should().NotBeEmpty();
        //}

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
            // _httpClient.DefaultRequestHeaders.Add("Authorization", _baseConfig.HttpClientConfig.Token);

            var msgBody = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_baseConfig.HttpClientConfig.BaseUrl}{endpoint}"),
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

        //// Update user

        //[Given(@"I have following user")]
        //public void GivenIHaveFollowingUser(Table table)
        //{
        //    _goRestUser = table.CreateInstance<GoRestUser>();
        //}

        //[When(@"I update a request to the (.*) endpoint")]
        //public void WhenIUpdateARequestToTheUsersEndpoint(string endpoint)
        //{
        //    var request = JsonConvert.SerializeObject(_user);
        //    var requestBody = new StringContent(request, Encoding.UTF8, "application/json");
        //    _httpClient.DefaultRequestHeaders.Add("Authorization", _baseConfig.HttpClientConfig.Token);

        //    var msgBody = new HttpRequestMessage
        //    {
        //        Method = HttpMethod.Put,
        //        RequestUri = new Uri($"{_baseConfig.HttpClientConfig.BaseUrl}{endpoint}/{_goRestUser.Id}"),
        //        Content = requestBody,
        //    };

        //    _response = _httpClient.SendAsync(msgBody).Result;
        //}

        // hooks



    }
}