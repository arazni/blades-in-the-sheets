using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using Persistence.Json;
using Persistence.Json.Migrations;
using UI;
using UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<IStorageService, StorageService>();
builder.Services.AddTransient<ISerializer, Serializer>();
builder.Services.AddTransient<ILoader, Loader>();
builder.Services.AddTransient<IFileReader, HttpFileReader>();
builder.Services.AddTransient<ICharacterCoordinator, CharacterCoordinator>();
builder.Services.AddTransient<IMigrationHandler, MigrationHandler>();
builder.Services.AddTransient<IThemeStorageService, ThemeStorageService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IThemeSettingService, ThemeSettingService>();

builder.Services.AddSingleton<SheetJank>();
builder.Services.AddSingleton<CreationJank>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddFluentUIComponents();

await builder.Build().RunAsync();