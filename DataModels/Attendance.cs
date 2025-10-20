using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PayrollProject.Models;

namespace PayrollProject.DataModels
{
    public class Attendance
    {
        [Key]
        public Guid AttendanceId { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }

        [DataType(DataType.Date)]
        public DateTime AttendanceDate { get; set; }

        public TimeSpan? InTime { get; set; }

        public TimeSpan? OutTime { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = PayrollEnums.AttendanceStatus.Present;

        [Column(TypeName = "decimal(5,2)")]
        public decimal WorkingHours { get; set; }
    }
}