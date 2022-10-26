using System.Net.Http;
using System.Net.Http.Headers;

namespace Http
{
    internal class Program
    {
        static async Task Main(string[] args)
        {


  //          var authors = new Dictionary<int, string>
  //{
  //    { 1, "hello" },
  //    { 2, "world" }
  //};

  //          var content = new FormUrlEncodedContent(authors);

  //          var response = await client.PostAsync("https://localhost:7057/AditionalAuthorInfo/GetAll", content);

  //          var responseString = await response.Content.ReadAsStringAsync();



            HttpClient client = new HttpClient();

            var stringTask = await client.GetStringAsync("http://localhost:5057/AditionalAuthorInfo/GetAll");

            Console.WriteLine(stringTask);
        }
    }
}