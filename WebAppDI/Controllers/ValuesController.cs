using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using OpenTelemetry;
using OpenTelemetry.Trace;
namespace CasCap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly ILogger<ValuesController> _logger;
        readonly IDITestService _diTestSvc;

        public ValuesController(ILogger<ValuesController> logger, IDITestService diTestSvc)
        {
            _logger = logger;
            _diTestSvc = diTestSvc;
        }

        [HttpGet("TestDI")]
        public IActionResult TestDI()
        {
            using var activity = WebAppDI.Telemetry.StartActivity("ValuesController.TestDI");
            _logger.LogTrace("TestDI REST endpoint fired...");
            
            activity?.SetTag("controller", "Values");
            activity?.SetTag("action", "TestDI");
            
            var ints = _diTestSvc.GetIntValues();
            activity?.SetTag("values_count", ints.Count);
            
            return Ok(ints);
        }
    }
}
