using Magang_API.Base;
using Magang_API.Model;
using Magang_API.ViewModel;

namespace Magang_API.Repository.Contracts
{
    public interface IStudentRepository : IBaseRepository<Student, string>
    {
        Task<UserDataVM> GetUserDataByEmailAsync(string email);
        Task<int> GetStudentCountAsync();

        Task<IEnumerable<dynamic>> GetAllStudentsNoAproval();
        Task<IEnumerable<string>> GetUniversitasAsyncbyid(int id);
        Task<int>AprovalTrue(StudentAproval student);
        Task<int> AprovalFalse(StudentAproval student);
    }
}
