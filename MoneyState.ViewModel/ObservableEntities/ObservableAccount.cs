using System.ComponentModel;
using System.Runtime.CompilerServices;
using MoneyState.Model.Entities;
using ReactiveUI;
using SQLite;

namespace MoneyState.ViewModel.ObservableEntities;

[Table("Account")]
public class ObservableAccount : Account, INotifyPropertyChanged
{
    private string _name = "";
    private Group? _group;
    private Currency? _currency;
    private float _balance = 0.0f;

    public override string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public override float Balance
    {
        get => _balance;
        set => SetField(ref _balance, value);
    }

    [Ignore]
    public override Currency? Currency
    {
        get => _currency;
        set => SetField(ref _currency, value);
    }

    [Ignore]
    public override Group? Group
    {
        get => _group;
        set => SetField(ref _group, value);
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