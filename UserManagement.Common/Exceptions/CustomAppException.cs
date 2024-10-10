
namespace UserManagement.Common.Exceptions
{
    public class CustomAppException : Exception
    {
        public CustomAppException(string message) : base(message) { }

        public CustomAppException(string message, Exception innerException) : base(message, innerException) { }
    }
}
