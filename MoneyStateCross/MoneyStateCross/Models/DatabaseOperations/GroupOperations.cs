using System.Threading.Tasks;
using MoneyStateCross.Models.Entities;
using SQLite;

namespace MoneyStateCross.Models.DatabaseOperations;

public static class GroupOperations
{
    public static async Task InsertNewAsync(string name)
    {
        var db = Database.ExistingConnection();
        await db.InsertAsync(new Group()
        {
            Name = name,
            Id = 0
        });
    }
    
    public static async Task<Group[]> GetFilledGroupsAsync()
    {
        var db = Database.ReadonlyConnection();
        var groups = await db.Table<Group>().ToArrayAsync();
        foreach (var group in groups)
        {
            await FillGroupAccountCurrenciesAsync(db, group);
        }

        return groups;
    }
    
    public static async Task FillGroupAccountCurrenciesAsync(SQLiteAsyncConnection db, Group group)
    {
        await FillGroupAccountsAsync(db, group);
        foreach (var account in group.Accounts)
        {
            await AccountOperations.FillAccountWithCurrencyAsync(db, account);
        }
    }
    
    public static async Task FillGroupAccountsAsync(SQLiteAsyncConnection db, Group group) =>
        group.Accounts = await AccountOperations.GetAccountsWithGroupIdAsync(db, group.Id);

    public static Task FillGroupAccountsAsync(Group group) =>
        FillGroupAccountsAsync(Database.ReadonlyConnection(), group);

    public static async Task FillGroupsAccountsAsync(SQLiteAsyncConnection db, Group[] groups)
    {
        foreach (var group in groups)
        {
            await FillGroupAccountsAsync(db, group);
        }
    }

    public static Task FillGroupsAccountsAsync(Group[] groups) =>
        FillGroupsAccountsAsync(Database.ReadonlyConnection(), groups);
    public static async Task<Group[]> GetGroupsAsync()
    {
        var db = Database.ReadonlyConnection();
        return await db.Table<Group>().ToArrayAsync();
    }
    
}