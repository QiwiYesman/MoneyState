using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MoneyState.Model.Entities;
using ReactiveUI;
using SQLite;

namespace MoneyState.ViewModel.ObservableEntities;

[Table("Group")]
public class ObservableGroup: Group, INotifyPropertyChanged
{
    private string _name = "";
    public override string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }
    
    [Ignore]
    public override Collection<Account> Accounts { get; set; } = new ObservableCollection<Account>();

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