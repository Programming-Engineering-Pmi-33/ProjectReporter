using System.ComponentModel.DataAnnotations;

namespace ProjectReporter.Modules.UsersService.Api.Contracts
{
    public class UserLoginContract
    {
        [Required(ErrorMessage = "Email is required")]
        [StringLength(50)]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Email format is incorrect")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required"), StringLength(256),
         RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",
             ErrorMessage =
                 "Password should contain minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}