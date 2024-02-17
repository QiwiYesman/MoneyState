using System;
using System.Threading.Tasks;
using MoneyStateCross.Models;
using MoneyStateCross.Models.Entities;

namespace MoneyStateCross.ViewModels;

public class AccountEditableBase : PageModelBase
{
    public ObservableAccount Account { get; set; }

    public Task<Currency[]> Currencies => MainViewModel.Currencies;
    public Task<Group[]> Groups => MainViewModel.Groups;
    
    public void Back()
    {
        LoadPrevPage();
    }
}