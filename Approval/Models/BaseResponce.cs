namespace Approval.Models
{
    public class BaseResponce<T>
    {
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
        public StatusCode Status { get; set; }
    }
    public enum StatusCode
    {
        Ok = 200,
        NotFound = 404,
        Error = 500

    }
}
