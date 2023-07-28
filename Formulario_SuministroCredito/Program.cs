using FluentValidation;
using FluentValidation.AspNetCore;
using Formulario_SuministroCredito.Data;
using Formulario_SuministroCredito.Models;
using Formulario_SuministroCredito.Service;
using Formulario_SuministroCredito.Validator;
using MySql.Data.MySqlClient;
using MySqlConnector;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

//sqlserver(local)
//var dbConnectionString = builder.Configuration.GetConnectionString("conexionPredeterminada");
//builder.Services.AddSingleton<IDbConnection>((sp) => new SqlConnection(dbConnectionString));

//sqlserver Unoee
var dbConnectionStringUnoee = builder.Configuration.GetConnectionString("conexionUnoee");
builder.Services.AddSingleton<IDbConnection>((sp) => new SqlConnection(dbConnectionStringUnoee));


//MySql servidor para integraciones signRequest
var dbMySqlConexion = builder.Configuration.GetConnectionString("mySqlConexion");
builder.Services.AddTransient<IDbConnection>((sp) => new MySqlConnector.MySqlConnection(dbMySqlConexion));


//MySql
//builder.Services.AddTransient<MySqlConnector.MySqlConnection>(_ =>
//    new MySqlConnector.MySqlConnection(builder.Configuration.GetConnectionString("mySqlConexion")));




//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://raw.githubusercontent.com/marcovega/colombia-json/master/colombia.min.json") });

//inyeccción mi propio dependencia
builder.Services.AddScoped<ISumiCredRepository, SumiCredRepository>();

//inyección servicio de carga archivos al drive
builder.Services.AddScoped<IServiceFileUpload, ServiceFileUpload>(); 

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
    pattern: "{controller=SumiCred}/{action=Insert}/{id?}");

//carga de paquetes .exe rotativa PDF
IWebHostEnvironment env = app.Environment;
Rotativa.AspNetCore.RotativaConfiguration.Setup(env.WebRootPath, "../Rotativa");



app.Run();
