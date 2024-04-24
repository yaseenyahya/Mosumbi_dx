using Mosumbi.Dx.Examples.ServerExample.Options;
using Mosumbi.Dx.Server.Abstractions;
using Mosumbi.Dx.Server.Models;
using Mosumbi.Dx.Shared.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace Mosumbi.Dx.Examples.ServerExample.Services;

public class ViewerOptionsProvider : IViewerOptionsProvider
{
    private readonly IOptionsMonitor<AppSettingsOptions> _appSettings;

    public ViewerOptionsProvider(IOptionsMonitor<AppSettingsOptions> appSettings)
    {
        _appSettings = appSettings;
    }

    public Task<RemoteControlViewerOptions> GetViewerOptions()
    {
        var options = new RemoteControlViewerOptions()
        {
            ShouldRecordSession = _appSettings.CurrentValue.ShouldRecordSessions
        };

        return Task.FromResult(options);
    }
}
