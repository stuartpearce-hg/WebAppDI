using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
namespace CasCap.Controllers
{
    [Route("api")]
    [ApiController]
    public class StringsController : ControllerBase
    {
        readonly ILogger<StringsController> _logger;
        readonly IDITestService _diTestSvc;

        public StringsController(ILogger<StringsController> logger, IDITestService diTestSvc)
        {
            _logger = logger;
            _diTestSvc = diTestSvc;
        }

        [HttpGet("TestDI")]
        public IActionResult TestDI()
        {
            _logger.LogTrace("TestDI REST endpoint fired...");
            var strings = _diTestSvc.GetStringValues();
            return Ok(strings);
        }
    }
}
