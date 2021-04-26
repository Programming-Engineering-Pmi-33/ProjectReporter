namespace ProjectReporter.Modules.UsersService.Repository.Models
{
    public class AcademicGroup
    {
        public int Id { get; }
        public string Name { get; }
        public int FacultyId { get; }

        public AcademicGroup(string name, int facultyId, int id = 0)
        {
            Id = id;
            Name = name;
            FacultyId = facultyId;
        }
    }
}