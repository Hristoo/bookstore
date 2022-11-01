using NUnit.Framework;
using TechTalk.SpecFlow.Assist;

namespace AcademySpecflow.StepDefinitions
{
    [Binding]
    public class WalletSteps
    {
        [Given(@": I have the following cards")]
        public void GivenIHaveTheFollowingCards(Table table)
        {
            var card = table.CreateSet<CardData>();
        }
    }

    public class CardData
    {
        public int Number { get; set; }
        public string ExpirationDate { get; set; }
        public int CVV { get; set; }
        public string Type { get; set; }
    }
}
