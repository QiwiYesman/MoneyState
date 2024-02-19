using MoneyState.Model.Entities;
using ReactiveUI;
using SQLite;

namespace MoneyState.ViewModel.ObservableEntities;

[Table("Currency")]
public class ObservableCurrency: ReactiveObject, ICurrency
{
    private string _name = "";
    private float _ratio = 1.0f;
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public float RatioToUah
    {
        get => _ratio;
        set => this.RaiseAndSetIfChanged(ref _ratio, value);
    }
}