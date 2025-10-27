using System.ComponentModel.DataAnnotations;

namespace PayrollProject.ViewModel
{
    public class DepartmentVM
    {
        public Guid DepartmentId { get; set; }

        [Required(ErrorMessage = "Department Name is Required")]
        [Display(Name = "Department Name")]
        public string? DepartmentName { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
