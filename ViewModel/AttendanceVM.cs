using System.ComponentModel.DataAnnotations;

namespace PayrollProject.ViewModel
{
    public class AttendanceVM
    {
        public Guid AttendanceId { get; set; }

        [Required(ErrorMessage = "Employee is required")]
        public Guid EmployeeId { get; set; }

        public string? EmployeeName { get; set; }

        [Required(ErrorMessage = "Attendance Date is required")]
        [DataType(DataType.Date)]
        public DateTime AttendanceDate { get; set; } = DateTime.Today;

        [DataType(DataType.Time)]
        public TimeSpan? InTime { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan? OutTime { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string? Status { get; set; }

        [Range(0, 24, ErrorMessage = "Working hours must be between 0 and 24")]
        public decimal WorkingHours { get; set; }
    }
}