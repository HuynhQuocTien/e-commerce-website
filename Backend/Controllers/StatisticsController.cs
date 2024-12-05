using e_commerce_website.Helper.Statistics;
using e_commerce_website.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;
        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }
        [HttpPost]
        public IActionResult RevenueStatistics(RevenueStatisticsRequest request)
        {
            var data = _statisticsService.RevenueStatistics(request);
            return Ok(data);
        }
        [HttpPost("ProductStatistics")]
        public IActionResult ProductStatistics(ProductStatisticsRequest request)
        {
            var data = _statisticsService.ProductStatistics(request);
            return Ok(data);
        }
        [HttpGet("StatusOrderStatistics")]
        public IActionResult StatusOrderStatistics()
        {
            return Ok(_statisticsService.StatusOrderStatistics());

        }
    }
}
