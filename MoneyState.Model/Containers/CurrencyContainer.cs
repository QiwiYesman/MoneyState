using System.Collections.ObjectModel;
using MoneyState.Model.DatabaseOperations;
using MoneyState.Model.Entities;

namespace MoneyState.Model.Containers;

public class CurrencyContainer
{
    public MainContainer MainContainer { get; set; }
    public Collection<Currency> Currencies => MainContainer.Currencies;

    public void Insert(string newName, float ratio)
    {
        var inserted = CrudOperations.InsertCurrency(newName, ratio);
        Currencies.Add(inserted); 
    }
    

    public void Update(Currency currency)
    {
        CrudOperations.Update(currency);
    }
    
    public void Delete(Currency currency)
    {
        CrudOperations.Delete<Currency>(currency.Id);
        Currencies.Remove(currency);
    }
    
}