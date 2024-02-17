using MoneyStateCross.Models.Entities;

namespace MoneyStateCross.Models.Operations;

public class ConvertOperation
{
    public static void ChangeCurrency(Account account, Currency newCurrency)
    {
        account.Balance *= account.Currency?.RatioToUah ?? 1;
        account.Currency = newCurrency;
        account.Balance /= newCurrency.RatioToUah;
    }
}