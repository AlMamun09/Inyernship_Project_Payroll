using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollProject.DataModels
{
    public class SalarySlip
    {
        [Key]
        public Guid SalarySlipId { get; set; }
        public Guid PayrollId { get; set; }
        public Guid EmployeeId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal GrossEarnings { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal NetPay { get; set; }
        public DateTime GeneratedDate { get; set; }
    }
}