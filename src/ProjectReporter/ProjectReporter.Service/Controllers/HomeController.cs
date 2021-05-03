using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectReporter.Service.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProjectReporter.Modules.GroupsService.Api;
using ProjectReporter.Modules.UsersService.Storage;

namespace ProjectReporter.Service.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IGroupsApi _groupsApi;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, SignInManager<User> signInManager, IGroupsApi groupsApi)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _groupsApi = groupsApi;
        }

        public async Task<IActionResult> Index()
        {
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
