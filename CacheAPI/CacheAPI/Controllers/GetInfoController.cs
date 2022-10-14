using CacheAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CacheAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class GetInfoController : ControllerBase
    {
        private readonly HostedService _dbInfoService;

        public GetInfoController(HostedService dbInfoService)
        {
            _dbInfoService = dbInfoService;
        }

        //[HttpGet(nameof(GetBDInfo))]
        //public async Task<IActionResult> GetBDInfo()
        //{
        //    return Ok(_dbInfoService.GetAllBooks());
        //}

    }
}
