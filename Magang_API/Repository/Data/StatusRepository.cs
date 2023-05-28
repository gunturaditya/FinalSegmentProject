using Magang_API.Base;
using Magang_API.Context;

using Magang_API.Model;

using Magang_API.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Magang_API.Repository.Data
{
    public class StatusRepository : BaseRepository<Status, string, MyContexts>, IStatusRepository
    {
        public StatusRepository(MyContexts context) : base(context)
        {
        }

        public async Task<int> GetStatusFalseCountAprovalAsync()
        {
            var status = await _context.Statuses.Where(x => x.Status1 == false).CountAsync();

            return status;
        }

        public async Task<int> GetStatusTrueCountAsync()
        {
            var status = await _context.Statuses.Where(x => x.Status1 == true).CountAsync();
            return status;
        }
    }
}
