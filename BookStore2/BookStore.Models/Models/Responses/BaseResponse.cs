using System.Net;

namespace BookStore.Models.Models.Responses
{
    public class BaseResponse
    {
        public HttpStatusCode HttpStatusCode { get; set;}

        public string Message { get; set; }
    }
}
