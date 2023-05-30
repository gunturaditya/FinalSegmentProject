using Magang_API.Base;
using Magang_API.Contexts;

using Magang_API.Models;

using Magang_API.Repository.Contracts;

namespace Magang_API.Repository.Data
{
    public class ProfillingRepository : BaseRepository<Profiling, string, MyContext>, IProfillingRepository
    {
        public ProfillingRepository(MyContext context) : base(context)
        {
        }
    }
}
