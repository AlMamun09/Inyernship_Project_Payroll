using System;
using System.ComponentModel.DataAnnotations;

namespace PayrollProject.ViewModel.EmployeeViewModels
{
    public class EmployeeVM
    {
        public Guid EmployeeId { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 50 characters")]
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Designation is required")]
        public string? Designation { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public string? Department { get; set; }

        [Required(ErrorMessage = "Joining Date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Joining Date")]
        public DateTime JoiningDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Basic Salary is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Basic Salary cannot be negative")]
        [Display(Name = "Basic Salary")]
        public decimal BasicSalary { get; set; }

        [Required(ErrorMessage = "Employment Type is required")]
        [Display(Name = "Employment Type")]
        public string? EmploymentType { get; set; }

        [StringLength(50, ErrorMessage = "Bank Account Number cannot exceed 50 characters")]
        [Display(Name = "Bank Account Number")]
        public string? BankAccountNumber { get; set; }

        [Display(Name = "Shift")]
        public Guid? ShiftId { get; set; }

        [Display(Name = "Shift")]
        public string? ShiftName { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string? Status { get; set; } = PayrollProject.Models.PayrollEnums.EmploymentStatus.Active;
    }

}
