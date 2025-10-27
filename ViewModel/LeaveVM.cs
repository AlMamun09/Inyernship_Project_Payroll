using System.ComponentModel.DataAnnotations;

namespace PayrollProject.ViewModel
{
    public class LeaveVM
    {
        public Guid LeaveId { get; set; }

        [Required(ErrorMessage = "Employee is required")]
        public Guid EmployeeId { get; set; }

        public string? EmployeeName { get; set; }

        [Required(ErrorMessage = "Leave Type is required")]
        public string? LeaveType { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "End Date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.Today;

        [Range(1, 365, ErrorMessage = "Total days must be between 1 and 365")]
        public int TotalDays { get; set; }

        [Required(ErrorMessage = "Leave Status is required")]
        public string? LeaveStatus { get; set; }

        public string? Remarks { get; set; }
    }
}