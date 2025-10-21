namespace PayrollProject.ViewModel.SalarySlipViewModels
{
    public class SalarySlipVM
    {
        public Guid SlipId { get; set; }
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
