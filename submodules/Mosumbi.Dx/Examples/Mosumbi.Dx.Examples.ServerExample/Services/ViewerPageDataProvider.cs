using Mosumbi.Dx.Server.Abstractions;
using Mosumbi.Dx.Server.Areas.RemoteControl.Pages;
using Mosumbi.Dx.Server.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Mosumbi.Dx.Examples.ServerExample.Services;

internal class ViewerPageDataProvider : IViewerPageDataProvider
{
    public Task<string> GetFaviconUrl(PageModel pageModel)
    {
        return Task.FromResult("/favicon.ico");
    }

    public Task<string> GetLogoUrl(PageModel pageModel)
    {
        return Task.FromResult("/viewer-logo.svg");
    }

    public Task<string> GetPageDescription(PageModel pageModel)
    {
        return Task.FromResult("");
    }

    public Task<string> GetPageTitle(PageModel pageModel)
    {
        return Task.FromResult("Mosumbi Remote Control");
    }

    public Task<string> GetProductName(PageModel pageModel)
    {
        return Task.FromResult("Mosumbi");
    }

    public Task<string> GetProductSubtitle(PageModel pageModel)
    {
        return Task.FromResult("Remote Control");
    }

    public Task<ViewerPageTheme> GetTheme(PageModel pageModel)
    {
        return Task.FromResult(ViewerPageTheme.Dark);
    }

    public Task<string> GetUserDisplayName(PageModel pageModel)
    {
        return Task.FromResult("Han Solo");
    }
}
