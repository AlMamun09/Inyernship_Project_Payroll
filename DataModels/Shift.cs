using System.ComponentModel.DataAnnotations;

namespace PayrollProject.DataModels
{
    public class Shift
    {
        [Key]
        public Guid ShiftId { get; set; }

        [Required]
        public string ShiftName { get; set; } = string.Empty;

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public bool IsActive { get; set; } = true;
    }
}