using System.ComponentModel;
using System.Runtime.CompilerServices;
using MoneyState.Model.Entities;
using ReactiveUI;
using SQLite;

namespace MoneyState.ViewModel.ObservableEntities;

[Table("Currency")]
public class ObservableCurrency: Currency, INotifyPropertyChanged
{
    private string _name = "";
    private float _ratio = 1.0f;

    public override string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public override float RatioToUah
    {
        get => _ratio;
        set => SetField(ref _ratio, value);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}