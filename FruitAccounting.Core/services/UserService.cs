using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using FruitAccounting.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services;

public class UserService
{
    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

    public UserService(IDbContextFactory<FruitAccountingContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<(bool success, string message)> CreateUserAsync(string username, string displayName, string password, UserRole role)
    {
        try
        {
            using var context = _contextFactory.CreateDbContext();

            var existingUser = await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username);
            if (existingUser != null)
                return (false, "Username already exists");

            var user = new User
            {
                Username = username,
                DisplayName = displayName ?? username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, 11),
                Role = role,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();
            return (true, "User created successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error creating user: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> DeleteUserAsync(long userId)
    {
        try
        {
            using var context = _contextFactory.CreateDbContext();

            var user = await context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
                return (false, "User not found");

            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return (true, "User deleted successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error deleting user: {ex.Message}");
        }
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Users.AsNoTracking().OrderBy(u => u.DisplayName).ToListAsync();
    }

    public async Task<User?> GetUserAsync(long userId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == userId);
    }

    public async Task<List<UserPermission>> GetUserPermissionsAsync(long userId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.UserPermissions.AsNoTracking().Where(p => p.UserId == userId).ToListAsync();
    }

    public async Task<(bool success, string message)> SaveUserPermissionsAsync(long userId, List<UserPermission> permissions)
    {
        try
        {
            using var context = _contextFactory.CreateDbContext();

            var existingPermissions = await context.UserPermissions.Where(p => p.UserId == userId).ToListAsync();
            context.UserPermissions.RemoveRange(existingPermissions);

            foreach (var permission in permissions)
            {
                permission.UserId = userId;
                context.UserPermissions.Add(permission);
            }

            await context.SaveChangesAsync();
            return (true, "Permissions saved successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error saving permissions: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> ChangePasswordAsync(long userId, string newPassword)
    {
        try
        {
            using var context = _contextFactory.CreateDbContext();

            var user = await context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
                return (false, "User not found");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword, 11);
            await context.SaveChangesAsync();
            return (true, "Password changed successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error changing password: {ex.Message}");
        }
    }
}
