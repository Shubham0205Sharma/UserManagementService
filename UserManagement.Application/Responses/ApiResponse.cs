
namespace UserManagement.Application.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<ErrorDetails> Errors { get; set; } = new List<ErrorDetails>();

        public ApiResponse() { }

        public ApiResponse(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public ApiResponse(string message, List<ErrorDetails> errors)
        {
            Success = false;
            Message = message;
            Errors = errors;
        }
    }

    public class ErrorDetails
    {
        public string Field { get; set; }
        public string Error { get; set; }
    }
}
