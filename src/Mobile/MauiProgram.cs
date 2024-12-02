#if ANDROID
using Android.Content.Res;
#elif IOS
using UIKit;
#endif

using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Mobile.Extensions;

namespace Mobile;


/// <summary>
/// The MauiProgram class is responsible for creating and configuring the .NET MAUI application.
/// </summary>
public static class MauiProgram
{
    /// <summary>
    /// Creates and configures the .NET MAUI application.
    /// </summary>
    /// <returns>A configured <see cref="MauiApp"/> instance.</returns>
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseUserDialogs()
            .UseMauiExtension()
            .ConfigureFonts(ConfigureFonts);

#if DEBUG
        builder.Logging.AddDebug();
#endif

        ConfigureHandlers();

        return builder.Build();
    }

    private static void ConfigureFonts(IFontCollection fonts)
    {
        _ = fonts
            .AddFont("OpenSans-Regular.ttf", "OpenSansRegular")
            .AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
    }

    private static void ConfigureHandlers()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#elif IOS
            handler.PlatformView.BackgroundColor = UIColor.Clear;
            handler.PlatformView.BorderStyle = UITextBorderStyle.None;
#endif
        });
    }
}
