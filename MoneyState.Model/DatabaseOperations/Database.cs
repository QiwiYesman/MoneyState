using MoneyState.Model.Entities;
using SQLite;

namespace MoneyState.Model.DatabaseOperations;


public static class Database
{
    private const string DefaultPath="db3.db";
    public static bool Init()
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
                CreateNewTables(dbNew);
                LoadDefaultGroup();
                LoadDefaultCurrency();
                return true;
            }
        }
        catch
        {
            using var db = NewConnection();
            CreateNewTables(db);
            LoadDefaultGroup();
            LoadDefaultCurrency();
            return true;
        }
        return false;
    }
    
    
    public static void CreateNewTables(SQLiteConnection db)
    {
        db.CreateTable<Group>();
        db.CreateTable<Currency>();
        db.CreateTable<Account>();
    }

    public static void LoadDefaultCurrency()
    {
        using var db = ExistingConnection();
        db.Insert(new Currency() { Name = "UAH", RatioToUah = 1.0f, Id = 0});
    }
    
    public static void LoadDefaultGroup() 
    {
        using var db = ExistingConnection();
        db.Insert(new Group() { Name = "Усі", Id = 0});
    }
    public static SQLiteConnection NewConnection(string path = DefaultPath) =>
        new(path,  SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

    public static SQLiteConnection ExistingConnection(string path = DefaultPath) =>
        new(path, SQLiteOpenFlags.ReadWrite);

    public static SQLiteConnection ReadonlyConnection(string path = DefaultPath) =>
        new(path, SQLiteOpenFlags.FullMutex | SQLiteOpenFlags.ReadOnly);
    
}