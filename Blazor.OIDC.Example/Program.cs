using Blazor.OIDC;
using Blazor.OIDC.Example.Data;
using Blazor.OIDC.Services;
using Blazor.OIDC.State;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Get out ConfigurationManager setup
var _configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

// Register our OIDC provider and state manager
builder.Services.AddBlazorOidc(_configuration.GetSection(Constants.BlazorOidc));
builder.Services.AddSingleton<CachedState>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthState>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Enable to register our schemes and allow policies
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();