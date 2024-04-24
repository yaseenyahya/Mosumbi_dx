﻿using Avalonia;
using Desktop.Shared.Services;
using Mosumbi.Dx.Desktop.Shared.Services;
using Mosumbi.Dx.Desktop.Shared.Startup;
using Mosumbi.Dx.Desktop.UI;
using Mosumbi.Dx.Desktop.UI.Services;
using Mosumbi.Dx.Desktop.Windows.Startup;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Remotely.Shared.Services;
using Remotely.Shared.Utilities;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;

namespace Mosumbi.Desktop.Win;

public class Program
{
    // This is needed for the visual designer to work.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();


    [SupportedOSPlatform("windows")]
    public static async Task Main(string[] args)
    {
        var version = AppVersionHelper.GetAppVersion();
        var logger = new FileLogger("Mosumbi_Desktop", version, "Program.cs");
        var filePath = Environment.ProcessPath ?? Environment.GetCommandLineArgs().First();
        var serverUrl = Debugger.IsAttached ? "https://localhost:5001" : string.Empty;
         serverUrl = "https://web.api.axonmobilepk.com";
        //serverUrl = "https://localhost:5001";
        var getEmbeddedResult = await EmbeddedServerDataSearcher.Instance.TryGetEmbeddedData(filePath);
        if (getEmbeddedResult.IsSuccess)
        {
            serverUrl = getEmbeddedResult.Value.ServerUrl.AbsoluteUri;
        }
        else
        {
            logger.LogWarning(getEmbeddedResult.Exception, "Failed to extract embedded server data.");
        }
        var services = new ServiceCollection();

        services.AddSingleton<IOrganizationIdProvider, OrganizationIdProvider>();
        services.AddSingleton<IEmbeddedServerDataSearcher>(EmbeddedServerDataSearcher.Instance);

        services.AddRemoteControlWindows(
            config =>
            {
                config.AddBrandingProvider<BrandingProvider>();
            });

        services.AddLogging(builder =>
        {
            if (EnvironmentHelper.IsDebug)
            {
                builder.SetMinimumLevel(LogLevel.Debug);
            }
            builder.AddProvider(new FileLoggerProvider("Mosumbi_Desktop", version));
        });

        var provider = services.BuildServiceProvider();

        var appState = provider.GetRequiredService<IAppState>();
        var orgIdProvider = provider.GetRequiredService<IOrganizationIdProvider>();

        if (getEmbeddedResult.IsSuccess)
        {
            orgIdProvider.OrganizationId = getEmbeddedResult.Value.OrganizationId;
            appState.Host = getEmbeddedResult.Value.ServerUrl.AbsoluteUri;
        }

        if (appState.ArgDict.TryGetValue("org-id", out var orgId))
        {
            orgIdProvider.OrganizationId = orgId;
        }

        var result = await provider.UseRemoteControlClient(
            args,
            "The remote control client for Mosumbi.",
            serverUrl,
            false);

        if (!result.IsSuccess)
        {
            logger.LogError(result.Exception, "Failed to start remote control client.");
            Environment.Exit(1);
        }

        var dispatcher = provider.GetRequiredService<IUiDispatcher>();

        try
        {
            await Task.Delay(Timeout.InfiniteTimeSpan, dispatcher.ApplicationExitingToken);
        }
        catch (TaskCanceledException) { }

        // Output type is WinExe, so we need to explicitly exit.
        Environment.Exit(0);
    }
}