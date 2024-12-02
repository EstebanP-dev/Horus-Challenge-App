using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

// ReSharper disable once CheckNamespace
namespace Mobile;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        UserDialogs.Init(this);

        Window?.SetSoftInputMode(SoftInput.AdjustResize);
    }

}
