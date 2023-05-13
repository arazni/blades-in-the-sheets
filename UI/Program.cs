using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Persistence.Json;
using Persistence.Json.Migrations;
using UI;
using UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

AddCloudRunEnvironment(builder.Services);
builder.Services.AddTransient<IStorageService, StorageService>();
builder.Services.AddTransient<ISerializer, Serializer>();
builder.Services.AddTransient<ILoader, Loader>();
builder.Services.AddTransient<IFileReader, HttpFileReader>();
builder.Services.AddTransient<ICharacterCoordinator, CharacterCoordinator>();
builder.Services.AddTransient<IMigrationHandler, MigrationHandler>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<SheetJank>();
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();

static void AddCloudRunEnvironment(IServiceCollection services)
{
	string service;
	try
	{
		service = Environment.GetEnvironmentVariable("K_SERVICE") ?? "???";
	}
	catch (ArgumentNullException)
	{
		service = "???";
	}

	// Cloud Run Revision
	string revision;
	try
	{
		revision = Environment.GetEnvironmentVariable("K_REVISION") ?? "???";
	}
	catch (ArgumentNullException)
	{
		revision = "???";
	}

	var envInfo = new EnvironmentInfo(service, revision);
	services.AddSingleton<IEnvironmentInfo>(envInfo);
}
