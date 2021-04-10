using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;

namespace ProjectReporter.Modules.UsersService.Exceptions
{
    public class UsersNotFoundException : UsersServiceException
    {
        public string[] UsersIds { get; }

        public UsersNotFoundException(params string[] ids) : base(
            $"User{(ids.Length > 0 ? "s" : "")} with id{(ids.Length > 0 ? "s" : string.Empty)} {ids.Select(i => i.ToString()).Join()} {(ids.Length > 0 ? " are" : " is")} not found.")
        {
            UsersIds = ids;
        }

        public UsersNotFoundException() { }
    }
}