using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using ProjectReporter.Modules.UsersService.Api;
using ProjectReporter.Modules.UsersService.Api.Contracts;
using ProjectReporter.Modules.UsersService.Exceptions;
using ProjectReporter.Modules.UsersService.Repository.Models;

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
            var groups = await GetGroups(faculties[0].Id);
            ViewBag.Groups = new SelectList(groups, "Id", "Name");
            return View();
        }
        public async Task<IActionResult> RegisterTeacher()
        {
            var faculties = await _usersApi.GetFaculties();
            var list = new SelectList(faculties, "Id", "Name");
            ViewBag.Faculties = list;
            var departments = await GetDepartments(faculties[0].Id);
            ViewBag.Departments = new SelectList(departments, "Id", "Name");
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterStudent(StudentRegisterContract contract)
        {
            await _usersApi.Register(contract);
            return Redirect("/home");
        }

        public async Task<AcademicGroup[]> GetGroups(int facultyId)
        {
            return await _usersApi.GetAcademicGroups(facultyId);
        }

        public async Task<Department[]> GetDepartments(int facultyId)
        {
            return await _usersApi.GetDepartments(facultyId);
        }
    }
}
