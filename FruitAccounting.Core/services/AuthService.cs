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
    }
}
