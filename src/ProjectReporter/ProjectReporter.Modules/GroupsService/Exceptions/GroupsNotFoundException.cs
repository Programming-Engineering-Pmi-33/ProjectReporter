using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;

namespace ProjectReporter.Modules.GroupsService.Exceptions
{
    public class GroupsNotFoundException : GroupsServiceException
    {
        public int[] GroupIds { get; }

        public GroupsNotFoundException(params int[] ids) : base(
            $"Group{(ids.Length > 0 ? "s" : "")} with id{(ids.Length > 0 ? "s" : string.Empty)} {ids.Select(i => i.ToString()).Join()} {(ids.Length > 0 ? " are" : " is")} not found.")
        {
            GroupIds = ids;
        }

        public GroupsNotFoundException() { }
    }
}