using Magang_API.Base;
using Magang_API.Models;
using Magang_API.ViewModel;

namespace Magang_API.Repository.Contracts
{
    public interface IAccountStudentRepository : IBaseRepository<AccountStudent, string>
    {
        Task RegisterStudentAsync(RegisterStudentVM registerStudentVM);
        Task<bool> LoginAsync(LoginVM loginVM);

        Task<int> DeleteAccount(string id);
    }
}
