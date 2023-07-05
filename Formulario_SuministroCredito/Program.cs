using FluentValidation;
using FluentValidation.AspNetCore;
using Formulario_SuministroCredito.Data;
using Formulario_SuministroCredito.Models;
using Formulario_SuministroCredito.Validator;
using MySql.Data.MySqlClient;
using MySqlConnector;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//MySql
var dbMySqlConexion = builder.Configuration.GetConnectionString("mySqlConexion");
builder.Services.AddSingleton<IDbConnection>((sp) => new MySqlConnector.MySqlConnection(dbMySqlConexion));


//sqlserver
//var dbConnectionString = builder.Configuration.GetConnectionString("conexionPredeterminada");
//builder.Services.AddSingleton<IDbConnection>((sp) => new SqlConnection(dbConnectionString));
////inyeccción mi propio dependencia
//builder.Services.AddScoped<ISumiCredRepository, SumiCredRepository>();

//inyeccción mi propio dependencia
builder.Services.AddScoped<ISumiCredRepository, SumiCredRepository>();

//inyección fluentValidator
builder.Services.AddTransient<IValidator<SuministroCredito>, SuministrosValidator>();





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
    pattern: "{controller=SumiCred}/{action=Index}/{id?}");

app.Run();
