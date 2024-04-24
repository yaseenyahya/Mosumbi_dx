using Mosumbi.Dx.Desktop.Shared.Abstractions;
using Mosumbi.Dx.Shared.Models;

namespace Mosumbi.Dx.Examples.LinuxDesktopExample;

internal class BrandingProvider : IBrandingProvider
{
    private BrandingInfoBase _brandingInfo = new()
    {
        Product = "Immy Remote"
    };


    public BrandingInfoBase CurrentBranding => _brandingInfo;

    public Task<BrandingInfoBase> GetBrandingInfo()
    {
        return Task.FromResult(_brandingInfo);
    }

    public async Task Initialize()
    {
        using var mrs = typeof(BrandingProvider).Assembly.GetManifestResourceStream("Mosumbi.Dx.Examples.LinuxDesktopExample.ImmyBot.png");
        using var ms = new MemoryStream();
        await mrs!.CopyToAsync(ms);

        _brandingInfo.Icon = ms.ToArray();
    }

    public void SetBrandingInfo(BrandingInfoBase brandingInfo)
    {
        _brandingInfo = brandingInfo;
    }
}