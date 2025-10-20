// In DataModels/AllowanceDeduction.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PayrollProject.Models;

namespace PayrollProject.DataModels
{
    public class AllowanceDeduction
    {
        [Key]
        public Guid ADId { get; set; }

        [Required, MaxLength(20)]
        public string ADType { get; set; } = PayrollEnums.ADType.Allowance;

        [Required, MaxLength(100)]
        public string ADName { get; set; } = string.Empty;

        [Required]
        public string CalculationType { get; set; } = PayrollEnums.CalculationType.FixedAmount;

        [Column(TypeName = "decimal(5,2)")]
        public decimal Percentage { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FixedAmount { get; set; }

        [DataType(DataType.Date)]
        public DateTime EffectiveFrom { get; set; }
    }
}