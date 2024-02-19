using System.Collections.ObjectModel;
using MoneyState.Model.Containers;
using MoneyState.Model.Entities;
using MoneyState.ViewModel.ObservableEntities;
using ReactiveUI;

namespace MoneyState.ViewModel;

public class CurrencyEditPageViewModel: PageBase
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
    private string _errorMessage = "";

    public string NewRatio
    {
        get => _newRatio;
        set => this.RaiseAndSetIfChanged(ref _newRatio, value);
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }

    private ObservableCurrency _currentCurrency;

    public ObservableCurrency CurrentCurrency
    {
        get => _currentCurrency;
        set => this.RaiseAndSetIfChanged(ref _currentCurrency, value);
    }
    
    public void Insert()
    {
        if (!float.TryParse(NewRatio, out var ratio))
        {
            ErrorMessage = "Введіть десяткове число";
            return;
        };
        CurrencyContainer.Insert(NewName, ratio);
        //this.RaisePropertyChanged(nameof(Currencies));
    }

    public bool TryUahEdit() => CurrentCurrency.Name == "UAH";

    public void Update()
    {
        if (TryUahEdit())
        {
            ErrorMessage = "Не можна змінювати гривню!";
            return;
        }
        if (!float.TryParse(NewRatio, out var ratio))
        {
            ErrorMessage = "Введіть десяткове число";
            return;
        };
        //CurrencyContainer.Update(CurrentCurrency, NewName, ratio);
        //this.RaisePropertyChanged(nameof(CurrencyContainer.Currencies));
    }
    public void Remove()
    {
        if (TryUahEdit())
        {
            ErrorMessage = "Не можна видаляти гривню!";
            return;
        }
        CurrencyContainer.Delete(CurrentCurrency.Id);
        //CurrencyContainer.Read();
        //this.RaisePropertyChanged(nameof(CurrencyContainer.Currencies));
        //OnRemovedCurrency?.Invoke(CurrentCurrency);
    }
    
}