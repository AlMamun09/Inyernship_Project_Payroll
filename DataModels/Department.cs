using System.ComponentModel.DataAnnotations;

namespace PayrollProject.DataModels
{
    public class Department
    {
        [Key]
        public Guid DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public bool IsActive { get; set; }

    }
}
