using System.Windows.Input;
using Mobile.Extensions;

namespace Mobile.Views.Inputs;

/// <summary>
/// Represents a text input control that inherits from the InputLayout class.
/// </summary>
public partial class TextInput : InputLayout
{
    private bool _isVisible;

    /// <inheritdoc />
	public TextInput()
    {
        InitializeComponent();

        Element.SetVisualElementBinding();
    }

    #region Text

    /// <summary>
    /// Identifies the Text bindable property.
    /// </summary>
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(TextInput),
            string.Empty,
            BindingMode.TwoWay,
            propertyChanged: TextChanged);

    /// <summary>
    /// Gets or sets the text value of the input.
    /// </summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    #endregion

    #region Placeholder

    /// <summary>
    /// Identifies the Placeholder bindable property.
    /// </summary>
    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(TextInput),
            string.Empty,
            propertyChanged: PlaceholderChanged);

    /// <summary>
    /// Gets or sets the placeholder text of the input.
    /// </summary>
    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    #endregion

    #region Keyboard

    /// <summary>
    /// Identifies the Keyboard bindable property.
    /// </summary>
    public static readonly BindableProperty KeyboardProperty =
        BindableProperty.Create(
            nameof(Keyboard),
            typeof(Keyboard),
            typeof(TextInput),
            Keyboard.Plain,
            propertyChanged: KeyboardChanged);

    /// <summary>
    /// Gets or sets the keyboard type of the input.
    /// </summary>
    public Keyboard Keyboard
    {
        get => (Keyboard)GetValue(KeyboardProperty);
        set => SetValue(KeyboardProperty, value);
    }

    #endregion

    #region Return Type

    /// <summary>
    /// Identifies the ReturnType bindable property.
    /// </summary>
    public static readonly BindableProperty ReturnTypeProperty =
        BindableProperty.Create(
            nameof(ReturnType),
            typeof(ReturnType),
            typeof(TextInput),
            ReturnType.Done,
            propertyChanged: ReturnTypeChanged);

    /// <summary>
    /// Gets or sets the return type of the input.
    /// </summary>
    public ReturnType ReturnType
    {
        get => (ReturnType)GetValue(ReturnTypeProperty);
        set => SetValue(ReturnTypeProperty, value);
    }

    #endregion

    #region Return Command

    /// <summary>
    /// Identifies the ReturnCommand bindable property.
    /// </summary>
    public static readonly BindableProperty ReturnCommandProperty =
        BindableProperty.Create(
            nameof(ReturnCommand),
            typeof(ICommand),
            typeof(TextInput),
            propertyChanged: ReturnCommandChanged);

    /// <summary>
    /// Gets or sets the return command of the input.
    /// </summary>
    public ICommand? ReturnCommand
    {
        get => (ICommand?)GetValue(ReturnCommandProperty);
        set => SetValue(ReturnCommandProperty, value);
    }

    /// <summary>
    /// Identifies the IsPassword bindable property.
    /// </summary>
    public static readonly BindableProperty IsPasswordProperty =
        BindableProperty.Create(
            nameof(IsPassword),
            typeof(bool),
            typeof(TextInput),
            false,
            propertyChanged: IsPasswordChanged);

    /// <summary>
    /// Gets or sets a value indicating whether the input is for a password.
    /// </summary>
    public bool IsPassword
    {
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }

    #endregion

    #region Text Transform

    /// <summary>
    /// Identifies the TextTransform bindable property.
    /// </summary>
    public static readonly BindableProperty TextTransformProperty =
        BindableProperty.Create(
            nameof(TextTransform),
            typeof(TextTransform),
            typeof(TextInput),
            TextTransform.Default,
            propertyChanged: TextTransformChanged);

    /// <summary>
    /// Gets or sets the text transform of the input.
    /// </summary>
    public TextTransform TextTransform
    {
        get => (TextTransform)GetValue(TextTransformProperty);
        set => SetValue(TextTransformProperty, value);
    }

    #endregion

    public ICommand ChangePasswordVisibilityStatusCommand => new Command(() =>
    {
        _isVisible = !_isVisible;
        UpdateHiddenPasswordStatusView();
    });

    #region Changed Methods

    private static void TextChanged(BindableObject bindable, object? oldValue, object? newValue)
    {
        ((TextInput)bindable).UpdateTextView();
    }

    private static void PlaceholderChanged(BindableObject bindable, object? oldValue, object? newValue)
    {
        ((TextInput)bindable).UpdatePlaceholderView();
    }

    private static void KeyboardChanged(BindableObject bindable, object? oldValue, object? newValue)
    {
        ((TextInput)bindable).UpdateKeyboardView();
    }

    private static void ReturnTypeChanged(BindableObject bindable, object? oldValue, object? newValue)
    {
        ((TextInput)bindable).UpdateReturnTypeView();
    }

    private static void ReturnCommandChanged(BindableObject bindable, object? oldValue, object? newValue)
    {
        ((TextInput)bindable).UpdateReturnCommandView();
    }

    private static void TextTransformChanged(BindableObject bindable, object? oldValue, object? newValue)
    {
        ((TextInput)bindable).UpdateTextTransformView();
    }

    private static void IsPasswordChanged(BindableObject bindable, object? oldValue, object? newValue)
    {
        ((TextInput)bindable).UpdateIsPasswordView();
    }

    #endregion

    #region Private Methods

    private void UpdateTextView()
    {
        if (Keyboard == Keyboard.Numeric)
        {
            if (string.IsNullOrEmpty(Text))
            {
                Element.Text = Text;
                return;
            }

            Element.Text = int.TryParse(Text, out int number)
                ? $"{number}"
                : Text[..^1];
        }
        else
        {
            Element.Text = Text;
        }
    }

    private void UpdatePlaceholderView()
    {
        Element.Placeholder = Placeholder;
    }

    private void UpdateKeyboardView()
    {
        Element.Keyboard = Keyboard;
    }

    private void UpdateReturnTypeView()
    {
        Element.ReturnType = ReturnType;
    }

    private void UpdateReturnCommandView()
    {
        Element.ReturnCommand = ReturnCommand;
    }

    private void UpdateTextTransformView()
    {
        Element.TextTransform = TextTransform;
    }

    private void UpdateIsPasswordView()
    {
        UpdateHiddenPasswordStatusView();
    }

    private void UpdateHiddenPasswordStatusView()
    {
        if (!IsPassword)
        {
            return;
        }

        string status = _isVisible ? "Shown" : "Hidden";

        Element.IsPassword = !_isVisible;
        VisualStateManager.GoToState(This, status);
    }

    #endregion
}
