using CasCap.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using OpenTelemetry;
using OpenTelemetry.Trace;
namespace CasCap.Controllers
{
    public class HomeController : Controller
    {
        readonly ILogger<HomeController> _logger;
        readonly IDITestService _diTestSvc;

        public HomeController(ILogger<HomeController> logger, IDITestService diTestSvc)
        {
            _logger = logger;
            _diTestSvc = diTestSvc;
        }

        public IActionResult Index()
        {
            using var activity = WebAppDI.Telemetry.StartActivity("HomeController.Index");
            _logger.LogInformation("Home page requested");
            
            activity?.SetTag("controller", "Home");
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
