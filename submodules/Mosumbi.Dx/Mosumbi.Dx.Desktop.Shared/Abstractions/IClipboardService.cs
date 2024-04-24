﻿namespace Mosumbi.Dx.Desktop.Shared.Abstractions;

public interface IClipboardService
{
    event EventHandler<string> ClipboardTextChanged;

    void BeginWatching();

    Task SetText(string clipboardText);
}
