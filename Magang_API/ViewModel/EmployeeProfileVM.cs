using Magang_API.Model;

namespace Magang_API.ViewModel
{
    public class EmployeeProfileVM
    {
        public string Nik { get; set; }
        public string FullName { get; set; } 
        public Gender Gender { get; set; }
        public string Email { get; set; } 
        public string Major { get; set; } 
        public string Degree { get; set; }
        public decimal Gpa { get; set; }
        public string University { get; set; }
        public DateTime HiringDate { get; set; }
    }
}
