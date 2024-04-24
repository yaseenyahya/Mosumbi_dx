using Mosumbi.Dx.Desktop.Shared;
using Mosumbi.Dx.Desktop.Shared.Abstractions;
using Mosumbi.Dx.Desktop.UI.Controls.Dialogs;
using Microsoft.Extensions.Logging;

namespace Mosumbi.Dx.Desktop.UI.ViewModels;

public interface ISessionIndicatorWindowViewModel : IBrandedViewModelBase
{
    Task PromptForExit();
}
public class SessionIndicatorWindowViewModel : BrandedViewModelBase, ISessionIndicatorWindowViewModel
{
    private readonly IShutdownService _shutdownService;
    private readonly IDialogProvider _dialogProvider;

    public SessionIndicatorWindowViewModel(
        IBrandingProvider brandingProvider,
        IUiDispatcher dispatcher,
        IShutdownService shutdownService,
        IDialogProvider dialogProvider,
        ILogger<BrandedViewModelBase> logger)
        : base(brandingProvider, dispatcher, logger)
    {
        _shutdownService = shutdownService;
        _dialogProvider = dialogProvider;
    }

    public async Task PromptForExit()
    {
        var result = await _dialogProvider.Show("Stop the remote control session?", "Stop Session", MessageBoxType.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            await _shutdownService.Shutdown();
        }
    }
}
