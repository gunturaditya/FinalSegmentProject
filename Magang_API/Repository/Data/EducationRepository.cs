using Magang_API.Base;
using Magang_API.Context;
using Magang_API.Model;
using Magang_API.Repository.Contracts;

namespace Magang_API.Repository.Data
{
    public class EducationRepository : BaseRepository<Education, int, MyContexts>, IEducationRepository
    {
        public EducationRepository(MyContexts context) : base(context)
        {
        }

    }
}
