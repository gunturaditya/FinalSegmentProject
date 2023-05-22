using Client.ViewModel;

namespace Client.Base
{
    public interface IBaseRepository<TEntity, Key>
        where TEntity : class
    {
        Task<ResponseListVM<TEntity>> Get();
        Task<ResponseViewModel<TEntity>> Get(Key id);
        Task<ResponseMessageVM> Post(TEntity entity);
        Task<ResponseMessageVM> Put(Key id, TEntity entity);
        Task<ResponseMessageVM> Delete(Key id);
    }
}
