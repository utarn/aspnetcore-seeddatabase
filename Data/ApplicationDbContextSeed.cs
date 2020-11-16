using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace aspnetcore_seeddatabase.Data
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<IdentityUser> userManager,
                                                      RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new IdentityUser()
            {
                UserName = "admin@utarn.com",
                Email = "admin@utarn.com",
                EmailConfirmed = true
            };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                var result = await userManager.CreateAsync(defaultUser, "Sample@pp!!!000");
            }
            else
            {
                defaultUser = await userManager.FindByNameAsync("admin@utarn.com");
            }

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            if (!await userManager.IsInRoleAsync(defaultUser, "Admin"))
            {
                await userManager.AddToRoleAsync(defaultUser, "Admin");
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            var item = context.Items.Find(-1);
            if (item == null)
            {
                item = new Item()
                {
                    Id = -1,
                    Name = "Sample data"
                };
                context.Items.Add(item);
            }

            await context.SaveChangesAsync();
        }
    }
}