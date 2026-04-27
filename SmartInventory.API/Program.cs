using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SmartInventory.Application.Interfaces;
using SmartInventory.Application.Interfaces.Product_Interfaces;
using SmartInventory.Application.Interfaces.Repo_Interfaces;
using SmartInventory.Application.Interfaces.Service_Interfaces.Product_Interface;
using SmartInventory.Application.Interfaces.Service_Interfaces.Supplier_Interface;
using SmartInventory.Application.Mappings;
using SmartInventory.Application.Services;
using SmartInventory.Application.Services.Product_Service;
using SmartInventory.Infrastructure.Data;
using SmartInventory.Infrastructure.Identity;
using SmartInventory.Infrastructure.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString(
            "DefaultConnection")));


// DI
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<ISupplierRepository,SupplierRepository>();
builder.Services.AddScoped<ISupplierService,SupplierService>();


// AutoMapper
builder.Services.AddAutoMapper(
typeof(MappingProfile));


// Identity
builder.Services.AddIdentity<
ApplicationUser,
IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();


// JWT
builder.Services
.AddAuthentication(
JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
      new TokenValidationParameters
      {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,

          ValidIssuer =
            builder.Configuration["Jwt:Issuer"],

          ValidAudience =
            builder.Configuration["Jwt:Audience"],

          IssuerSigningKey =
          new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
              builder.Configuration["Jwt:Key"]))
      };
});

// Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(
    "AllowAngular",
    p => p.WithOrigins(
    "http://localhost:4200")
    .AllowAnyHeader()
    .AllowAnyMethod());
});
// Controllers
builder.Services.AddControllers();


// Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();