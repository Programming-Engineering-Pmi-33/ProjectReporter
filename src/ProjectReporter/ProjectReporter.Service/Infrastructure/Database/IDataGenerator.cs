namespace ProjectReporter.Service.Infrastructure.Database
{
    public interface IDataGenerator
    {
        void AddUsers(int usersAmount, int teachersAmount, int adminsAmount);
        void AddGroups(int groupsAmount, int projects, int tasksAmount);

    }
}
