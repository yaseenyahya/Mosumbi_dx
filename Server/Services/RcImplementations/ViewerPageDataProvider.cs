﻿using Mosumbi.Dx.Server.Abstractions;
using Mosumbi.Dx.Server.Areas.RemoteControl.Pages;
using Mosumbi.Dx.Server.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Org.BouncyCastle.Ocsp;
using Remotely.Shared.Enums;
using Remotely.Shared.Models;
using System;
using System.Threading.Tasks;

namespace Remotely.Server.Services.RcImplementations;

public class ViewerPageDataProvider : IViewerPageDataProvider
{
    private readonly IDataService _dataService;
    public ViewerPageDataProvider(IDataService dataService)
    {
        _dataService = dataService;
    }

    public Task<string> GetFaviconUrl(PageModel viewerModel)
    {
        return Task.FromResult("/_content/Mosumbi.Dx.Server/favicon.ico");
    }

    public async Task<string> GetLogoUrl(PageModel viewerModel)
    {
        return await GetTheme(viewerModel) == ViewerPageTheme.Dark ?
           "/images/viewer/logo.png" :
           "/images/viewer/logo.png";
    }

    public Task<string> GetPageDescription(PageModel viewerModel)
    {
        return Task.FromResult("");
    }

    public Task<string> GetPageTitle(PageModel pageModel)
    {
        return Task.FromResult("Mosumbi Remote Control");
    }

    public Task<ViewerPageTheme> GetTheme(PageModel pageModel)
    {
        // TODO: Implement light theme in new viewer design.
        return Task.FromResult(ViewerPageTheme.Dark);
        //if (pageModel.User.Identity.IsAuthenticated)
        //{
        //    var user = _dataService.GetUserByNameWithOrg(pageModel.User.Identity.Name);

        //    var userTheme = user.UserOptions.Theme switch
        //    {
        //        Theme.Light => ViewerPageTheme.Light,
        //        Theme.Dark => ViewerPageTheme.Dark,
        //        _ => ViewerPageTheme.Dark
        //    };
        //    return Task.FromResult(userTheme);
        //}

        //var appTheme = _appConfig.Theme switch
        //{
        //    Theme.Light => ViewerPageTheme.Light,
        //    Theme.Dark => ViewerPageTheme.Dark,
        //    _ => ViewerPageTheme.Dark
        //};
        //return Task.FromResult(appTheme);
    }

    public async Task<string> GetUserDisplayName(PageModel pageModel)
    {
        if (string.IsNullOrWhiteSpace(pageModel?.User?.Identity?.Name))
        {
            return string.Empty;
        }

        var userResult = await _dataService.GetUserByName(pageModel.User.Identity.Name);

        if (!userResult.IsSuccess)
        {
            return string.Empty;
        }

        var user = userResult.Value;
        var displayName = user.UserOptions?.DisplayName ?? user.UserName ?? string.Empty;
        return displayName;
    }
}
