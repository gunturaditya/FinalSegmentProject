using Magang_API.Base;
using Magang_API.Contexts;
using Magang_API.Models;
using Magang_API.Repository.Contracts;

namespace Magang_API.Repository.Data
{
    public class AccountStudentRoleRepository : BaseRepository<AccountStudentRole, string, MyContext>, IAccountStudentRoleRepository
    {
        private readonly IRoleRepository _roleRepository;

        public AccountStudentRoleRepository(
            MyContext context,
            IRoleRepository roleRepository) : base(context)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<string>> GetRolesByNimAsync(string nim)
        {
            var getAccountRoleByAccountNik = GetAllAsync().Result.Where(x => x.AccountStudentId == nim);
            var getRole = await _roleRepository.GetAllAsync();

            var getRoleByNim = from ar in getAccountRoleByAccountNik
                               join r in getRole on ar.RoleId equals r.Id
                               select r.Name;

            return getRoleByNim;
        }
    }
}
