using M002;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Dependency Injection
//Services anmelden (hier oben), in den Konstruktoren der Controller/Pages/Components empfangen
//Können beliebige C#-Klassen sein
//Wird über die Add Methoden durchgeführt

//Singleton, Transient, Scoped
//Basismethoden; alle anderen Methoden bauen auf diesen Methoden auf
//Alternative: Keyed-Methoden (bei mehreren gleichen Objekten, die differenziert werden müssen)
builder.Services.AddSingleton<DITest>();

var app = builder.Build();

///////////////////////////////////////////////////////////////

//Middleware
//Wenn der User einen Request sendet, wird diese Pipeline durchgegangen
//z.B.: Lokalisierung anhand von IP-Adressen
//WICHTIG: Reihenfolge beachten

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