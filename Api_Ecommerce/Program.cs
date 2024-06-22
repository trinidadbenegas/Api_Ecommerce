using Api_Ecommerce;
using Api_Ecommerce.Data;
using Api_Ecommerce.Interfaces;
using Api_Ecommerce.Models;
using Api_Ecommerce.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//.AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//    options.JsonSerializerOptions.WriteIndented = true;
//});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultString")));

//Configure services
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IMarcaService,MarcaService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IShoppingCart, ShoppingCartService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);


//Authentication and authorization
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();

app.Run();
