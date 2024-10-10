using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace UserManagement.Domain.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
    }
}

