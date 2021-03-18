using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectReporter.Modules.TestData;
using ProjectReporter.Service.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectReporter.Service.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var Generator = new DataGenerator(
                new Modules.GroupsService.Storage.GroupsStorage(
                    new Microsoft.EntityFrameworkCore.DbContextOptions<Modules.GroupsService.Storage.GroupsStorage>()),
                new Modules.UsersService.Storage.UsersStorage(
                    new Microsoft.EntityFrameworkCore.DbContextOptions<Modules.UsersService.Storage.UsersStorage>())
                );
            Generator.AddUsers(10, 3, 1);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
