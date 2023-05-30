using Magang_API.Base;
using Magang_API.Contexts;

using Magang_API.Models;

using Magang_API.Repository.Contracts;

namespace Magang_API.Repository.Data
{
    public class StatusRepository : BaseRepository<Status, string, MyContext>, IStatusRepository
    {
        public StatusRepository(MyContext context) : base(context)
        {
        }

    }
}
