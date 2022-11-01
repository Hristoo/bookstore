using Bogus;
using static Bogus.DataSets.Name;

namespace GoRestTests.Support
{
    [Binding]
    public class BogusSteps
    {
        [Given(@"I want to create new user request body")]
        public void GivenIWantToCreateNewUserRequestBody()
        {
            var fakerUser = new Faker<GoRestUserRequest>()
                    .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())
                    .RuleFor(u => u.Name, (f, u) => f.Name.FirstName(u.Gender))
                    .RuleFor(u => u.Name, (f, u) => f.Name.LastName(u.Gender))
                    .RuleFor(u => u.Email, (f, u) => f.Internet.Email())
                    .RuleFor(u => u.Status, (f, u) => f.PickRandom<Status>().ToString());

        }
    }
}
