using ReactiveUI;

namespace MoneyState.ViewModel;

public abstract class EditPageBase: PageBase
{
    private string _errorMessage = "";
    public string ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }
    public async void InsertAsync() => await Task.Run(Insert);
    public async void UpdateAsync() => await Task.Run(Update);
    public async void RemoveAsync() => await Task.Run(Remove);


    public abstract void Insert();
    public abstract void Update();
    public abstract void Remove();
}