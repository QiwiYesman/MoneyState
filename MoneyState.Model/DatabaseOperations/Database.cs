using MoneyState.Model.Entities;
using SQLite;

namespace MoneyState.Model.DatabaseOperations;


public static class Database
{
    private const string DbName="db.db";

    private static string DbPath => Path.Combine(AppContext.BaseDirectory, DbName);
    public static bool Init()
    {
        try
        {
            using var db = ExistingConnection();
            var groupInfo = db.GetTableInfo("Group");
            var accountInfo = db.GetTableInfo("Account");
            var currencyInfo = db.GetTableInfo("Currency");
            var logInfo = db.GetTableInfo("AccountLog");
            if (groupInfo.Count == 0 || accountInfo.Count == 0 || currencyInfo.Count == 0 || logInfo.Count == 0)
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
        db.CreateTable<AccountLog>();
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
    public static SQLiteConnection NewConnection() =>
        new(DbPath,  SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

    public static SQLiteConnection ExistingConnection() =>
        new(DbPath, SQLiteOpenFlags.ReadWrite);

    public static SQLiteConnection ReadonlyConnection() =>
        new(DbPath, SQLiteOpenFlags.FullMutex | SQLiteOpenFlags.ReadOnly);
    
}