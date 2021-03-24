namespace ProjectReporter.Modules.UsersService.Repository.Models
{
    public class Student : User
    {
        public int GroupId { get; }
        public string GitLink { get; }

        public Student(string email, string firstName, string lastName, int groupId, string gitLink, string id = null) :
            base(email, firstName, lastName, id)
        {
            GroupId = groupId; //?
            GitLink = gitLink;
        }
    }
}