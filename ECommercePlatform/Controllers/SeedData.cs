using Microsoft.AspNetCore.Identity;

namespace ECommercePlatform.Controllers
{
    public static class SeedData
    {
        public static async Task SeedRolesAndAdmin(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await roleManager.RoleExistsAsync("User"))
                await roleManager.CreateAsync(new IdentityRole("User"));

            var adminEmail = "admin@ecommerce.com";
            var adminUser = await userManager.FindByNameAsync(adminEmail);
            if (adminUser == null)
            {
                var user = new IdentityUser { UserName = adminEmail, Email = adminEmail };
                var result = await userManager.CreateAsync(user, "Admin123!");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
