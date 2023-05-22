using Client.Base;
using Client.Models;
using Client.Repository.Interface;

namespace Client.Repository.Data
{
    public class UniversityRepository : BaseRepository<University, int>, IUniversityRepository
    {


        public UniversityRepository(string request = "University/") : base(request)
        {

        }



    }
}
