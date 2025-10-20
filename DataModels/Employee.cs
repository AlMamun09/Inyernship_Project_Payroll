using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PayrollProject.Models;

namespace PayrollProject.DataModels
{
    public class Employee
    {
        [Key]
        public Guid EmployeeId { get; set; }

        [Required, MaxLength(200)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Gender { get; set; } = PayrollEnums.Gender.Male;

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [MaxLength(100)]
        public string? Designation { get; set; }

        [MaxLength(100)]
        public string? Department { get; set; }

        [DataType(DataType.Date)]
        public DateTime JoiningDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BasicSalary { get; set; }

        [MaxLength(50)]
        public string EmploymentType { get; set; } = PayrollEnums.EmploymentType.Permanent;

        [MaxLength(50)]
        public string? BankAccountNumber { get; set; }

        public Guid ShiftId { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = PayrollEnums.EmploymentStatus.Active;
    }
}