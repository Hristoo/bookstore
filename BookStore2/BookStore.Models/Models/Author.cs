namespace BookStore.Models.Models
{
    public class Author : Person
    {
        public int Age { get; set; }

        public string Nickname { get; set; }

        public DateTime DateOoBirth { get; set; }
    }
}
