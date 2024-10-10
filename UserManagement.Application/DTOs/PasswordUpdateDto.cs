
namespace UserManagement.Application.DTOs
{
    public class PasswordUpdateDto
    {
        public string UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
