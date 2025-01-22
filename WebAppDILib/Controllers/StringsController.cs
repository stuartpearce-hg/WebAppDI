using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using OpenTelemetry;
using OpenTelemetry.Trace;
namespace CasCap.Controllers
{
    [Route("api/[controller]")]
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
            using var activity = WebAppDI.Telemetry.StartActivity("StringsController.TestDI");
            _logger.LogTrace("TestDI REST endpoint fired...");
            
            activity?.SetTag("controller", "Strings");
            activity?.SetTag("action", "TestDI");
            
            var strings = _diTestSvc.GetStringValues();
            activity?.SetTag("values_count", strings.Count);
            
            return Ok(strings);
        }
    }
}
