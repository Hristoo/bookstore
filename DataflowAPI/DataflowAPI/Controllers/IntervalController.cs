using DataflowAPI.Kafka;
using DataflowAPI.Models;
using DataflowAPI.Models.Configuration;
using DataflowAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataflowAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class IntervalController : ControllerBase
    {

        [HttpPost(nameof(SetPurchaseInterval))]
        public async Task<ActionResult> SetPurchaseInterval(int interval)
        {
            IntervalSettings.PurchaseInterval = interval;
            return Ok();
        }
    }
}
