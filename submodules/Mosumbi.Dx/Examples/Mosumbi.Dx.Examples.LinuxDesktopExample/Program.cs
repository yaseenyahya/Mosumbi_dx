using Mosumbi.Dx.Desktop.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mosumbi.Dx.Desktop.Shared.Startup;
using Mosumbi.Dx.Desktop.Shared.Services;
using Mosumbi.Dx.Desktop.Linux.Startup;
using Avalonia;
using Mosumbi.Dx.Desktop.UI;
using Mosumbi.Dx.Desktop.UI.Services;

namespace Mosumbi.Dx.Examples.LinuxDesktopExample;

public class Program
{
    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();

    public static async Task Main(string[] args)
    {

        var services = new ServiceCollection();
        services.AddRemoteControlLinux(config =>
        {
            config.AddBrandingProvider<BrandingProvider>();
        });

        services.AddLogging(builder =>
        {
            builder.SetMinimumLevel(LogLevel.Debug);
            // Add file logger, etc.
        });
        // Add other services.

        var provider = services.BuildServiceProvider();

        var result = await provider.UseRemoteControlClient(
            args,
            "The remote control client for Remotely.",
            serverUri: "https://localhost:7024");

        if (!result.IsSuccess)
        {
            Console.WriteLine($"Remote control failed with message: {result.Reason}");
        }

        var shutdownService = provider.GetRequiredService<IShutdownService>();
        Console.CancelKeyPress += async (s, e) =>
        {
            await shutdownService.Shutdown();
        };

        var appState = provider.GetRequiredService<IAppState>();
        Console.WriteLine("Unattended session ready at: ");
        Console.WriteLine($"https://localhost:7024/RemoteControl/Viewer?mode=Unattended&sessionId={appState.SessionId}&accessKey={appState.AccessKey}");

        Console.WriteLine("Press Ctrl + C to exit.");
        var dispatcher = provider.GetRequiredService<IUiDispatcher>();
        try
        {
            await Task.Delay(Timeout.InfiniteTimeSpan, dispatcher.ApplicationExitingToken);
        }
        catch (TaskCanceledException)
        {
            // Ok.
        }

    }
}