using System.Collections.ObjectModel;
using MoneyState.Model.Containers;
using MoneyState.ViewModel.ObservableEntities;
using ReactiveUI;

namespace MoneyState.ViewModel;

public class CurrencyEditPageViewModel: EditPageBase
{

    public CurrencyEditPageViewModel()
    {
        CurrencyContainer = new(); 
    }

    public CurrencyEditPageViewModel(CurrencyContainer<ObservableCurrency> currencyContainer)
    {
        CurrencyContainer = currencyContainer;
    }
    public CurrencyContainer<ObservableCurrency> CurrencyContainer { get; set; }

    public Collection<ObservableCurrency> Currencies => CurrencyContainer.Collection;
    
    private string _newName = "";

    public string NewName
    {
        get => _newName;
        set => this.RaiseAndSetIfChanged(ref _newName, value);
    }

    private string _newRatio = "";

    public string NewRatio
    {
        get => _newRatio;
        set => this.RaiseAndSetIfChanged(ref _newRatio, value);
    }
    

    private ObservableCurrency _currentCurrency;

    public ObservableCurrency CurrentCurrency
    {
        get => _currentCurrency;
        set => this.RaiseAndSetIfChanged(ref _currentCurrency, value);
    }
    public bool TryUahEdit() => CurrentCurrency.Name == "UAH";

    public override void Insert()
    {
        if (!float.TryParse(NewRatio, out var ratio))
        {
            ErrorMessage = "Введіть десяткове число";
            return;
        }
        CurrencyContainer.Insert(NewName, ratio); 
    }

  
    public override void Update()
    {
        if (TryUahEdit())
        {
            ErrorMessage = "Не можна змінювати гривню!";
            return;
        }

        bool toEditName = !string.IsNullOrEmpty(NewName);
        bool toEditRatio = !string.IsNullOrEmpty(NewRatio);
        if (toEditRatio)
        {
            if(!float.TryParse(NewRatio, out var ratio))
            {
                ErrorMessage = "Введіть десяткове число";
                return; 
            }
            CurrentCurrency.RatioToUah = ratio;
        }

        if (toEditName)
        {
            CurrentCurrency.Name = NewName;    
        }
        CurrencyContainer.Update(CurrentCurrency);
    }
    public override void Remove()
    {
        if (TryUahEdit())
        {
            ErrorMessage = "Не можна видаляти гривню!";
            return;
        }
        CurrencyContainer.Delete(CurrentCurrency);
    }
}