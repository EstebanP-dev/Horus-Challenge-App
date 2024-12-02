using CommunityToolkit.Maui;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Mobile.Behaviors;
using Mobile.Extensions.Http;
using Mobile.Extensions.Http.Handlers;
using Mobile.Features.Authentication;
using Mobile.Features.Authentication.LogIn;
using Mobile.Features.Challenges;
using Mobile.Options;
using Mobile.Resources.Strings;
using LogInPageViewModel = Mobile.Features.Authentication.LogIn.LogInPageViewModel;

namespace Mobile.Extensions;

internal static class MauiAppBuilderExtensions
{
    internal static MauiAppBuilder UseMauiExtension(this MauiAppBuilder builder)
    {
        builder
            .AddAppConfiguration()
            .AddResources();

        builder.Services
            .AddOptions(builder.Configuration)
            .AddClients()
            .AddPages()
            .AddServices();

        return builder;
    }

    private static IServiceCollection AddClients(this IServiceCollection services)
    {
        services.AddAppHttpClient<IAuthenticationClient, ApiSettings>((options, client) =>
        {
            client.BaseAddress = new Uri(options.BaseUrl);
        });

        services.AddAppHttpClient<IChallengesClient, ApiSettings>((options, client) =>
        {
            client.BaseAddress = new Uri(options.BaseUrl);
        });

        return services;
    }

    private static IServiceCollection AddPages(this IServiceCollection services)
    {
        services
            .AddTransientWithShellRoute<LogInPage, LogInPageViewModel>(AppRoutes.LogIn)
            .AddTransientWithShellRoute<ChallengesPage, ChallengesPageViewModel>(AppRoutes.Challenges);

        return services;
    }

    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddModelOptions<ApiSettings>(configuration);

        return services;
    }

    private static void AddResources(this MauiAppBuilder builder)
    {
        _ = builder.UseLocalizationResourceManager(options =>
        {
            _ = options.AddResource(AppImageStrings.ResourceManager);
            _ = options.AddResource(AppStrings.ResourceManager);
        });
    }

    private static void AddServices(this IServiceCollection services)
    {
        _ = services
            .AddScoped<ConnectionDelegatingHandler>()
            .AddScoped<RetryDelegatingHandler>()
            .AddScoped<HeaderDelegatingHandler>();

        _ = services
            .AddMediatR(options =>
            {
                _ = options
                    .RegisterServicesFromAssembly(typeof(MauiAppBuilderExtensions).Assembly)
                    .AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));
            });

        _ = services.AddValidatorsFromAssembly(typeof(MauiAppBuilderExtensions).Assembly);
    }
}
