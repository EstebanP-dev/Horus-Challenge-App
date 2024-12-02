using Android.App;
using Android.Runtime;

#pragma warning disable IDE0130
// ReSharper disable once CheckNamespace
namespace Mobile;
#pragma warning restore IDE0130

/// <inheritdoc />
[Application]
public class MainApplication : MauiApplication
{
    /// <inheritdoc />
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    /// <inheritdoc />
    protected override MauiApp CreateMauiApp()
    {
        return MauiProgram.CreateMauiApp();
    }
}
