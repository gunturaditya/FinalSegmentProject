using Client.Base;
using Client.Models;
using Client.Repository.Interface;
using Client.Response;
using Client.ViewModel;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repository.Data
{
    public class AccountStudentRepository : BaseRepository<AccountStudent, string>, IAccountStudentRepository
    {

        private readonly HttpClient httpClient;
        private readonly string request;
        public AccountStudentRepository(string request = "AccountStudent/") : base(request)
        {
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7167/api/")
            };
        }

        public async Task<LoginResponse> LoginStudent(LoginVM loginVM)
        {
            LoginResponse entityVM = null;
            var data = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            using (var response = await httpClient.PostAsync(request + "Auth", data))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<LoginResponse>(apiResponse);
            }
            return entityVM;
        }

        public async Task<BaseResponse<MessageResponse>> RegisterStudent(RegisterStudentVM registerStudentVM)
        {
            BaseResponse<MessageResponse> entityVM = null;
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var data = new StringContent(JsonConvert.SerializeObject(registerStudentVM), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PostAsync(request + "Register", data))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<BaseResponse<MessageResponse>>(apiResponse);
            }
            return entityVM;
        }
    }
}
