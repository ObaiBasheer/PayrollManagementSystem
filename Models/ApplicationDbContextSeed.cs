using Microsoft.AspNetCore.Identity;

namespace PayrollManagementSystem.Models
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roles = new string[] { "Admin", "Manager", "Accountant" };

            foreach (string role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var users = new List<IdentityUser>
        {
            new IdentityUser { UserName = "admin@example.com", Email = "admin@example.com" },
            new IdentityUser { UserName = "manager@example.com", Email = "manager@example.com" },
            new IdentityUser { UserName = "accountant@example.com", Email = "accountant@example.com" }
        };

            foreach (var user in users)
            {
                if (await userManager.FindByEmailAsync(user.Email) == null)
                {
                    await userManager.CreateAsync(user, "Password123!");
                    if (user.Email == "admin@example.com")
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                    else if (user.Email == "manager@example.com")
                    {
                        await userManager.AddToRoleAsync(user, "Manager");
                    }
                    else if (user.Email == "accountant@example.com")
                    {
                        await userManager.AddToRoleAsync(user, "Accountant");
                    }
                }
            }
        }
    }

}
