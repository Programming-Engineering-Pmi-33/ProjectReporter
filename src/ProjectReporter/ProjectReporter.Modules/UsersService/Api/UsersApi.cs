using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProjectReporter.Modules.UsersService.Api.Contracts;
using ProjectReporter.Modules.UsersService.Api.Factories;
using ProjectReporter.Modules.UsersService.Repository;
using ProjectReporter.Modules.UsersService.Repository.Models;

namespace ProjectReporter.Modules.UsersService.Api
{
    public class UsersApi : IUsersApi
    {
        private readonly UserManager<Storage.User> _userManager;
        private readonly SignInManager<Storage.User> _signInManager;
        private readonly IStorageUserModelMapper _mapper;
        private readonly IUsersRepository _repository;

        public UsersApi(UserManager<Storage.User> userManager,
            SignInManager<Storage.User> signInManager,
            IStorageUserModelMapper mapper,
            IUsersRepository repository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Register(StudentRegisterContract contract)
        {
            var user = _mapper.Map(contract);
            var result = await _userManager.CreateAsync(user, contract.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
            }
        }

        public async Task Register(TeacherRegisterContract contract)
        {
            var user = _mapper.Map(contract);
            var result = await _userManager.CreateAsync(user, contract.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
            }
        }

        public async Task Login(UserLoginContract contract)
        {
            await _signInManager.PasswordSignInAsync(contract.Email, contract.Password, contract.RememberMe, false);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<User[]> GetUsers(params string[] ids)
        {
            return await _repository.GetUsers(ids);
        }

        public async Task<Student[]> GetStudents(int academicGroupId)
        {
            return await _repository.GetStudents(academicGroupId);
        }

        public async Task<Teacher[]> GetTeachers(int facultyId, string[] ids = null)
        {
            return await _repository.GetTeachers(facultyId, ids);
        }
    }
}