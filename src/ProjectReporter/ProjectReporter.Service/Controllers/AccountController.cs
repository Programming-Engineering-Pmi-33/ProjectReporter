using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using ProjectReporter.Modules.UsersService.Api;
using ProjectReporter.Modules.UsersService.Api.Contracts;

namespace ProjectReporter.Service.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUsersApi _usersApi;
        public AccountController(ILogger<AccountController> logger, IUsersApi usersApi)
        {
            _logger = logger;
            _usersApi = usersApi;
        }

        public async Task<IActionResult> RegisterStudent()
        {
            var faculties = await _usersApi.GetFaculties();
            var list = new SelectList(faculties, "Id", "Name");
            ViewBag.Faculties = list;
            return View();
        }
        public IActionResult RegisterTeacher()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterStudent(StudentRegisterContract contract)
        {
            return Redirect("/home");
        }

        public async Task<dynamic> LoadGroups(int facultyId)
        {
            return await _usersApi.GetAcademicGroups(facultyId);
        }
    }
}
