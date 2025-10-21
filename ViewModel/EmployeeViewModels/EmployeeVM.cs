namespace PayrollProject.ViewModel.EmployeeViewModels
{
    public class EmployeeVM
    {
        public Guid EmployeeId { get; set; }
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Designation { get; set; }
        public string? Department { get; set; }
        public DateTime JoiningDate { get; set; }
        public decimal BasicSalary { get; set; }
        public string? EmploymentType { get; set; }
        public string? BankAccountNumber { get; set; }
        public Guid? ShiftId { get; set; }
        public string? Status { get; set; }
    }
}
