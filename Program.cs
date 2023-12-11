using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Iskolaryo.Auth;
using Iskolaryo.Components;
using Iskolaryo.Database;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddBlazorise(
        options =>
        {
            options.Immediate = true;
        }
    )
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

builder.Services.AddSignalR(builder => builder.MaximumReceiveMessageSize = 1000000000);

builder.Services.AddServerSideBlazor();

builder.Services.AddAuthenticationCore();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<UserAccountService>();
builder.Services.AddSingleton<DatabaseAccess>();
builder.Services.AddSingleton<MemberDatabaseAccess>();
builder.Services.AddSingleton<LoggedUserSingleton>();
builder.Services.AddCascadingAuthenticationState();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();