using Mosumbi.Dx.Server.Models;
using Mosumbi.Dx.Shared.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosumbi.Dx.Server.Abstractions;

/// <summary>
/// Provides options related to how the viewer front-end should behave.
/// </summary>
public interface IViewerOptionsProvider
{
    Task<RemoteControlViewerOptions> GetViewerOptions();
}
