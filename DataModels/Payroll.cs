using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PayrollProject.Models;

namespace PayrollProject.DataModels
{
    public class Payroll
    {
        [Key]
        public Guid PayrollId { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }

        [DataType(DataType.Date)]
        public DateTime PayPeriodStart { get; set; }

        [DataType(DataType.Date)]
        public DateTime PayPeriodEnd { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BasicSalary { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAllowances { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalDeductions { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal NetSalary { get; set; }

        [MaxLength(20)]
        public string PaymentStatus { get; set; } = PayrollEnums.PaymentStatus.Pending;

        [DataType(DataType.Date)]
        public DateTime? PaymentDate { get; set; }
    }
}