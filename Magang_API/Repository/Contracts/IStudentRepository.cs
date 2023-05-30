using Magang_API.Base;
using Magang_API.Models;
using Magang_API.ViewModel;

namespace Magang_API.Repository.Contracts
{
    public interface IStudentRepository : IBaseRepository<Student, string>
    {
        Task<UserDataVM> GetUserDataByEmailAsync(string email);
        Task<int> GetStudentCountAsync();

        Task<int> GetStudentCountAprovalAsync();

        Task<IEnumerable<dynamic>> GetAllStudentsNoAproval();
        Task<IEnumerable<dynamic>> GetAllStudentsTrueAproval();
        Task<IEnumerable<dynamic>> GetAllStudentsFalseAproval();
        Task<IEnumerable<string>> GetUniversitasAsyncbyid(int id);
        Task<int>AprovalTrue(StudentAproval student);
        Task<int> AprovalFalse(StudentAproval student);

        Task<IEnumerable<StudentProfilVM>> GetAllStudentProfil();

        Task<IEnumerable<dynamic>> GetStudentByNik(string nik);
        Task<IEnumerable<dynamic>> GetStudentPenilaian(string nik);
        Task<IEnumerable<dynamic>> GetStudentByNim(string nim);

        Task<IEnumerable<StudentChart>> GetStudentCharts();
    }
}
