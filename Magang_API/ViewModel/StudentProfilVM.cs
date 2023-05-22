using Magang_API.Model;

namespace Magang_API.ViewModel
{
    public class StudentProfilVM
    {
        public string Nim { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public decimal Gpa { get; set; }
        public string University { get; set; }
        public string Mentor { get; set;}
        public string Department { get; set; }
        public bool? Status { get; set; } = null;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        
    }
}
