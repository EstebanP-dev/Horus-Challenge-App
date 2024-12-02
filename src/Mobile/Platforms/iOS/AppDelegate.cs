using Foundation;

// ReSharper disable once CheckNamespace
namespace Mobile;

/// <inheritdoc />
[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    /// <inheritdoc />
    protected override MauiApp CreateMauiApp()
    {
        return MauiProgram.CreateMauiApp();
    }
}
