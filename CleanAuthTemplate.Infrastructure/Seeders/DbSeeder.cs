using CleanAuthTemplate.Domain.Constants;
using CleanAuthTemplate.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CleanAuthTemplate.Infrastructure.Seeders
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndUsersAsync(IServiceProvider serviceProvider)
        {
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("DbSeeder");

            try
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();

                logger.LogInformation("Seeding database...");

                // Seed Roles
                await SeedRoleAsync(roleManager, Roles.Admin);
                await SeedRoleAsync(roleManager, Roles.User);

                // Seed Admin User
                var adminEmail = configuration["AdminUser:Email"] ?? "admin@localhost.com";
                var adminPassword = configuration["AdminUser:Password"] ?? "123456";

                await SeedUserAsync(userManager, logger, new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "System",
                    LastName = "Admin",
                    EmailConfirmed = true
                }, adminPassword, Roles.Admin);

                // Seed Regular User
                var userEmail = configuration["DefaultUser:Email"] ?? "user@localhost.com";
                var userPassword = configuration["DefaultUser:Password"] ?? "123456";

                await SeedUserAsync(userManager, logger, new ApplicationUser
                {
                    UserName = userEmail,
                    Email = userEmail,
                    FirstName = "Regular",
                    LastName = "User",
                    EmailConfirmed = true
                }, userPassword, Roles.User);

                logger.LogInformation("Database seeding completed successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }

        // Helper Method to Create Roles
        private static async Task SeedRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Helper Method to Create Users
        private static async Task SeedUserAsync(
            UserManager<ApplicationUser> userManager,
            ILogger logger,
            ApplicationUser user,
            string password,
            string role)
        {
            var existingUser = await userManager.FindByEmailAsync(user.Email!);

            if (existingUser == null)
            {
                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    logger.LogInformation($"User {user.Email} created successfully.");
                    await userManager.AddToRoleAsync(user, role);
                }
                else
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    logger.LogError($"Error creating user {user.Email}: {errors}");
                }
            }
        }
    }
}