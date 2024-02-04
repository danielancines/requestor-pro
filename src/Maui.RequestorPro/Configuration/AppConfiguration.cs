using Maui.RequestorPro.ViewModels;
using Maui.RequestorPro.ViewModels.Base;

namespace Maui.RequestorPro.Configuration;

internal static class AppConfiguration
{
    #region Public Methods

    public static MauiAppBuilder ConfigApp(this MauiAppBuilder appBuilder)
    {
        ConfigureViews(appBuilder);
        ConfigureViewModels(appBuilder);

        appBuilder.Services.AddHttpClient();

        return appBuilder;
    }

    #endregion

    #region Private Methods
    private static void ConfigureViews(MauiAppBuilder appBuilder)
    {
        appBuilder.Services.AddTransient<MainPage>();
    }

    private static void ConfigureViewModels(MauiAppBuilder appBuilder)
    {
        appBuilder.Services.AddScoped<IMainPageViewModel, MainPageViewModel>();
    }

    #endregion
}
