using System.Collections.ObjectModel;
using MoneyState.Model.DatabaseOperations;
using MoneyState.Model.Entities;

namespace MoneyState.Model.Containers;

public class CurrencyContainer<TCurrency> : EntityContainerBase<TCurrency> where TCurrency: ICurrency, new()
{

    public void Insert(string newName, float ratio)
    {
        var inserted = CrudOperations.InsertCurrency<TCurrency>(newName, ratio);
        Collection.Add(inserted); 
    }
    

    public void Update(TCurrency currency)
    {
        CrudOperations.Update(currency);
    }
    
    public void Delete(TCurrency currency)
    {
        CrudOperations.Delete<TCurrency>(currency.Id);
        Collection.Remove(currency);
    }
    
}