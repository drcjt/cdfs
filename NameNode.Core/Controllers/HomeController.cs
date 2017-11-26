using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NameNode.Core.Models;
using System;

namespace NameNode.Core.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            //ViewData["Message"] = _messagingService.GetMessage();
            return View();
        }

        [HttpPost]
        public IActionResult Register()
        {
            return Json(Guid.NewGuid());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
