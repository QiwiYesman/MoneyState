using SQLite;

namespace MoneyState.Model.Entities;

public class AccountLog: EntityBase
{
    [Indexed]
    public int AccountId { get; set; }
    
    public string Message { get; set; } = "";
    public DateTime OperationDate { get; set; } = DateTime.Now;

    public float BalanceChange { get; set; } = 0.0f;
}