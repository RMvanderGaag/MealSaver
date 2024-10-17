using DomainServices.Repositories;
using DomainServices.Services;
using DomainServices.Services.Interface;
using Infrastructure.Contexts;
using Infrastructure.EntityFramework;
using MealSaverAPI.GraphQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Register DbContext
builder.Services.AddDbContext<MealSaverEFDBContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("EFDBConnection"));
});

// Register repositories and services
builder.Services.AddScoped<IMealPackageRepository, MealPackageRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICanteenRepository, CanteenRepository>();
builder.Services.AddScoped<ICanteenEmployeeRepository, CanteenEmployeeRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IMealPackageService, MealPackageService>();
builder.Services.AddScoped<ICanteenService, CanteenService>();

// Add GraphQL
builder.Services.AddGraphQLServer()
    .AddQueryType<MealPackageQuery>()
    .AddProjections();

// Add swagger and middleware
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapGraphQL();

app.Run();