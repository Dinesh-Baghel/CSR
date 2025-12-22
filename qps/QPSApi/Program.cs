using Application.Interfaces;
using BSRApi.Interfaces;
using BSRApi.Services;
using Domain.Entities.Common;
using Infrastructure;
using Infrastructure.Middleware;
using Infrastructure.Services;
using Infrastructure.ServicesConfiguration;
using Infrastructure.Utilitys;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyNewEncDec;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Configure services
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Set the PropertyNamingPolicy to 'null' to preserve the PascalCase of C# properties
        options.JsonSerializerOptions.PropertyNamingPolicy = null;  // Preserve the original casing (PascalCase)

        // Alternatively, use JsonNamingPolicy.CamelCase if you want camelCase naming convention
        // options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; // Use camelCase for JSON responses
    });

// JWT configuration (adjust these settings as needed)
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
string secretKey = jwtSettings["SecretKey"]!; // This should be in your configuration
New_Enc_Dec _Enc_Dec = new();
secretKey = _Enc_Dec.My_Decode(secretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Set to true in production for HTTPS
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = false,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

// Register New_Enc_Dec (assuming it has its own constructor, you can add it like so)
builder.Services.AddSingleton<New_Enc_Dec>();
ConfigurationInitializer.InitializeApiSettings(builder.Configuration);
// Read ProxyValue from appsettings.json
int useProxyValue = builder.Configuration.GetValue<int>("AppConfigurationSettings:ProxyValue");
string logFilePath = builder.Configuration.GetValue<string>("ErrorLoggingSettings:LogFilePath")!;
builder.Services.AddInfrastructure(useProxyValue, logFilePath!);
// ?? Add file logging to built-in ILogger
builder.Logging.ClearProviders(); // Optional: removes Console, Debug, etc.
builder.Logging.AddFile("Logs/log-{Date}.txt", minimumLevel: LogLevel.Error);
builder.Services.AddScoped<IReporting, ReportingService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your Bearer token in the format **'Bearer {your token}'**"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
    // ?? This adds 401 and 403 automatically for all [Authorize] endpoints
    options.OperationFilter<AddAuthErrorResponsesOperationFilter>();
});
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowBlazorApp", policy =>
//        policy.WithOrigins(
//            "https://localhost:7175",
//            "http://localhost:5091",
//            "https://devapp.vishalmegamart.com:8025",
//            "http://192.168.0.175:8025",
//            "http://192.168.0.246:8124"
//            )
//              .AllowAnyHeader()
//              .AllowAnyMethod()); 8864821628- Ram /8864819634-
//});
// Bind CORS settings from configuration
//builder.Services.Configure<CorsSettings>(builder.Configuration.GetSection("Cors"));
builder.Services.AddCors(options =>
{
    var corsSettings = builder.Configuration.GetSection("Cors").Get<CorsSettings>();

    options.AddPolicy("AllowBlazorApp", policy =>
        policy.WithOrigins(corsSettings!.AllowBlazorApp)
              .AllowAnyHeader()
              .AllowAnyMethod());
});
var app = builder.Build();
app.Use(async (context, next) =>
{
    var origin = context.Request.Headers["Origin"].ToString();

    if (!string.IsNullOrEmpty(origin) && origin != "https://localhost:7171")
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        await context.Response.WriteAsync("Origin not allowed");
        return;
    }

    await next();
});
// Enable CORS
//var corsSettings = app.Services.GetRequiredService<IOptions<CorsSettings>>().Value;

//app.UseCors(options =>
//{
//    options.WithOrigins(corsSettings.AllowBlazorApp)
//           .AllowAnyHeader()
//           .AllowAnyMethod();
//});
app.UseCors("AllowBlazorApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use the custom StaticTokenMiddleware before UseAuthorization and MapControllers
app.UseMiddleware<StaticTokenMiddleware>(); // Register the middleware
app.UseAuthentication(); // This enables JWT authentication
app.UseAuthorization(); // This ensures endpoints require authorization
app.MapControllers();
app.Run();
