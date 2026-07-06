using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace FruitAccounting.Core.services
{
    public class AuthService
    {
        private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

        public AuthService(IDbContextFactory<FruitAccountingContext> contextFactory) => _contextFactory = contextFactory;

        public async Task<User?> LoginAsync(string username, string password)
        {
            await using var db = await _contextFactory.CreateDbContextAsync();

            var user = await db.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);

            if (user == null) return null;

            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)
                ? user
                : null;
        }

        public async Task<(bool, string)> ChangePasswordAsync(string username, string currentPassword, string newPassword)
        {
            await using var db = await _contextFactory.CreateDbContextAsync();

            var user = await db.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
                return (false, "User not found.");

            if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash))
                return (false, "Current password is incorrect.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword, workFactor: 11);
            db.Users.Update(user);
            await db.SaveChangesAsync();

            return (true, "Password changed successfully.");
        }

        public async Task<List<Company>> GetUserCompaniesAsync(long userId)
        {
            await using var db = await _contextFactory.CreateDbContextAsync();

            var user = await db.Users
                .Include(u => u.Companies)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            return user?.Companies.ToList() ?? new List<Company>();
        }
    }
}
