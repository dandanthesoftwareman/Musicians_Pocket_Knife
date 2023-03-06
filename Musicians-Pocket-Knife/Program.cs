using Microsoft.EntityFrameworkCore;
using Musicians_Pocket_Knife.Models;
using Musicians_Pocket_Knife.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration["ConnectionStrings:ConnectionStringMpkdb"];
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDBRepository, DBRepository>();
builder.Services.AddDbContext<MpkdbContext>(x => x.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();