using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using Persistence.Json;
using Persistence.Json.Migrations;
using Toolbelt.Blazor.Extensions.DependencyInjection;
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
builder.Services.AddTransient<IAccessibilityStorageService, AccessibilityStorageService>();
builder.Services.AddTransient<IHttpDemoReader, HttpDemoReader>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IThemeSettingService, ThemeSettingService>();
builder.Services.AddScoped<IAccessibilitySettingService, AccessibilitySettingService>();

builder.Services.AddSingleton<SheetJank>();
builder.Services.AddSingleton<CreationJank>();

var jankyErrorProvider = new InterceptErrorProvider();
builder.Logging.AddProvider(jankyErrorProvider);

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddFluentUIComponents();
builder.Services.AddHotKeys2();

var host = builder.Build();

// https://ben-5.azurewebsites.net/2024/4/17/display-unhandled-client-exceptions-with-blazor/
jankyErrorProvider.Intercept += async exception =>
{
	var jSRuntime = host.Services.GetRequiredService<IJSRuntime>();

	await InterceptErrorProvider.SetErrorMessage(jSRuntime, exception);
};

await host.RunAsync();

