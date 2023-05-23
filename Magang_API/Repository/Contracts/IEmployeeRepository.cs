using Magang_API.Base;
using Magang_API.Model;

using Magang_API.ViewModel;

namespace Magang_API.Repository.Contracts
{
    public interface IEmployeeRepository : IBaseRepository<Employee, string>
    {
        Task<UserDataVM> GetUserDataByEmailAsync(string email);
        Task<IEnumerable<EmployeeProfileVM>> GetDataProfile();
        Task<IEnumerable<dynamic>> GetDataEmployePembina();
        Task<IEnumerable<dynamic>> GetDataProfileBynik(string nik);

        Task<IEnumerable<dynamic>> GetEmployeeByIdDepartment(int id);
    }
}
