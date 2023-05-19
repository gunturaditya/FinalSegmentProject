using Magang_API.Base;
using Magang_API.Context;

using Magang_API.Model;

using Magang_API.Repository.Contracts;
using MessagePack;
using Microsoft.EntityFrameworkCore;

namespace Magang_API.Repository.Data
{
    public class UniversityRepository : BaseRepository<University, int, MyContexts>,IUniversityRepository
    {
        public UniversityRepository(MyContexts context) : base(context) { }
        public async Task<University?> GetByNameAsync(string name)
        {
            return await _context.Set<University>().FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<bool> IsNameExistAsync(string name)
        {
            return await _context.Set<University>().AnyAsync(x => x.Name == name);
        }

        public override async Task<University?> InsertAsync(University entity)
        {
            if (await IsNameExistAsync(entity.Name))
            {
                return await GetByNameAsync(entity.Name);
            }
            return await base.InsertAsync(entity);
        }


    }
}
