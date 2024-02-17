using System.Threading.Tasks;
using MoneyStateCross.Models.Entities;
using SQLite;

namespace MoneyStateCross.Models.DatabaseOperations;

public static class AccountOperations
{

    public static async Task<Account[]> GetAccountsAsync(SQLiteAsyncConnection db) =>
        await db.Table<Account>().ToArrayAsync();

    public static Task<Account[]> GetAccountsAsync() =>
        GetAccountsAsync(Database.ReadonlyConnection());

    public static async Task InsertNewAsync(SQLiteAsyncConnection db, Account account) =>
        await db.InsertAsync(account);
    public static Task InsertNewAsync(Account account) =>
        InsertNewAsync(Database.ExistingConnection(), account);
    
    public static async Task ConvertAndChangeAccountCurrency(Account account, Currency newCurrency)
    {
        var db = Database.ExistingConnection();
        if (account.Currency is null)
        {
            account.Currency = newCurrency;
            account.CurrencyId = newCurrency.Id;
        }
        else
        {
            account.Balance *= CurrencyOperations.GetCurrenciesDifference(account.Currency, newCurrency);
            account.Currency = newCurrency;
            account.CurrencyId = newCurrency.Id;
        }

        await db.UpdateAsync(account);
    }
    public static async Task ChangeAccountCurrencyAsync(SQLiteAsyncConnection db, Account account, int currencyId)
    {
        account.CurrencyId = currencyId;
        await db.UpdateAsync(account);
    }

    public static Task ChangeAccountCurrencyAsync(Account account, int currencyId) =>
        ChangeAccountCurrencyAsync(Database.ExistingConnection(), account, currencyId);

    public static Task ChangeAccountGroupAsync(Account account, int newGroupId) =>
        ChangeAccountGroupAsync(Database.ExistingConnection(), account, newGroupId);
    
    public static async Task ChangeAccountGroupAsync(SQLiteAsyncConnection db, Account account, int newGroupId)
    {
        account.GroupId = newGroupId;
        await db.UpdateAsync(account);
    }
    
    public static async Task ChangeAccountsGroupsAsync(SQLiteAsyncConnection db, Account[] accounts, int[] newGroupId)
    {
        for (int i = 0; i < accounts.Length; i++)
        {
            accounts[i].GroupId = newGroupId[i];
        }

        await db.UpdateAllAsync(accounts);
    }

    public static Task ChangeAccountsGroupsAsync(Account[] accounts, int[] newGroupId) =>
        ChangeAccountsGroupsAsync(Database.ExistingConnection(), accounts, newGroupId);
    
    
    public static async Task<Account[]> GetAccountsWithGroupIdAsync(SQLiteAsyncConnection db, int groupId) =>
        await db.Table<Account>().Where(x => x.GroupId == groupId).ToArrayAsync();
    
    public static async Task FillAccountWithCurrencyAsync(SQLiteAsyncConnection db, Account account) =>
        account.Currency = await CurrencyOperations.GetCurrencyByIdAsync(db, account.CurrencyId);

}