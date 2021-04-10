namespace ProjectReporter.Modules.GroupsService.Exceptions
{
    public class GroupsModelException : GroupsServiceException
    {
        public GroupsModelException(string property) : base($"Property {property} is incorrect.") { }
    }
}