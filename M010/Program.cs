using M000.Models;
using M010;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(o => o.OutputFormatters.Add(new CSVFormatter())).AddXmlSerializerFormatters();

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

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi("");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
