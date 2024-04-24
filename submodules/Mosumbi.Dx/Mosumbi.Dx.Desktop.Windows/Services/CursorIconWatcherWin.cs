﻿using Mosumbi.Dx.Desktop.Shared.Abstractions;
using Mosumbi.Dx.Desktop.Shared.Native.Windows;
using Mosumbi.Dx.Shared.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Timers;

namespace Mosumbi.Dx.Desktop.Windows.Services;

/// <summary>
/// A class that can be used to watch for cursor icon changes.
/// </summary>
[SupportedOSPlatform("windows")]
public class CursorIconWatcherWin : ICursorIconWatcher
{
    private const int IBeamHandle = 65541;

    private readonly System.Timers.Timer _changeTimer;

    private User32.CursorInfo _cursorInfo;

    private int _previousCursorHandle;

    public CursorIconWatcherWin()
    {
        _changeTimer = new System.Timers.Timer(25);
        _changeTimer.Elapsed += ChangeTimer_Elapsed;
        _changeTimer.Start();
    }

    public event EventHandler<CursorInfo>? OnChange;

    public CursorInfo GetCurrentCursor()
    {
        try
        {
            var ci = new User32.CursorInfo();
            ci.cbSize = Marshal.SizeOf(ci);
            User32.GetCursorInfo(out ci);
            if (ci.flags == User32.CURSOR_SHOWING)
            {
                if (ci.hCursor.ToInt32() == IBeamHandle)
                {
                    return new CursorInfo(Array.Empty<byte>(), Point.Empty, "text");
                }

                var hotspot = Point.Empty;

                if (User32.GetIconInfo(ci.hCursor, out var iconInfo))
                {
                    hotspot = new Point(iconInfo.xHotspot, iconInfo.yHotspot);
                }

                using var icon = Icon.FromHandle(ci.hCursor);
                using var ms = new MemoryStream();
                icon.ToBitmap().Save(ms, ImageFormat.Png);
                return new CursorInfo(ms.ToArray(), hotspot);
            }
            else
            {
                return new CursorInfo(Array.Empty<byte>(), Point.Empty, "default");
            }
        }
        catch
        {
            return new CursorInfo(Array.Empty<byte>(), Point.Empty, "default");
        }
    }

    private void ChangeTimer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        if (OnChange == null)
        {
            return;
        }
        try
        {
            _cursorInfo = new User32.CursorInfo();
            _cursorInfo.cbSize = Marshal.SizeOf(_cursorInfo);
            User32.GetCursorInfo(out _cursorInfo);
            if (_cursorInfo.flags == User32.CURSOR_SHOWING)
            {
                var currentCursor = _cursorInfo.hCursor.ToInt32();
                if (currentCursor != _previousCursorHandle)
                {
                    if (currentCursor == IBeamHandle)
                    {
                        OnChange?.Invoke(this, new CursorInfo(Array.Empty<byte>(), Point.Empty, "text"));
                    }
                    else
                    {
                        using var icon = Icon.FromHandle(_cursorInfo.hCursor);
                        using var ms = new MemoryStream();
                        var hotspot = Point.Empty;

                        if (User32.GetIconInfo(_cursorInfo.hCursor, out var iconInfo))
                        {
                            hotspot = new Point(iconInfo.xHotspot, iconInfo.yHotspot);
                        }
                        icon.ToBitmap().Save(ms, ImageFormat.Png);
                        OnChange?.Invoke(this, new CursorInfo(ms.ToArray(), hotspot));
                    }
                    _previousCursorHandle = currentCursor;
                }
            }
            else if (_previousCursorHandle != 0)
            {
                _previousCursorHandle = 0;
                OnChange?.Invoke(this, new CursorInfo(Array.Empty<byte>(), Point.Empty, "default"));
            }
        }
        catch
        {
            OnChange?.Invoke(this, new CursorInfo(Array.Empty<byte>(), Point.Empty, "default"));
        }
    }

}
