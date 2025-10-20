using System.ComponentModel.DataAnnotations;
using PayrollProject.Models;

namespace PayrollProject.DataModels
{
    public class Leave
    {
        [Key]
        public Guid LeaveId { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }

        [MaxLength(50)]
        public string LeaveType { get; set; } = PayrollEnums.LeaveType.Casual;

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public int TotalDays { get; set; }

        [MaxLength(20)]
        public string LeaveStatus { get; set; } = PayrollEnums.LeaveStatus.Pending;

        [MaxLength(500)]
        public string? Remarks { get; set; }
    }
}