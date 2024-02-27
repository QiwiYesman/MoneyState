using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using MoneyState.ViewModel.ObservableEntities;

namespace MoneyState.View.ViewControls;

public class AccountView : TemplatedControl
{
    public static readonly StyledProperty<ObservableAccount> AccountProperty = AvaloniaProperty.Register<AccountView, ObservableAccount>(
        "Account");

    public ObservableAccount Account
    {
        get => GetValue(AccountProperty);
        set => SetValue(AccountProperty, value);
    }
}