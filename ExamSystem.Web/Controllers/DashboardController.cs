using ExamSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.Web.Controllers
{

    [Route("[controller]")]
    public class DashboardController(IDashboardService _service) : BaseController
    {
        [HttpGet("")]
        public IActionResult Index() => View();

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats() => await ExecuteApiAsync(() => _service.GetStatisticsAsync());

    }
}
