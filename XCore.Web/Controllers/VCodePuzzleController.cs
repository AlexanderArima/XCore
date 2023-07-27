using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCore.Web.Controllers
{
    public class VCodePuzzleController : Controller
    {
        private readonly ILogger<VCodePuzzleController> _logger;

        public VCodePuzzleController(ILogger<VCodePuzzleController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string Create()
        {
            return "Create";
        }

        public string Check()
        {
            return "Check";
        }
    }
}
