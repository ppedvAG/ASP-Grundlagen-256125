using M000.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string? connString = builder.Configuration.GetConnectionString("KursDB");
if (connString != null)
{
	//Options
	//Können bei vielen Add-Methoden gesetzt werden, um die DI zu konfigurieren
	builder.Services.AddDbContext<KursDBContext>(o =>
	{
		o.UseSqlServer(connString);
		if (builder.Environment.IsDevelopment())
		{
			o.EnableDetailedErrors();
			o.EnableSensitiveDataLogging();
		}

		//Kurzform:
		//o.UseSqlServer(connString)
		//	.EnableDetailedErrors()
		//	.EnableSensitiveDataLogging();
	});
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}")
	.WithStaticAssets();


app.Run();
