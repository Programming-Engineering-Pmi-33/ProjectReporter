using System.ComponentModel.DataAnnotations;

namespace ProjectReporter.Modules.UsersService.Storage
{
    public class Admin: User
    {
        [Required] public string SecretKey { get; set; }
    }
}