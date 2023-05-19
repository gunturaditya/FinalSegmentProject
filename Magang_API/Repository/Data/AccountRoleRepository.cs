using Magang_API.Base;
using Magang_API.Context;

using Magang_API.Model;

using Magang_API.Repository.Contracts;

namespace Magang_API.Repository.Data
{
    public class AccountRoleRepository : BaseRepository<AccountRole, string, MyContexts>, IAccountRoleRepository
    {
        private readonly IRoleRepository _roleRepository;

        public AccountRoleRepository(
            MyContexts context,
            IRoleRepository roleRepository) : base(context)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<string>> GetRolesByNikAsync(string nik)
        {
            var getAccountRoleByAccountNik = GetAllAsync().Result.Where(x => x.AccountId == nik);
            var getRole = await _roleRepository.GetAllAsync();

            var getRoleByNik = from ar in getAccountRoleByAccountNik
                               join r in getRole on ar.RoleId equals r.Id
                               select r.Name;

            return getRoleByNik;
        }
    }
}
