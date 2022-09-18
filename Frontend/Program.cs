using Blazored.LocalStorage;
using Frontend;
using Frontend.Providers;
using Frontend.Services;
using Frontend.Services.Auth;
using Frontend.Services.Base;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7101") });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ApiAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(p => p.GetRequiredService<ApiAuthStateProvider>());
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<IClient, Client>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGenerosService, GenerosService>();
builder.Services.AddScoped<IPeliculasService, PeliculasService>();
builder.Services.AddScoped<IPersonajesService, PersonajesService>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
