using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public static class Seed
    {
        public static async Task SeedUsers(DataContext ctx)
        {
            // if (await ctx.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();

                user.Username = user.Username!.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("PassPass"));
                user.PasswordSalt = hmac.Key;

                ctx.Users.Add(user);

            }
            await ctx.SaveChangesAsync();

        }

        
    }
}