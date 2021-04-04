namespace ProjectReporter.Modules.GroupsService.Exceptions
{
    public class NameAlreadyUsedException : GroupsServiceException
    {
        public NameAlreadyUsedException(string name) : base($"Name {name} is already used.") { }
    }
}