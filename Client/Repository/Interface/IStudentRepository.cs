using Client.Base;
using Client.Models;
using Client.ViewModel;

namespace Client.Repository.Interface
{
    public interface IStudentRepository : IBaseRepository<Student, string>
    {
        Task<ResponseListVM<StudentNullAprovalVM>> GetStudentNullAproval();
    }
}
