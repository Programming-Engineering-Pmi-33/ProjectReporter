using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;

namespace ProjectReporter.Modules.GroupsService.Exceptions
{
    public class TasksNotFoundException : GroupsServiceException
    {
        public int[] TaskIds { get; }

        public TasksNotFoundException(params int[] ids) : base(
            $"Task{(ids.Length > 0 ? "s" : "")} with id{(ids.Length > 0 ? "s" : string.Empty)} {ids.Select(i => i.ToString()).Join()} {(ids.Length > 0 ? " are" : " is")} not found.")
        {
            TaskIds = ids;
        }
    }
}