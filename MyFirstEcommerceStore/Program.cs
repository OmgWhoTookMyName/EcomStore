using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MyFirstEcommerceStore.Data;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

//DB login
var cs = "Host=localhost;Username=ecomm_user;Password=masonjar;Database=ecomm_app";

using var con = new NpgsqlConnection(cs);
con.Open();

using var cmd = new NpgsqlCommand();
cmd.Connection = con;

cmd.CommandText = "CREATE TABLE if not exists products(id SERIAL PRIMARY KEY, ProductName VARCHAR(75), price INT, ProductDescription VARCHAR(255), ProductId VARCHAR(50))";
cmd.ExecuteNonQuery();

//cmd.CommandText = @"CREATE TABLE cars(id SERIAL PRIMARY KEY,
//        name VARCHAR(255), price INT)";
//cmd.ExecuteNonQuery();

//cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Audi',52642)";
//cmd.ExecuteNonQuery();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddBlazoredModal();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
