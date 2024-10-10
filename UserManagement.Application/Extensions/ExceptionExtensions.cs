using UserManagement.Application.Responses;

namespace UserManagement.Application.Extensions
{
    public static class ExceptionExtensions
    {
        public static ApiResponse<T> ToApiResponse<T>(this Exception ex)
        {
            return new ApiResponse<T>(ex.Message, new List<ErrorDetails>
        {
            new ErrorDetails { Field = ex.Source, Error = ex.Message }
        });
        }
    }
}
