using System.ComponentModel.DataAnnotations;

namespace ProjectReporter.Modules.UsersService.Api.Contracts
{
    public class TeacherRegisterContract
    {
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string PasswordConfirm { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public int DepartmentId { get; set; }
    }
}