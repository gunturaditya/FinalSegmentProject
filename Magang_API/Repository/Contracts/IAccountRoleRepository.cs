using Magang_API.Base;
using Magang_API.Models;


namespace Magang_API.Repository.Contracts
{
    public interface IAccountRoleRepository : IBaseRepository<AccountRole, string>
    {
        Task<IEnumerable<string>> GetRolesByNikAsync(string nik);
    }
}
