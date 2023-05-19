using Magang_API.Base;
using Magang_API.Model;

using Magang_API.ViewModel;

namespace Magang_API.Repository.Contracts
{
    public interface IAccountRepository : IBaseRepository<Account, string>
    {
        Task RegisterAsync(RegisterVM registerVM);
        Task<bool> LoginAsync(LoginVM loginVM);
    }
}
