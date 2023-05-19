using Magang_API.Base;
using Magang_API.Model;

namespace Magang_API.Repository.Contracts
{
    public interface IAccountStudentRoleRepository : IBaseRepository<AccountStudentRole, string>
    {
        Task<IEnumerable<string>> GetRolesByNimAsync(string nim);
    }
}
