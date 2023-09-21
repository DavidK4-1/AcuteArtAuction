using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using ArtAuction.Data;
using ArtAuction.Services.ArtworkServ;
using ArtAuction.Services.BidServ;
using ArtAuction.Services.GenreServ;
using ArtAuction.Services.UserReviewServ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IArtworkService, ArtworkService>();
builder.Services.AddScoped<IBidService, BidService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IUserReviewService, UserReviewService>();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ArtAuctionDB") ?? throw new InvalidOperationException("Connection string not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    pattern: "{controller=Artwork}/{action=All}/{id?}");
app.MapRazorPages();

app.Run();
