using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollProject.Models
{
    public enum EmployeeGender
    {
        Male,
        Female,
        Other,
    }
    public enum EmployeeDesignation
    {
        Manager,
        TeamLead,
        Developer,
        Intern,
    }
    public enum EmployeeDepartment
    {
        HR,
        IT,
        Finance,
        Marketing,
    }
    public enum EmploymentType
    {
        FullTime,
        PartTime,
        Contract,
        Temporary,
    }
    public enum EmployeeStatus
    {
        Active,
        Inactive,
        OnLeave,
        Resigned,
    }
    public class Employee
    {
        [Key]
        public Guid EmployeeId { get; set; }

        [Required, MaxLength(200)]
        public string FullName { get; set; } = string.Empty; 

        [MaxLength(20)]
        public string Gender { get; set; } = "Male";

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } = default;

        [MaxLength(100)]
        public string? Designation { get; set; }

        [MaxLength(100)]
        public string? Department { get; set; }

        [DataType(DataType.Date)]
        public DateTime JoiningDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BasicSalary { get; set; }

        [MaxLength(50)]
        public string? EmploymentType { get; set; }

        [MaxLength(50)]
        public string? BankAccountNumber { get; set; }

        // Foreign key to Shift table (optional)
        public int ShiftId { get; set; }

        [MaxLength(20)]
        public string Status { get; set; }
    }
}
