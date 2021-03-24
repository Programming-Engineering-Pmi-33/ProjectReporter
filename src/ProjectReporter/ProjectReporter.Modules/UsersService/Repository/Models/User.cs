namespace ProjectReporter.Modules.UsersService.Repository.Models
{
    public class User
    {
        public string Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }

        public User(string email, string firstName, string lastName, string id = null)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}