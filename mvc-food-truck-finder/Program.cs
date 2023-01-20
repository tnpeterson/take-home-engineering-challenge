using Microsoft.EntityFrameworkCore;
using mvc_food_truck_finder.Data;
using mvc_food_truck_finder.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FoodTruckContext>(options => options.UseInMemoryDatabase("FoodTrucks"));
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

FoodTruckInitializer.SeedTrucks(app).Wait();

app.Run();

