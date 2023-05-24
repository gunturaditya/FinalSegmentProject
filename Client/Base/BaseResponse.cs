namespace Client.Base
{
    public class BaseResponse<TEntity>
    {
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public TEntity Data { get; set; }
    }
}
