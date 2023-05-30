using Magang_API.Base;
using Magang_API.Contexts;
using Magang_API.Models;
using Magang_API.Repository.Contracts;
namespace Magang_API.Repository.Data
{
    public class RoleRepository : BaseRepository<Role, int, MyContext>, IRoleRepository
    {
        public RoleRepository(MyContext context) : base(context) { }
    }
}
