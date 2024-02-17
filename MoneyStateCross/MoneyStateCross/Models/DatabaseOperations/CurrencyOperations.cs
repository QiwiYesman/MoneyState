using System.Threading.Tasks;
using MoneyStateCross.Models.Entities;
using SQLite;

namespace MoneyStateCross.Models.DatabaseOperations;

public static class CurrencyOperations
{
    public static async Task InsertNewAsync(string name, float ratio)
    {
        var db = Database.ExistingConnection();
        await db.InsertAsync(new Currency()
        {
            RatioToUah = ratio,
            Name = name,
            Id = 0
        });
    }
    public static async Task<Currency> GetCurrencyByIdAsync(SQLiteAsyncConnection db, int currencyId)
        => await db.Table<Currency>().Where(x => x.Id == currencyId).FirstAsync();

    public static async Task<Currency[]> GetCurrenciesAsync()
    {
        var db = Database.ReadonlyConnection();
        return await db.Table<Currency>().ToArrayAsync();
    }
    public static float GetCurrenciesDifference(Currency first, Currency second)
        => second.RatioToUah / first.RatioToUah;
}