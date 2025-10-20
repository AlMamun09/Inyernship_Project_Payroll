using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollProject.DataModels
{
    public class SalarySlip
    {
        [Key]
        public Guid SlipId { get; set; }

        [Required]
        public Guid PayrollId { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal GrossEarnings { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalDeductions { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal NetPay { get; set; }

        public DateTime GeneratedDate { get; set; }
    }
}