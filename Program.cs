using MudBlazor.Services;
using ReserGO.Utils.Event;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Refit;
using ReserGO.WebApp.Components;
using ReserGO.Service.Extensions;
using ReserGO.Service.Service.Authentication;
using ReserGO.ViewModel.Extensions;
using Serilog;
using ReserGO.Service.Interface.Utils;
using ReserGO.Service.Service.Utils;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);
var serverapi = builder.Configuration.GetValue<string>("serverapi");
MyConfigurationAccessor.Configuration = builder.Configuration;
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<IEvent, Event>();
builder.Services.AddSignalR();
builder.Services.AddMemoryCache();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddRefitClients(builder.Configuration);
builder.Services.AddReserGOServices();
builder.Services.AddScoped<JwtAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<JwtAuthenticationStateProvider>());
builder.Services.AddReserGOViewModels();
builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient()
{ BaseAddress = new Uri(serverapi) });

builder.Host.UseSerilog((hostingContext, services, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .ReadFrom.Services(services);
}, writeToProviders: false, preserveStaticLogger: false);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.Configure<CircuitOptions>(options =>
{
    options.DetailedErrors = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapHub<NotificationHub>("/notificationHub");

app.Run();
