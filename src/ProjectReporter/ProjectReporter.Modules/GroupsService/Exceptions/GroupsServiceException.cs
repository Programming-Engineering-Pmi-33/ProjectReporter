using System;

namespace ProjectReporter.Modules.GroupsService.Exceptions
{
    public class GroupsServiceException : Exception
    {
        public GroupsServiceException()
        { }

        public GroupsServiceException(string message) : base(message)
        { }
    }
}