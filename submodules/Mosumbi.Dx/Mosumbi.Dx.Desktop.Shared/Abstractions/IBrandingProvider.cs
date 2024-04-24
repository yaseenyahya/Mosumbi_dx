using Mosumbi.Dx.Shared.Models;

namespace Mosumbi.Dx.Desktop.Shared.Abstractions;

public interface IBrandingProvider
{
    BrandingInfoBase CurrentBranding { get; }
    Task Initialize();
    void SetBrandingInfo(BrandingInfoBase brandingInfo);
}
