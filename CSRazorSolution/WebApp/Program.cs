#region Additional Namespaces
using Microsoft.EntityFrameworkCore;
using WestWindSystem;
#endregion

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// A service is anything you are going to use in the WestWind system. Also known as methods (or a method call)
// Setup the connection string service for the application.
// 1) retreive the connection string information from your appsetting.json
var connectionString = builder.Configuration.GetConnectionString("WWDB");

// 2) register any "Services" you wish to use.
// in our solution our services will be created (coded) in the class library WestWindSystem
// One of these services will be the setup of the database context connection
// another service will be creating as the application requires.

// this setup can be done here, locally
// this setup can also be done elsewhere and called from this location ****
builder.Services.WWBackendDependencies(options => options.UseSqlServer(connectionString));


// 3) register any "services that are created in the class library you wish to use
// in our solution our services will be created (coded) in the class library WestWindSystem



builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
