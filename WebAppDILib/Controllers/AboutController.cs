using CasCap.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using OpenTelemetry;
using OpenTelemetry.Trace;
namespace CasCap.Controllers
{
    public class AboutController : Controller
    {
        readonly ILogger<AboutController> _logger;
        readonly IDITestService _diTestSvc;

        public AboutController(ILogger<AboutController> logger, IDITestService diTestSvc)
        {
            _logger = logger;
            _diTestSvc = diTestSvc;
        }

        public IActionResult Index()
        {
            using var activity = WebAppDI.Telemetry.StartActivity("AboutController.Index");
            _logger.LogInformation("About page requested");
            
            activity?.SetTag("controller", "About");
            activity?.SetTag("action", "Index");
            
            var vm = new IndexViewModel
            {
                SomeIntValues = _diTestSvc.GetIntValues(),
                SomeStringValues = _diTestSvc.GetStringValues()
            };
            
            activity?.SetTag("values_count", vm.SomeIntValues.Count);
            return View(vm);
        }
    }
}
