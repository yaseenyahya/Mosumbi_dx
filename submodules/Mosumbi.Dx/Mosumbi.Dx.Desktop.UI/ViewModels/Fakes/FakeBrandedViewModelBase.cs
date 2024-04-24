﻿using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Mosumbi.Dx.Desktop.UI.Controls.Dialogs;
using Mosumbi.Dx.Shared.Models;
using System.Diagnostics;
using System.IO;

namespace Mosumbi.Dx.Desktop.UI.ViewModels.Fakes;

public class FakeBrandedViewModelBase : IBrandedViewModelBase
{
    private readonly BrandingInfoBase _brandingInfo;
    private Bitmap? _icon;

    public FakeBrandedViewModelBase()
    {
        _brandingInfo = new BrandingInfoBase();
        _icon = GetBitmapImageIcon(_brandingInfo);
    }
    public Bitmap? Icon
    {
        get => _icon;
        set => _icon = value;
    }
    public string ProductName { get; set; } = "Test Product";
    public WindowIcon? WindowIcon { get; set; }

    public Task ApplyBranding()
    {
        return Task.CompletedTask;
    }

    private Bitmap? GetBitmapImageIcon(BrandingInfoBase bi)
    {
        try
        {
            using var imageStream = typeof(Shared.Services.AppState)
                .Assembly
                .GetManifestResourceStream("Mosumbi.Dx.Desktop.Shared.Assets.DefaultIcon.png") ?? new MemoryStream();

            return new Bitmap(imageStream);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null;
        }
    }
}
