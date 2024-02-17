using System;
using SQLite;

namespace MoneyStateCross.Models.Entities;

public class Group
{
    private Account[] _accounts = Array.Empty<Account>();
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; } = "";

    [Ignore]
    public Account[] Accounts
    {
        get => _accounts;
        set
        {
            _accounts = value;
            foreach (var account in _accounts)
            {
                account.Group = this;
            }
        }
    }
    
}