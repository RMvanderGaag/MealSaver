using DomainServices.Repositories;
using DomainServices.Services;
using DomainServices.Services.Interface;
using Infrastructure.Contexts;
using Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<IMealPackageService, MealPackageService>();
builder.Services.AddScoped<ICanteenService, CanteenService>();

builder.Services.AddScoped<IMealPackageRepository, MealPackageRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICanteenRepository, CanteenRepository>();
builder.Services.AddScoped<ICanteenEmployeeRepository, CanteenEmployeeRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<MealSaverEFDBContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:EFDBConnection"]);
});

builder.Services.AddDbContext<MealSaverIFDBContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:IFDBConnection"]);
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<MealSaverIFDBContext>();

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