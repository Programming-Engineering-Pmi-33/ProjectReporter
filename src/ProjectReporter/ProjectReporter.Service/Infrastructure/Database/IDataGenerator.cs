namespace ProjectReporter.Service.Infrastructure
{
    public interface IDataGenerator
    {
        void AddUsers(int usersAmount, int teachersAmount, int adminsAmount);
        void AddGroups(int groupsAmount, int progectsAmount, int tasksAmount);

    }
}
