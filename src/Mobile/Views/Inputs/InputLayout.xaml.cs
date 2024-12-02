using System.Windows.Input;
using Mobile.Helpers;

namespace Mobile.Views.Inputs;

/// <summary>
/// Represents a custom input layout that inherits from the Grid class.
/// </summary>
public partial class InputLayout : Grid
{
    #region View

    /// <summary>
    /// Identifies the View bindable property.
    /// </summary>
    public static readonly BindableProperty ViewProperty =
        BindableProperty.Create(
            nameof(View),
            typeof(View),
            typeof(InputLayout),
             null,
            BindingMode.OneWay,
            ViewHelper.ValidateCustomView,
            ElementChanged);

    /// <summary>
    /// Gets or sets the custom view for the input layout.
    /// </summary>
    public View View
    {
        get => (View)GetValue(ViewProperty);
        set => SetValue(ViewProperty, value);
    }

    #endregion

    #region Hint

    /// <summary>
    /// Identifies the Hint bindable property.
    /// </summary>
    public static readonly BindableProperty HintProperty =
        BindableProperty.Create(
            nameof(Hint),
            typeof(string),
            typeof(InputLayout),
            defaultValue: string.Empty,
            BindingMode.OneWay,
            null,
            HintChanged);

    /// <summary>
    /// Gets or sets the hint text for the input layout.
    /// </summary>
    public string Hint
    {
        get => (string)GetValue(HintProperty);
        set => SetValue(HintProperty, value);
    }

    /// <summary>
    /// Identifies the Error bindable property.
    /// </summary>
    public static readonly BindableProperty ErrorProperty =
        BindableProperty.Create(
            nameof(Error),
            typeof(string),
            typeof(InputLayout),
            string.Empty,
            propertyChanged: ErrorChanged);

    /// <summary>
    /// Gets or sets the error message for the input layout.
    /// </summary>
    public string Error
    {
        get => (string)GetValue(ErrorProperty);
        set => SetValue(ErrorProperty, value);
    }

    #endregion

    #region Action Icon Source

    public static readonly BindableProperty ActionIconSourceProperty =
        BindableProperty.Create(
            nameof(ActionIconSource),
            typeof(ImageSource),
            typeof(InputLayout),
            propertyChanged: ActionIconSourceChanged);

    public ImageSource? ActionIconSource
    {
        get => (ImageSource?)GetValue(ActionIconSourceProperty);
        set => SetValue(ActionIconSourceProperty, value);
    }

    #endregion

    #region Action Icon Command

    public static readonly BindableProperty ActionIconCommandProperty =
        BindableProperty.Create(
            nameof(ActionIconCommand),
            typeof(ICommand),
            typeof(InputLayout),
            propertyChanged: ActionIconCommandChanged);

    public ICommand? ActionIconCommand
    {
        get => (ICommand?)GetValue(ActionIconCommandProperty);
        init => SetValue(ActionIconCommandProperty, value);
    }

    #endregion

    /// <inheritdoc />
    public InputLayout()
    {
        InitializeComponent();
    }

    #region Changed Events

    private static void ElementChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((InputLayout)bindable).UpdateElementView();
    }

    private static void HintChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((InputLayout)bindable).UpdateHintView();
    }

    private static void ErrorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((InputLayout)bindable).UpdateErrorView();
    }

    private static void ActionIconSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((InputLayout)bindable).UpdateActionIconSourceView();
    }

    private static void ActionIconCommandChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((InputLayout)bindable).UpdateActionIconCommandView();
    }

    #endregion

    #region Private Methods

    private void UpdateElementView()
    {
        BorderInput.Content = View;
        UpdateIsRequiredView();
    }

    private void UpdateIsRequiredView()
    {
        HintLabel.Text = "*" + HintLabel.Text;
    }

    private void UpdateHintView()
    {
        HintLabel.Text = Hint;
    }

    private void UpdateErrorView()
    {
        bool hasError = !string.IsNullOrEmpty(Error);
        string state = hasError ? "Error" : "Default";

        ErrorLabel.Text = Error;
        ErrorLabel.IsVisible = hasError;

        _ = VisualStateManager.GoToState(HintLabel, state);
        _ = VisualStateManager.GoToState(BorderInput, state);
    }

    private void UpdateActionIconSourceView()
    {
        ActionIconButton.IsVisible = ActionIconSource is not null;
        ActionIconButton.Source = ActionIconSource;
    }

    private void UpdateActionIconCommandView()
    {
        ActionIconButton.Command = ActionIconCommand;
    }

    #endregion
}
