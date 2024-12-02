using CommunityToolkit.Maui;
using LocalizationResourceManager.Maui;
using Mobile.Behaviors;
using Mobile.Features.Authentication.LogIn;
using Mobile.Features.Challenges;
using Mobile.Resources.Strings;

namespace Mobile.Extensions;

internal static class MauiAppBuilderExtensions
{
    internal static MauiAppBuilder UseMauiExtension(this MauiAppBuilder builder)
    {
        builder.AddResources();

        builder.Services
            .AddPages()
            .AddServices();

        return builder;
    }

    private static IServiceCollection AddPages(this IServiceCollection services)
    {
        services
            .AddTransientWithShellRoute<LogInPage, LogInPageViewModel>(AppRoutes.LogIn)
            .AddTransientWithShellRoute<ChallengesPage, ChallengesPageViewModel>(AppRoutes.Challenges);

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
            .AddMediatR(options =>
            {
                _ = options
                    .RegisterServicesFromAssembly(typeof(MauiAppBuilderExtensions).Assembly)
                    .AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));
            });
    }
}
