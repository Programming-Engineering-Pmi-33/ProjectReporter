using System.ComponentModel.DataAnnotations;

namespace ProjectReporter.Modules.UsersService.Storage
{
    public class Teacher: User
    {
        [Required] public int DepartmentId { get; set; }
        
        public virtual Department Department { get; set; }
    }
}