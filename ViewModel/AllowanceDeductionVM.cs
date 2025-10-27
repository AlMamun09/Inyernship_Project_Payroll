namespace PayrollProject.ViewModel
{
    public class AllowanceDeductionVM
    {
        public Guid AllowanceDeductionId { get; set; }
        public string? AllowanceDeductionType { get; set; }
        public string? AllowanceDeductionName { get; set; }
        public string? CalculationType { get; set; }
        public decimal? Percentage { get; set; }
        public decimal? FixedAmount { get; set; }
        public DateTime EffectiveFrom { get; set; }
    }
}
