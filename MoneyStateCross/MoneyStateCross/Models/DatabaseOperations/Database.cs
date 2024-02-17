using System.Threading.Tasks;
using MoneyStateCross.Models.Entities;
using SQLite;

namespace MoneyStateCross.Models.DatabaseOperations;

public static class Database
{
    private const string DefaultPath="db.db";

    public static void Init()
    {
        try
        {
            var db = new SQLiteConnection(DefaultPath, SQLiteOpenFlags.ReadWrite);
            db.Table<Account>().First();
            db.Table<Group>().First();
            db.Table<Currency>().First();
        }
        catch
        {
            var db = new SQLiteConnection(DefaultPath, SQLiteOpenFlags.Create| SQLiteOpenFlags.ReadWrite);
            CreateNewTables(db);
            LoadDefaults(db);
        }
    }
    
    
    public static void CreateNewTables(SQLiteConnection db)
    {
        db.CreateTable<Group>();
        db.CreateTable<Currency>();
        db.CreateTable<Account>();
    }

    public static void LoadDefaults(SQLiteConnection db)
    {
        db.Insert(new Currency() { Name = "UAH", RatioToUah = 1.0f, Id = 0});
        db.Insert(new Group() { Name = "Усі", Id = 0});
    }

    public static SQLiteAsyncConnection NewConnection(string path = DefaultPath) =>
        new(path,  SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

    public static SQLiteAsyncConnection ExistingConnection(string path = DefaultPath) =>
        new(path, SQLiteOpenFlags.ReadWrite);

    public static SQLiteAsyncConnection ReadonlyConnection(string path = DefaultPath) =>
        new(path, SQLiteOpenFlags.FullMutex | SQLiteOpenFlags.ReadOnly);
    
}