using System;
using System.Collections.Generic;

namespace MoneyStateCross.ViewModels;

public class OperationManagerViewModel : PageModelBase
{
    public Action? OnUpdateGroups;
    public Dictionary<string, Action> Operations { get; set; } = new()
    {
        [""] = () => {},
        
    };

    public void OpenOperationPage(string pageName)
    {
        if(!Operations.TryGetValue(pageName, out var method)) return;
        method();
    }
    
    public void OpenAccountsGroupsChange()
    {
        var page = new AccountsGroupChangeViewModel()
        {
            PrevPage = this
        };
        page.OnUpdate = OnUpdateGroups;
        
        NextPage = page;
        LoadNextPage();
    }
}