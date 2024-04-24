using Mosumbi.Dx.Desktop.Shared.Abstractions;
using Mosumbi.Dx.Shared.Models;
using System.Drawing;

namespace Mosumbi.Dx.Desktop.Linux.Services;

public class CursorIconWatcherLinux : ICursorIconWatcher
{
#pragma warning disable CS0067
    public event EventHandler<CursorInfo>? OnChange;
#pragma warning restore


    public CursorInfo GetCurrentCursor() => new(Array.Empty<byte>(), Point.Empty, "default");
}
