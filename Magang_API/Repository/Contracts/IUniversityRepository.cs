using Magang_API.Base;
using Magang_API.Models;

namespace Magang_API.Repository.Contracts
{
    public interface IUniversityRepository : IBaseRepository<University, int>
    {
        Task<University?> GetByNameAsync(string name);
        Task<bool> IsNameExistAsync(string name);
        
    }
}
