namespace PayrollProject.ViewModel.AttendanceViewModels
{
    public class AttendanceVM
    {
        public Guid AttendanceId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public TimeSpan? InTime { get; set; }
        public TimeSpan? OutTime { get; set; }
        public string? Status { get; set; }
        public decimal WorkingHours { get; set; }
    }
}
