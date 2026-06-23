using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Data.Entities.Identity;

namespace Infrastructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<User> _userManager)
        {
            var adminUser = await _userManager.FindByNameAsync("admin");
            if (adminUser == null)
            {
                var defaultuser = new User()
                {
                    UserName = "admin",
                    Email = "admin@project.com",
                    FullName="",
                    Country="Egypt",
                    PhoneNumber="123456",
                    Address="Egypt",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await _userManager.CreateAsync(defaultuser, "M123_m");
                await _userManager.AddToRoleAsync(defaultuser, "Admin");
            }

            // Create a second admin user
            var admin2User = await _userManager.FindByNameAsync("admin2");
            if (admin2User == null)
            {
                var admin2 = new User()
                {
                    UserName = "admin2",
                    Email = "admin2@project.com",
                    FullName = "Second Admin",
                    Country = "Egypt",
                    PhoneNumber = "123456789",
                    Address = "Egypt",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await _userManager.CreateAsync(admin2, "M123_m");
                await _userManager.AddToRoleAsync(admin2, "Admin");
            }
        }
    }
}
