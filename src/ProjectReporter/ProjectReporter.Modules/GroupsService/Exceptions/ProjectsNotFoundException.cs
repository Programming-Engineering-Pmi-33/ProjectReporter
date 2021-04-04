using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;

namespace ProjectReporter.Modules.GroupsService.Exceptions
{
    public class ProjectsNotFoundException : GroupsServiceException
    {
        public int[] ProjectsIds { get; }

        public ProjectsNotFoundException(params int[] ids) : base(
            $"Project{(ids.Length > 0 ? "s" : "")} with id{(ids.Length > 0 ? "s" : string.Empty)} {ids.Select(i => i.ToString()).Join()} {(ids.Length > 0 ? " are" : " is")} not found.")
        {
            ProjectsIds = ids;
        }
    }
}