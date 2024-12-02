using FluentValidation;
using Mobile.Resources.Strings;

namespace Mobile.Features.Authentication.LogIn;

/// <summary>
/// Validator for the <see cref="LogInPageViewModel"/> class.
/// </summary>
internal sealed class LogInPageViewModelValidator : AbstractValidator<LogInPageViewModel>
{
    /// <summary>
    /// The name of the Email property.
    /// </summary>
    public const string EmailPropertyName = nameof(LogInPageViewModel.Email);

    /// <summary>
    /// The name of the Password property.
    /// </summary>
    public const string PasswordPropertyName = nameof(LogInPageViewModel.Password);

    /// <summary>
    /// Initializes a new instance of the <see cref="LogInPageViewModelValidator"/> class.
    /// </summary>
    /// <param name="resources">The localization resource manager.</param>
    public LogInPageViewModelValidator(ILocalizationResourceManager resources)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(resources[AppStringKeys.LogInFormEmailValidationRequired])
            .EmailAddress()
            .WithMessage(resources[AppStringKeys.LogInFormEmailValidationInvalidFormat]);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(resources[AppStringKeys.LogInFormPasswordValidationRequired]);
    }
}
