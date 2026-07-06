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

        public async Task<(bool success, string message)> ChangePasswordAsync(string username, string currentPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
                return (false, "New password must be at least 6 characters.");

            await using var db = await _contextFactory.CreateDbContextAsync();

            var user = await db.Users.FirstOrDefaultAsync(u => u.Username == username && u.IsActive);
            if (user == null)
                return (false, "User not found.");

            if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash))
                return (false, "Current password is incorrect.");

            if (BCrypt.Net.BCrypt.Verify(newPassword, user.PasswordHash))
                return (false, "New password must be different from current password.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword, workFactor: 11);
            await db.SaveChangesAsync();

            return (true, "Password changed successfully.");
        }

        public async Task<List<(long CompanyId, string CompanyCode, string CompanyName)>> GetUserCompaniesAsync(long userId)
        {
            await using var db = await _contextFactory.CreateDbContextAsync();

            var companies = await db.Users
                .Where(u => u.UserId == userId)
                .SelectMany(u => u.Companies)
                .Select(c => new { c.CompanyId, c.Code, c.Name })
                .ToListAsync();

            return companies.Select(c => (c.CompanyId, c.Code, c.Name)).ToList();
        }
    }
}
