using UIKit;

// ReSharper disable once CheckNamespace
namespace Mobile;

/// <summary>
/// Represents the main program class for the iOS application.
/// </summary>
public class Program
{
    /// <summary>
    /// The main entry point of the application.
    /// </summary>
    /// <param name="args">An array of command-line arguments.</param>
    private static void Main(string[] args)
    {
        // if you want to use a different Application Delegate class from "AppDelegate"
        // you can specify it here.
        UIApplication.Main(args, null, typeof(AppDelegate));
    }
}
