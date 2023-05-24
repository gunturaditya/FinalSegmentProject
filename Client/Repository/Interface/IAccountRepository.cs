using Client.Base;
using Client.Models;
using Client.Response;
using Client.ViewModel;

namespace Client.Repository.Interface
{
    public interface IAccountRepository : IBaseRepository<Account, string>
    {
        Task<LoginResponse> LoginEmployee(LoginVM loginVM);
        Task<BaseResponse<MessageResponse>> RegisterEmployee(RegisterVM registerVM);
    }
}
