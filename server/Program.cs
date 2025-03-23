using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Banyan.Test.WeatherAPI;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins(
            "http://localhost:3000"       // React app origin
        )
        .AllowAnyMethod()                  // Allow GET, POST, PUT, DELETE, etc.
        .AllowAnyHeader()                  // Allow all headers
        .AllowCredentials();               // Allow cookies and authorization headers
    });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<WeatherDbContext>(options =>
    options.UseSqlite("Data Source=weather.db"));
builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();
builder.Services.AddScoped<JWTService>();
builder.Services.AddScoped<PasswordHasher>();
builder.Services.AddHttpClient<OpenWeatherService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;        // Use default version if none is specified
    options.DefaultApiVersion = new ApiVersion(1, 0);           // Set default version
    options.ReportApiVersions = true;                           // Display version info in response headers
    options.ApiVersionReader = new UrlSegmentApiVersionReader(); // Use version in URL path
});

// Add versioned API Explorer (required for Swagger)
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";                         // Format: "v1"
    options.SubstituteApiVersionInUrl = true;                   // Substitute {version} with actual version
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtConfig = builder.Configuration.GetSection("Jwt");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig["Issuer"],
            ValidAudience = jwtConfig["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtConfig["Key"]!)
            )
        };
    });
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// JWT Authentication

app.UseCors("AllowLocalhost");
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.MapControllers();
// Redirect root URL to Swagger
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});


app.Run();
