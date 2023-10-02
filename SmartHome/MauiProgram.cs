using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DataAccess;
using SmartApp.MVVM.Views;
using SmartHome.MVVM.Pages;
using SmartHome.MVVM.ViewModels;
using SmartHome.Services;

namespace SmartHome
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Rubik-Regular.ttf", "RubikRegular");
                    fonts.AddFont("fa-thin-100.ttf", "FontAwesomeThin");
                    fonts.AddFont("fa-light-300.ttf", "FontAwesomeLight");
                    fonts.AddFont("fa-regular-400.ttf", "FontAwesomeRegular");
                    fonts.AddFont("fa-solid-900.ttf", "FontAwesomeSolid");
                });

            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<SettingsViewModel>();
            builder.Services.AddSingleton<SettingsPage>();
            builder.Services.AddSingleton<AllDevicesViewModel>();
            builder.Services.AddSingleton<AllDevicesPage>();
            builder.Services.AddSingleton<GetStartedViewModel>();
            //builder.Services.AddSingleton<GetStartedPage>();

            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<DeviceManager>();
            builder.Services.AddDbContext<DataContext>(x => x.UseSqlite($"Data Source={DataBasePathFinder.GetPath()}"));


            return builder.Build();
        }
    }
}