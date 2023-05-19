using Magang_API.Base;
using Magang_API.Model;


namespace Magang_API.Repository.Contracts
{
    public interface IDepartmentRepository : IBaseRepository<Department, int>
    {
        Task<Department?> GetByNameAsync(string name);
        Task<bool> IsNameExistAsync(string name);
    }
}
