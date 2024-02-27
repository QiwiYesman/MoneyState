using Android.App;
using Android.Content.PM;
using Android.Views;
using Avalonia;
using Avalonia.Android;
using Avalonia.ReactiveUI;
using MoneyState.View;

namespace MoneyState.Cross.Android;

[Activity(
    Label = "MoneyState.Cross.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    WindowSoftInputMode = SoftInput.AdjustResize,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return base.CustomizeAppBuilder(builder)
            .WithInterFont()
            .UseReactiveUI();
    }
}