var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

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
app.MapFallbackToFile("index.html");

app.Run(CloudRunUrl());

static string CloudRunUrl()
{
	string port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

	return $"http://0.0.0.0:{port}";
}
