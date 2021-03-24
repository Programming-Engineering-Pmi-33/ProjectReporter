using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ProjectReporter.Modules.UsersService.Storage
{
    public class User: IdentityUser
    {
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }

        [Required] public DateTime DateTimeCreated { get; set; }
        [Required] public DateTime DateTimeModified { get; set; }
    }
}