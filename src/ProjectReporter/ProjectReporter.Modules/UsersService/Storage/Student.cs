using System.ComponentModel.DataAnnotations;

namespace ProjectReporter.Modules.UsersService.Storage
{
    public class Student: User
    {
        [Required] public int GroupId { get; set; }
        public string GitLink { get; set; }
        
        public virtual AcademicGroup Group { get; set; }
    }
}