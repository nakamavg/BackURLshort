using UrlShorteners.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls($"http://*:{Environment.GetEnvironmentVariable("PORT") ?? "5000"}");

// Configurar la cadena de conexión desde la variable de entorno DATABASE_URL
string databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

if (string.IsNullOrEmpty(databaseUrl))
{
    throw new Exception("La variable de entorno DATABASE_URL no está configurada.");
}

// Convertir el formato de la cadena de conexión
var uri = new Uri(databaseUrl);
var host = uri.Host;
var port = uri.Port > 0 ? uri.Port : 5432; // Usa 5432 si el puerto no está especificado
var database = uri.AbsolutePath.Trim('/');
var userInfo = uri.UserInfo.Split(':');
var username = userInfo[0];
var password = userInfo[1];

// Construir la cadena de conexión en el formato correcto
string connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password};Ssl Mode=Require;";

// Registrar el DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// Agregar los servicios necesarios para los controladores
builder.Services.AddControllers();

// Construir la aplicación
var app = builder.Build();

// Middleware para manejar solicitudes HTTP
app.UseHttpsRedirection();
app.MapControllers();

// Ejecutar la aplicación
app.Run();