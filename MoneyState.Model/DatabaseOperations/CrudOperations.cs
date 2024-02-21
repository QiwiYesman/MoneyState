using System.Collections;
using MoneyState.Model.Entities;

namespace MoneyState.Model.DatabaseOperations;

public static class CrudOperations
{
    public static TEntity[] GetArray<TEntity>() where TEntity: new()
    {
        using var connection = Database.ReadonlyConnection();
        return connection.Table<TEntity>().ToArray();
    }

    public static TEntity GetSingle<TEntity>(int id) where TEntity : EntityBase, new()
    {
        using var connection = Database.ReadonlyConnection();
        return connection.Table<TEntity>().First(x => x.Id == id);
    }

    public static void Delete<TEntity>(int id) where TEntity : EntityBase
    {
        using var connection = Database.ExistingConnection();
        connection.Delete<TEntity>(id);
    }

    public static void Update<TEntity>(TEntity obj) where TEntity : EntityBase
    {
        using var connection = Database.ExistingConnection();
        connection.Update(obj);
    }

    public static TGroup InsertGroup<TGroup>(string groupName)
        where TGroup: Group, new()
    {
        using var connection = Database.ExistingConnection();
        var group = new TGroup
        {
            Id = 0,
            Name = groupName
        };
        connection.Insert(group);
        return group;
    }

    public static TCurrency InsertCurrency<TCurrency>(string currencyName, float ratioConversion)
        where TCurrency: Currency, new()
    {
        using var connection = Database.ExistingConnection();
        var currency = new TCurrency
        {
            Id = 0,
            Name = currencyName,
            RatioToUah = ratioConversion
        }; 
        connection.Insert(currency);
        return currency;
    }
    
    public static TAccount InsertAccount<TAccount>(string name, float balance, int groupId, int currencyId)
        where TAccount: Account, new()
    {
        using var connection = Database.ExistingConnection();
        var account = new TAccount
        {
            Name = name,
            Balance = balance,
            GroupId = groupId,
            CurrencyId = currencyId
        }; 
        connection.Insert(account);
        return account;
    }
    
}