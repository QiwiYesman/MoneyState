using MoneyState.Model.Entities;
using SQLite;

namespace MoneyState.Model.DatabaseOperations;


public static class Database
{
    private const string DefaultPath="db.db";
    public static void Init()
    {
        try
        {
            using var db = ExistingConnection();
            db.Table<Group>().First();
        }
        catch
        {
            using var db = NewConnection();
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

    public static SQLiteConnection NewConnection(string path = DefaultPath) =>
        new(path,  SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

    public static SQLiteConnection ExistingConnection(string path = DefaultPath) =>
        new(path, SQLiteOpenFlags.ReadWrite);

    public static SQLiteConnection ReadonlyConnection(string path = DefaultPath) =>
        new(path, SQLiteOpenFlags.FullMutex | SQLiteOpenFlags.ReadOnly);
    
}