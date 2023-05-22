namespace Client.Models
{
    public class Employee
    {
        public string Nik { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string? LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime HiringDate { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public int? DepartmentId { get; set; }
    }

    public enum Gender
    {
        Male, Female
    }
}
