using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PayrollProject.Models;

namespace PayrollProject.DataModels
{
    public class Employee
    {
        [Key]
        public Guid EmployeeId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Gender { get; set; } = PayrollEnums.Gender.Male;
        public DateTime DateOfBirth { get; set; }
        public string? Designation { get; set; }
        public string? Department { get; set; }
        public DateTime JoiningDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BasicSalary { get; set; }

        [MaxLength(50)]
        public string EmploymentType { get; set; } = PayrollEnums.EmploymentType.Permanent;

        [MaxLength(50)]
        public string? BankAccountNumber { get; set; }

        public Guid? ShiftId { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = PayrollEnums.EmploymentStatus.Active;
    }
}