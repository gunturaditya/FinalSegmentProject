using Client.Base;
using Client.Models;
using Client.Repository.Interface;
using Client.ViewModel;
using Newtonsoft.Json;

namespace Client.Repository.Data
{
    public class StudentRepository : BaseRepository<Student, string>, IStudentRepository
    {

        private readonly HttpClient httpClient;
        private readonly string request;
        public StudentRepository(string request = "Student/") : base(request)
        {
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7004/api/")
            };
        }

        public async Task<ResponseListVM<StudentNullAprovalVM>> GetStudentNullAproval()
        {
            ResponseListVM<StudentNullAprovalVM> entityVM = null;
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            using (var response = await httpClient.GetAsync(request + "StudentNullAproval"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseListVM<StudentNullAprovalVM>>(apiResponse);
            }
            return entityVM;
        }
    }
}
