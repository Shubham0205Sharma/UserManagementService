using Microsoft.AspNetCore.Identity;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;

namespace UserManagement.Infrastructure.Identity
{
    public static class SeedIdentityData
    {
        public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            foreach (RoleType roleType in (RoleType[])System.Enum.GetValues(typeof(RoleType)))
            {
                if (!await roleManager.RoleExistsAsync(roleType.ToString()))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = roleType.ToString() });
                }
            }

            var adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@example.com",
                FirstName = "Admin",
                LastName = "User"
            };

            if (await userManager.FindByNameAsync(adminUser.UserName) == null)
            {
                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, RoleType.Admin.ToString());
                }
            }

            var regularUser = new ApplicationUser
            {
                UserName = "user",
                Email = "user@example.com",
                FirstName = "Regular",
                LastName = "User"
            };

            if (await userManager.FindByNameAsync(regularUser.UserName) == null)
            {
                var result = await userManager.CreateAsync(regularUser, "User@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(regularUser, RoleType.User.ToString());
                }
            }
        }
    }
}
