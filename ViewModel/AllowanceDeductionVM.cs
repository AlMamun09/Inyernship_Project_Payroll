namespace PayrollProject.ViewModel
{
    public class AllowanceDeductionVM
    {
        public Guid ADId { get; set; }
        public string? ADType { get; set; }
        public string ADName { get; set; } = string.Empty;
        public string? CalculationType { get; set; }
        public decimal? Percentage { get; set; }
        public decimal? FixedAmount { get; set; }
        public DateTime EffectiveFrom { get; set; }
    }
}
