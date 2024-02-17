using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace MoneyStateCross.Views;

public class AccountTemplate : TemplatedControl
{
    public static readonly StyledProperty<ICommand?> CommandProperty =
        Button.CommandProperty.AddOwner<AccountTemplate>();

    public ICommand? Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
}