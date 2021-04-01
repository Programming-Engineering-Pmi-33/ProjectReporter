using System.Collections.Generic;
using System.Linq;

namespace ProjectReporter.Modules.GroupsService.Exceptions
{
    public class UserAlreadyJoinedException : GroupsServiceException
    {
        public string[] UsersIds { get; }

        public UserAlreadyJoinedException(IEnumerable<string> usersIds)
        {
            UsersIds = usersIds.ToArray();
        }

        public UserAlreadyJoinedException()
        {
            UsersIds = new string[0];
        }
    }
}