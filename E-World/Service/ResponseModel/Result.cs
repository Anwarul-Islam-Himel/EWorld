
namespace Service.ResponseModel
{
    public class Result<T>
    {
        public int StatusCode { get; set; }
        public T Response { get; set; }
    }
}
