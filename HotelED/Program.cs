using HotelED.Core.Entities.Identities;
using HotelED.Core.Interfaces;
using HotelED.Core.Services;
using HotelED.Extensions;
using HotelED.Infrastructure.DbContext;
using HotelED.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
    {
        options.Password.RequiredLength = 8;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
    .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

builder.Services.AddAuthorizationBuilder()
    .AddFallbackPolicy("RequireAuthenticatedUser", policyBuilder => policyBuilder.RequireAuthenticatedUser());

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login";
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
}
app.UseHsts();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
