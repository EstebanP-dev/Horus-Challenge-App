#pragma warning disable S2743

using System.Windows.Input;
using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Mobile.Pages;

/// <summary>
/// Represents a page with rich content that initializes a command when it appears.
/// </summary>
public abstract class RichContentPage : ContentPage
{
    private bool _isInitialized;

    /// <summary>
    /// Initializes a new instance of the <see cref="RichContentPage"/> class and registers the route for the child type.
    /// </summary>
    protected RichContentPage()
    {
        AddBehaviors();

        HideSoftInputOnTapped = true;
    }

    #region OnInitialize

    /// <summary>
    /// Identifies the OnInitializedCommand bindable property.
    /// </summary>

    public static readonly BindableProperty OnInitializedCommandProperty = BindableProperty.Create(
        nameof(OnInitializedCommand),
        typeof(ICommand),
        typeof(RichContentPage));

    /// <summary>
    /// Gets or sets the command that is executed when the page is initialized.
    /// </summary>
    public ICommand? OnInitializedCommand
    {
        get => (ICommand?)GetValue(OnInitializedCommandProperty);
        set => SetValue(OnInitializedCommandProperty, value);
    }

    #endregion

    #region OnNavigatedFrom

    /// <summary>
    /// Identifies the OnNavigatedFromCommand bindable property.
    /// </summary>
    public static readonly BindableProperty OnNavigatedFromCommandProperty = BindableProperty.Create(
        nameof(OnNavigatedFromCommand),
        typeof(ICommand),
        typeof(RichContentPage));

    /// <summary>
    /// Gets or sets the command that is executed when the page is initialized.
    /// </summary>
    public ICommand? OnNavigatedFromCommand
    {
        get => (ICommand?)GetValue(OnNavigatedFromCommandProperty);
        init => SetValue(OnNavigatedFromCommandProperty, value);
    }

    #endregion

    #region Overrides

    /// <summary>
    /// Called when the page appears. Executes the OnInitializedCommand if it has not been initialized before.
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (_isInitialized)
        {
            return;
        }

        _isInitialized = true;
        _ = Dispatcher.Dispatch(() =>
        {
            OnInitializedCommand?.Execute(null);
        });
    }

    /// <summary>
    /// Called when the page is navigated from. Executes the OnNavigatedFromCommand.
    /// </summary>
    /// <param name="args">The event data for the navigation event.</param>
    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);

        _ = Dispatcher.Dispatch(() =>
        {
            OnNavigatedFromCommand?.Execute(null);
        });
    }

    #endregion

    private void AddBehaviors()
    {
#pragma warning disable CA1416
        Behaviors.Add(new StatusBarBehavior
        {
            StatusBarColor = Colors.Transparent,
            StatusBarStyle = StatusBarStyle.DarkContent
        });
#pragma warning restore CA1416
    }
}
