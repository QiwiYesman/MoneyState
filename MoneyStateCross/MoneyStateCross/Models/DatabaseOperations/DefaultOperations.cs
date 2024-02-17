using System.Threading.Tasks;
using MoneyStateCross.Models.Entities;

namespace MoneyStateCross.Models.DatabaseOperations;

public static class DefaultOperations
{
    public static async Task Update<TEntity>(TEntity entity)
    {
        var db = Database.ExistingConnection();
        await db.UpdateAsync(entity);
    }
    
    public static async Task UpdateAll<TEntity>(TEntity[] entity)
    {
        var db = Database.ExistingConnection();
        await db.UpdateAllAsync(entity);
    }

    public static async Task Delete<TEntity>(int id)
    {
        var db = Database.ExistingConnection();
        await db.DeleteAsync<TEntity>(id);
    }
    
    
    public static async Task InsertGroupAsync(string name)
    {
        var db = Database.ExistingConnection();
        await db.InsertAsync(new Group()
        {
            Name = name,
            Id = 0
        });
    }
    
    public static async Task InsertCurrencyAsync(string name, float ratio)
    {
        var db = Database.ExistingConnection();
        await db.InsertAsync(new Currency()
        {
            RatioToUah = ratio,
            Name = name,
            Id = 0
        });
    }
    
}