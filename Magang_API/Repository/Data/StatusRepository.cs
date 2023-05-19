using Magang_API.Base;
using Magang_API.Context;

using Magang_API.Model;

using Magang_API.Repository.Contracts;

namespace Magang_API.Repository.Data
{
    public class StatusRepository : BaseRepository<Status, string, MyContexts>, IStatusRepository
    {
        public StatusRepository(MyContexts context) : base(context)
        {
        }
    }
}
