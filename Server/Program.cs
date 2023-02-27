using Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
AddCloudRunEnvironment(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run(CloudRunUrl());

static string CloudRunUrl()
{
	string port;
	try
	{
		port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
	}
	catch (ArgumentNullException)
	{
		port = "8080";
	}

	return $"http://0.0.0.0:{port}";
}

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
