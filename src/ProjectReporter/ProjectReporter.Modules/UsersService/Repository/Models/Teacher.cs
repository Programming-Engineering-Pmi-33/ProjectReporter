namespace ProjectReporter.Modules.UsersService.Repository.Models
{
    public record Teacher : User
    {
        public int DepartmentId { get; }

        public Teacher(string email, string firstName, string lastName, int departmentId, string id = null) :
            base(email, firstName, lastName, id)
        {
            DepartmentId = departmentId;
        }
    }
}