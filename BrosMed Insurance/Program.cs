using BrosMed_Insurance.Data;
using BrosMed_Insurance.Models;
using BrosMed_Insurance.Models.Reservation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("BrosMed Insurance");
builder.Services.AddHttpClient("client", options =>
{
    options.BaseAddress = new Uri(connectionString);
});

builder.Services.AddDbContext<ReservationDbContext>(options =>
options.UseSqlServer(connectionString));
builder.Services.AddDbContext<UserDbContext>(options =>
options.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, IdentityRole>(
    options =>
    {
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 0;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;

        options.User.AllowedUserNameCharacters = null; 
        options.User.RequireUniqueEmail = true;
       
    }
    )
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.AddControllers();
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

app.Run();
