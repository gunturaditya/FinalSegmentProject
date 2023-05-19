using Magang_API.Base;
using Magang_API.Context;
using Magang_API.Model;
using Magang_API.Repository.Contracts;

namespace Magang_API.Repository.Data
{
    public class AccountStudentRoleRepository : BaseRepository<AccountStudentRole, string, MyContexts>, IAccountStudentRoleRepository
    {
        private readonly IRoleRepository _roleRepository;

        public AccountStudentRoleRepository(
            MyContexts context,
            IRoleRepository roleRepository) : base(context)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<string>> GetRolesByNimAsync(string nim)
        {
            var getAccountRoleByAccountNik = GetAllAsync().Result.Where(x => x.AccountStudentId == nim);
            var getRole = await _roleRepository.GetAllAsync();

            var getRoleByNim = from ar in getAccountRoleByAccountNik
                               join r in getRole on ar.RoleStudentId equals r.Id
                               select r.Name;

            return getRoleByNim;
        }
    }
}
