using Client.Base;
using Client.Models;
using Client.Response;
using Client.ViewModel;

namespace Client.Repository.Interface
{
    public interface IAccountStudentRepository : IBaseRepository<AccountStudent, string>
    {
        Task<LoginResponse> LoginStudent(LoginVM loginVM);
        Task<BaseResponse<MessageResponse>> RegisterStudent(RegisterStudentVM registerStudentVM);
    }
}
