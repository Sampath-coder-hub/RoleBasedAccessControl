using Assignment.Models;
using Assignment.Service;

namespace Assignment.Data
{
    public static class DbInitializer
    {
        public static void SeedUsers(ApplicationDbContext context, IAuthService authService)
        {
            if (context.Users.Any()) return; // Already seeded

            var users = new List<User>();

            // Admin
            authService.CreatePasswordHash("admin123", out var hash1, out var salt1);
            users.Add(new User { Username = "admin", PasswordHash = hash1, PasswordSalt = salt1, Role = "Admin" });

            // Editor
            authService.CreatePasswordHash("editor123", out var hash2, out var salt2);
            users.Add(new User { Username = "editor", PasswordHash = hash2, PasswordSalt = salt2, Role = "Editor" });

            // Viewer
            authService.CreatePasswordHash("viewer123", out var hash3, out var salt3);
            users.Add(new User { Username = "viewer", PasswordHash = hash3, PasswordSalt = salt3, Role = "Viewer" });

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }

}
