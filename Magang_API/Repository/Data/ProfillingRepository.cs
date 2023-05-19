using Magang_API.Base;
using Magang_API.Context;

using Magang_API.Model;

using Magang_API.Repository.Contracts;

namespace Magang_API.Repository.Data
{
    public class ProfillingRepository : BaseRepository<Profiling, string, MyContexts>, IProfillingRepository
    {
        public ProfillingRepository(MyContexts context) : base(context)
        {
        }
    }
}
