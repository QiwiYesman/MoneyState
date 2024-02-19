using MoneyState.Model.Entities;
using SQLite;

namespace MoneyState.Model.DatabaseOperations;


public static class Database
{
    private const string DefaultPath="db2.db";
    public static bool Init(Type[] types)
    {
        try
        {
            using var db = ExistingConnection();
            var groupInfo = db.GetTableInfo("Group");
            var accountInfo = db.GetTableInfo("Account");
            var currencyInfo = db.GetTableInfo("Currency");
            if (groupInfo.Count == 0 || accountInfo.Count == 0 || currencyInfo.Count == 0)
            {
                using var dbNew = NewConnection();
                CreateNewTables(dbNew, types);
                return true;
            }
        }
        catch
        {
            using var db = NewConnection();
            CreateNewTables(db, types);
            return true;
            //LoadDefaults(db);
        }
        return false;
    }
    
    
    public static void CreateNewTables(SQLiteConnection db, Type[] types)
    {
        foreach (var t in types)
        {
            db.CreateTable(t);
        }
    }

    public static void LoadDefaultCurrency<TCurrency>() where TCurrency : ICurrency, new()
    {
        using var db = ExistingConnection();
        db.Insert(new TCurrency() { Name = "UAH", RatioToUah = 1.0f, Id = 0});
    }
    
    public static void LoadDefaultGroup<TGroup>() where TGroup : IGroup, new() 
    {
        using var db = ExistingConnection();
        db.Insert(new TGroup() { Name = "Усі", Id = 0});
    }
    public static SQLiteConnection NewConnection(string path = DefaultPath) =>
        new(path,  SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

    public static SQLiteConnection ExistingConnection(string path = DefaultPath) =>
        new(path, SQLiteOpenFlags.ReadWrite);

    public static SQLiteConnection ReadonlyConnection(string path = DefaultPath) =>
        new(path, SQLiteOpenFlags.FullMutex | SQLiteOpenFlags.ReadOnly);
    
}