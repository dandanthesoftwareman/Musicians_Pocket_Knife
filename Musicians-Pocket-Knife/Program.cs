using Microsoft.EntityFrameworkCore;
using Musicians_Pocket_Knife.Models;
using Musicians_Pocket_Knife.Orchestrators;
using Musicians_Pocket_Knife.Profiles;
using Musicians_Pocket_Knife.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserRepository>(provider => new UserRepository(connectionString));
builder.Services.AddScoped<IPlaylistRepository>(provider =>
{
    var context = provider.GetRequiredService<MpkdbContext>();
    return new PlaylistRepository(context, connectionString);
});
builder.Services.AddScoped<ISongRepository, SongRepository>();
builder.Services.AddScoped<IUserOrchestrator, UserOrchestrator>();
builder.Services.AddScoped<IPlaylistOrchestrator, PlaylistOrchestrator>();
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
app.UseDeveloperExceptionPage();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();