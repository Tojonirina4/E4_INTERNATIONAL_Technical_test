using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Test2StringCalculator.Models;
using Test2StringCalculator.Services;

namespace Test2StringCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringCalculatorService _service;

        public HomeController(ILogger<HomeController> logger, IStringCalculatorService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        //POST create action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(StringCalculator stringCalculator)
        {
            try
            {
                var result = _service.Add(stringCalculator.Numbers);
                TempData["SuccessMessage"] = $"The result is : {result}";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
