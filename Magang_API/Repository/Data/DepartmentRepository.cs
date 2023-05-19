using Magang_API.Base;
using Magang_API.Context;
using Magang_API.Model;
using Magang_API.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Magang_API.Repository.Data
{
    public class DepartmentRepository : BaseRepository<Department, int, MyContexts>, IDepartmentRepository
    {
        public DepartmentRepository(MyContexts context) : base(context) { }
        public async Task<Department?> GetByNameAsync(string name)
        {
            return await _context.Set<Department>().FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<bool> IsNameExistAsync(string name)
        {
            return await _context.Set<Department>().AnyAsync(x => x.Name == name);
        }

        public override async Task<Department?> InsertAsync(Department entity)
        {
            if (await IsNameExistAsync(entity.Name))
            {
                return await GetByNameAsync(entity.Name);
            }
            return await base.InsertAsync(entity);
        }
    }
}
