namespace PayrollProject.ViewModel.ShiftViewModels
{
    public class ShiftVM
    {
        public Guid ShiftId { get; set; }
        public string? ShiftName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
