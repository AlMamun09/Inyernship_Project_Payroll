using System.ComponentModel.DataAnnotations;

namespace PayrollProject.ViewModel.DesignationViewModels
{
    public class DesignationVM
    {
        public Guid DesignationId { get; set; }

        [Required(ErrorMessage = "Designation Name is required")]
        [Display(Name = "Designation Name")]
        public string? DesignationName { get; set; }

        public bool IsActive { get; set; } = true;
    }
}