using Magang_API.Base;
using Magang_API.Model;
using Magang_API.ViewModel;

namespace Magang_API.Repository.Contracts
{
    public interface IStatusRepository : IBaseRepository<Status, string>
    {
        Task<int> GetStatusTrueCountAsync();

        Task<int> GetStatusFalseCountAprovalAsync();
    }
}
