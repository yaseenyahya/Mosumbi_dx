using Mosumbi.Dx.Server.Abstractions;
using Mosumbi.Dx.Server.Filters;
using Mosumbi.Dx.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Mosumbi.Dx.Server.Areas.RemoteControl.Pages;

//[ServiceFilter(typeof(ViewerAuthorizationFilter))] yaseen for no auth access
public class ViewerModel : PageModel
{
    private readonly IViewerPageDataProvider _viewerDataProvider;

    public ViewerModel(IViewerPageDataProvider viewerDataProvider)
    {
        _viewerDataProvider = viewerDataProvider;
    }

    public string FaviconUrl { get; private set; } = string.Empty;
    public string LogoUrl { get; private set; } = string.Empty;
    public string PageDescription { get; private set; } = string.Empty;
    public string PageTitle { get; private set; } = string.Empty;
    public string ThemeUrl { get; private set; } = string.Empty;
    public string UserDisplayName { get; private set; } = string.Empty;

    public bool ShowConnect { get; private set; } = true;
    public async Task OnGet()
    {
        var theme = await _viewerDataProvider.GetTheme(this);

        ThemeUrl = theme switch
        {
            ViewerPageTheme.Dark => "/_content/Mosumbi.Dx.Server/css/remote-control-dark.css",
            ViewerPageTheme.Light => "/_content/Mosumbi.Dx.Server/css/remote-control-light.css",
            _ => "/_content/Mosumbi.Dx.Server/css/remote-control-dark.css"
        };
        UserDisplayName = await _viewerDataProvider.GetUserDisplayName(this);

        if(UserDisplayName == string.Empty)
        {
            if (HttpContext.Request.Query.TryGetValue("name", out var displayname))
            {
                UserDisplayName = displayname;
                if(UserDisplayName == string.Empty) 
                    ShowConnect = false;
            }
            else
                ShowConnect = false;
        }
        PageTitle = await _viewerDataProvider.GetPageTitle(this);
        PageDescription = await _viewerDataProvider.GetPageDescription(this);
        FaviconUrl = await _viewerDataProvider.GetFaviconUrl(this);
        LogoUrl = await _viewerDataProvider.GetLogoUrl(this);
    }
}
