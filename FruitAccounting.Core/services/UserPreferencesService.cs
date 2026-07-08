using FruitAccounting.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FruitAccounting.Core.services
{
    public class UserPreferencesService
    {
        private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

        public UserPreferencesService(IDbContextFactory<FruitAccountingContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<string>> GetToolbarButtonsAsync(long userId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var prefs = await context.UserPreferences
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.UserId == userId && p.PreferenceKey == "toolbar_buttons");

                if (prefs == null)
                    return GetDefaultToolbarButtons();

                return JsonSerializer.Deserialize<List<string>>(prefs.PreferenceValue) ?? GetDefaultToolbarButtons();
            }
            catch (Exception ex) when (ex.InnerException?.Message.Contains("relation \"user_preferences\" does not exist") == true)
            {
                // Table doesn't exist yet - return default preferences
                return GetDefaultToolbarButtons();
            }
            catch
            {
                // Any other error - return default preferences
                return GetDefaultToolbarButtons();
            }
        }

        public async Task<bool> SaveToolbarButtonsAsync(long userId, List<string> buttons)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var prefs = await context.UserPreferences
                    .FirstOrDefaultAsync(p => p.UserId == userId && p.PreferenceKey == "toolbar_buttons");

                if (prefs == null)
                {
                    prefs = new Data.Entities.UserPreference
                    {
                        UserId = userId,
                        PreferenceKey = "toolbar_buttons",
                        PreferenceValue = JsonSerializer.Serialize(buttons),
                        CreatedAt = DateTime.UtcNow
                    };
                    context.UserPreferences.Add(prefs);
                }
                else
                {
                    prefs.PreferenceValue = JsonSerializer.Serialize(buttons);
                    context.UserPreferences.Update(prefs);
                }

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) when (ex.InnerException?.Message.Contains("relation \"user_preferences\" does not exist") == true)
            {
                // Table doesn't exist - silently fail and return true (preferences will be lost on restart)
                return true;
            }
            catch
            {
                // Any other error - return false but don't crash
                return false;
            }
        }

        public static List<string> GetDefaultToolbarButtons()
        {
            return new List<string>
            {
                "Purchase",
                "Sales",
                "Sale Update",
                "Cash Payment",
                "Cash Receipt",
                "Freight Payment",
                "Bank Payment",
                "Bank Receipt",
                "Stock",
                "Exit"
            };
        }

        public static List<string> GetAllAvailableButtons()
        {
            return new List<string>
            {
                "Purchase",
                "Sales",
                "Sale Update",
                "Cash Payment",
                "Cash Receipt",
                "Freight Payment",
                "Bank Payment",
                "Bank Receipt",
                "Stock",
                "J.V",
                "Crate Receipt",
                "Crate Delivery",
                "Crate Ledger",
                "Ledger",
                "Calculator"
            };
        }
    }
}
