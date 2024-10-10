using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {

        public string LastName { get; set; }

        public string FirstName { get; set; }

    }
}
