using Microsoft.AspNetCore.Mvc;

namespace randomAuthor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AditionalAuthorInfo : ControllerBase
    {
        private readonly static Dictionary<int, string> list = new Dictionary<int, string>
        {
            {1, "Author 1" },
            {2, "Author 2" },
            {3, "Author 3" },
            {4, "Author 4" },
            {5, "Author 5" },
 
        };

        [HttpGet(nameof(GetAll))]
        public Task<IActionResult> GetAll()
        {
            return Task.FromResult<IActionResult>(Ok(list));
        }

    }
}
