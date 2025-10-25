namespace PayrollProject.Models
{
    public static class PayrollEnums
    {
        public static class Gender
        {
            public const string Male = "Male";
            public const string Female = "Female";
        }

        public static class EmploymentStatus
        {
            public const string Active = "Active";
            public const string Inactive = "Inactive";
            public const string Resigned = "Resigned";
        }

        public static class EmploymentType
        {
            public const string Permanent = "Permanent";
            public const string Contract = "Contract";
            public const string Intern = "Intern";
        }

        public static class AttendanceStatus
        {
            public const string Present = "Present";
            public const string Absent = "Absent";
            public const string Leave = "Leave";
            public const string Holiday = "Holiday";
        }

        public static class LeaveStatus
        {
            public const string Pending = "Pending";
            public const string Approved = "Approved";
            public const string Rejected = "Rejected";
        }

        public static class LeaveType
        {
            public const string Casual = "Casual";
            public const string Sick = "Sick";
            public const string Earned = "Earned";
            public const string Unpaid = "Unpaid";
        }

        public static class AllowanceDeductionType
        {
            public const string Allowance = "Allowance";
            public const string Deduction = "Deduction";
        }

        public static class CalculationType
        {
            public const string FixedAmount = "Fixed Amount";
            public const string Percentage = "Percentage";
        }

        public static class PaymentStatus
        {
            public const string Pending = "Pending";
            public const string Paid = "Paid";
        }
    }
}
