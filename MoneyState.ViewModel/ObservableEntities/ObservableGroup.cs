using System.Collections.ObjectModel;
using MoneyState.Model.Entities;
using ReactiveUI;
using SQLite;

namespace MoneyState.ViewModel.ObservableEntities;

[Table("Group")]
public class ObservableGroup: ReactiveObject, IGroup
{
   
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    private string _name = "";
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    [Ignore]
    public Collection<IAccount> Accounts { get; set; } = new ObservableCollection<IAccount>();
}