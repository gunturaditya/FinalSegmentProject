using Magang_API.Base;
using Magang_API.Context;
using Magang_API.Model;
using Magang_API.Repository.Contracts;
namespace Magang_API.Repository.Data
{
    public class RoleRepository : BaseRepository<Role, int, MyContexts>, IRoleRepository
    {
        public RoleRepository(MyContexts context) : base(context) { }
    }
}
