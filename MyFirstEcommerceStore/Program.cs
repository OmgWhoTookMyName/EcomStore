using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MyFirstEcommerceStore.Data;
using Npgsql;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

//DB login + add tables
//TODO: Move this db login stuff to config file?
var cs = "Host=localhost;Username=ecomm_user;Password=masonjar;Database=ecomm_app";

using var con = new NpgsqlConnection(cs);
con.Open();

using var cmd = new NpgsqlCommand();
cmd.Connection = con;
//TODO: Move the Db creation to its own class
cmd.CommandText = "CREATE TABLE if not exists products(id SERIAL PRIMARY KEY, ProductName VARCHAR(75), price INT, ProductDescription VARCHAR(255), ProductId VARCHAR(50))";
cmd.ExecuteNonQuery();
cmd.CommandText = "CREATE TABLE if not exists Brands(id SERIAL PRIMARY KEY, Name VARCHAR(60), Description VARCHAR(255), BrandId VARCHAR(50))";
cmd.ExecuteNonQuery();
cmd.CommandText = "CREATE TABLE if not exists BrandLinks(id SERIAL PRIMARY KEY, ProductId VARCHAR(50), BrandId VARCHAR(50))";
cmd.ExecuteNonQuery();
cmd.CommandText = "CREATE TABLE if not exists ProductImages(id SERIAL PRIMARY KEY, ProductId VARCHAR(50), URL VARCHAR(1000))";
cmd.ExecuteNonQuery();
cmd.CommandText = "CREATE TABLE if not exists Categories(id SERIAL PRIMARY KEY, CategoryId VARCHAR(50), Name VARCHAR(100), Description VARCHAR(1000), ParentCategoryId VARCHAR(50), Tier VARCHAR(5))";
cmd.ExecuteNonQuery();
cmd.CommandText = "CREATE TABLE if not exists CategoryLinks(id SERIAL PRIMARY KEY, CategoryId VARCHAR(50), ProductId VARCHAR(50))";
cmd.ExecuteNonQuery();

con.Close();


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
