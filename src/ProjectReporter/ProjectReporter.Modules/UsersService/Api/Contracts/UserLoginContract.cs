using System.ComponentModel.DataAnnotations;

namespace ProjectReporter.Modules.UsersService.Api.Contracts
{
    public class UserLoginContract
    {
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}