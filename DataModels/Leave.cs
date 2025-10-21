using System.ComponentModel.DataAnnotations;
using PayrollProject.Models;

namespace PayrollProject.DataModels
{
    public class Leave
    {
        [Key]
        public Guid LeaveId { get; set; }
        public Guid EmployeeId { get; set; }
        public string LeaveType { get; set; } = PayrollEnums.LeaveType.Casual;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalDays { get; set; }
        public string LeaveStatus { get; set; } = PayrollEnums.LeaveStatus.Pending;
        public string? Remarks { get; set; }
    }
}