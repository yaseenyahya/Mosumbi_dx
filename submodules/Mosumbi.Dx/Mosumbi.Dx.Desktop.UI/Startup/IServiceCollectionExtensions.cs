
using Mosumbi.Dx.Desktop.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Mosumbi.Dx.Desktop.UI.Services;

namespace Mosumbi.Dx.Desktop.UI.Startup;

internal static class IServiceCollectionExtensions
{
    internal static void AddRemoteControlUi(
       this IServiceCollection services)
    {
        services.AddSingleton<IUiDispatcher, UiDispatcher>();
        services.AddSingleton<IChatUiService, ChatUiService>();
        services.AddSingleton<IClipboardService, ClipboardService>();
        services.AddSingleton<ISessionIndicator, SessionIndicator>();
        services.AddSingleton<IRemoteControlAccessService, RemoteControlAccessService>();
        services.AddSingleton<IViewModelFactory, ViewModelFactory>();
        services.AddSingleton<IMainWindowViewModel, MainWindowViewModel>();
        services.AddSingleton<IMainViewViewModel, MainViewViewModel>();
        services.AddSingleton<ISessionIndicatorWindowViewModel, SessionIndicatorWindowViewModel>();
        services.AddTransient<IMessageBoxViewModel, MessageBoxViewModel>();
        services.AddSingleton<IDialogProvider, DialogProvider>();
    }
}
