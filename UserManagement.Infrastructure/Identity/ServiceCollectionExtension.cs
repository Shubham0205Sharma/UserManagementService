using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Identity
{
    public static class ServiceCollectionExtensions
    {
        public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

                await SeedIdentityData.InitializeAsync(userManager, roleManager);
            }
        }
    }
}
