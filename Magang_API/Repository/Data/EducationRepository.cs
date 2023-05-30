using Magang_API.Base;
using Magang_API.Contexts;
using Magang_API.Models;
using Magang_API.Repository.Contracts;

namespace Magang_API.Repository.Data
{
    public class EducationRepository : BaseRepository<Education, int, MyContext>, IEducationRepository
    {
        public EducationRepository(MyContext context) : base(context)
        {
        }

    }
}
