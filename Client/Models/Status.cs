namespace Client.Models
{
    public class Status
    {
        public string StudentId { get; set; } = null!;

        public int? DepartmentId { get; set; }

        public string? MentorId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? Status1 { get; set; } = null!;
    }
}
