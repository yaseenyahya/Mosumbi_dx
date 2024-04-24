using Mosumbi.Dx.Examples.ServerExample.Options;
using Mosumbi.Dx.Examples.ServerExample.Services;
using Mosumbi.Dx.Server.Abstractions;
using Mosumbi.Dx.Server.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();

builder.Services.AddRemoteControlServer(config =>
{
    config.AddHubEventHandler<HubEventHandler>();
    config.AddViewerAuthorizer<ViewerAuthorizer>();
    config.AddViewerPageDataProvider<ViewerPageDataProvider>();
    config.AddViewerOptionsProvider<ViewerOptionsProvider>();
    config.AddSessionRecordingSink<SessionRecordingSink>();
});

builder.Services.Configure<AppSettingsOptions>(
    builder.Configuration.GetSection(AppSettingsOptions.KeyName));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthorization();

app.UseRemoteControlServer();

//app.MapRazorPages();

app.Run();
