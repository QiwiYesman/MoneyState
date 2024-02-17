using System.Collections;
using MoneyState.Model.Entities;

namespace MoneyState.Model.DatabaseOperations;

public static class CrudOperations
{
    public static T[] GetArray<T>() where T: EntityBase, new()
    {
        using var connection = Database.ReadonlyConnection();
        return connection.Table<T>().ToArray();
    }

    public static T GetSingle<T>(int id) where T : EntityBase, new()
    {
        using var connection = Database.ReadonlyConnection();
        return connection.Table<T>().First(x => x.Id == id);
    }

    public static void Delete<T>(int id) where T : EntityBase
    {
        using var connection = Database.ExistingConnection();
        connection.Delete<T>(id);
    }

    public static void Update<T>(T obj) where T : EntityBase
    {
        using var connection = Database.ExistingConnection();
        connection.Update(obj);
    }

    public static Group InsertGroup(string groupName)
    {
        using var connection = Database.ExistingConnection();
        var group = new Group()
        {
            Id = 0,
            Name = groupName
        };
        connection.Insert(group);
        return group;
    }

    public static Currency InsertCurrency(string currencyName, float ratioConversion)
    {
        using var connection = Database.ExistingConnection();
        var currency = new Currency()
        {
            Id = 0,
            Name = currencyName,
            RatioToUah = ratioConversion
        }; 
        connection.Insert(currency);
        return currency;
    }
    
    public static Account InsertAccount(string name, int groupId, int currencyId)
    {
        using var connection = Database.ExistingConnection();
        var account = new Account
        {
            Name = name,
            GroupId = groupId,
            CurrencyId = currencyId
        }; 
        connection.Insert(account);
        return account;
    }
    
}