using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using MoneyState.Model.Entities;

namespace MoneyState.View.ViewControls;

public class AccountLogView : TemplatedControl
{
    public static readonly StyledProperty<AccountLog> AccountLogProperty = AvaloniaProperty.Register<AccountLogView, AccountLog>(
        "AccountLog");

    public AccountLog AccountLog
    {
        get => GetValue(AccountLogProperty);
        set => SetValue(AccountLogProperty, value);
    }
}