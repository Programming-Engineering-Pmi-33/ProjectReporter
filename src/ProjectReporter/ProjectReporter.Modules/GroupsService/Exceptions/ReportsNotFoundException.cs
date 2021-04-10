using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;

namespace ProjectReporter.Modules.GroupsService.Exceptions
{
    public class ReportsNotFoundException : GroupsServiceException
    {
        public int[] ReportsIds { get; }

        public ReportsNotFoundException(params int[] ids) : base(
            $"Report{(ids.Length > 0 ? "s" : "")} with id{(ids.Length > 0 ? "s" : string.Empty)} {ids.Select(i => i.ToString()).Join()} {(ids.Length > 0 ? " are" : " is")} not found.")
        {
            ReportsIds = ids;
        }

        public ReportsNotFoundException() { }
    }
}