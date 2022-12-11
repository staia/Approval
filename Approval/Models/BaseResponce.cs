namespace Approval.Models
{
    public class BaseResponce<T>
    {
        public string ErrorMessage { get; set; }
        public T Date { get; set; }
        public StatusCode Status { get; set; }
    }
    public enum StatusCode    // фантик
    {
       Ok = 200,
       NotFound = 404,
       Error = 505
    }

}
