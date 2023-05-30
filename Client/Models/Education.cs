namespace Client.Models
{
    public class Education
    {
        public int Id { get; set; }

        public string Major { get; set; } = null!;

        public string Degree { get; set; } = null!;

        public decimal? Gpa { get; set; }

        public int? UniversityId { get; set; }
    }
}
