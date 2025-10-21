using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PayrollProject.Models;

namespace PayrollProject.DataModels
{
    public class AllowanceDeduction
    {
        [Key]
        public Guid ADId { get; set; }
        public string? ADType { get; set; }
        public string ADName { get; set; } = string.Empty;
        public string? CalculationType { get; set; }
        public decimal? Percentage { get; set; }
        public decimal? FixedAmount { get; set; }
        public DateTime EffectiveFrom { get; set; }
    }
}