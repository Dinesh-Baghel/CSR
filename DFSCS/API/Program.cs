using API.Filters;
using API.Middlewares;
using Application;
using Domain.Entities.Common;
using Infrastructure;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<ApiExceptionFilter>();
//});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var configuration = builder.Configuration;

// Logging
builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

int useProxyValue = builder.Configuration.GetValue<int>("AppConfigurationSettings:ProxyValue");

// Add layered services
builder.Services.AddApplicationServices();//
builder.Services.AddInfrastructureServices(configuration, useProxyValue);

// Bind ServerSettings once from appsettings.json
builder.Services.Configure<ApiData>(builder.Configuration.GetSection("HRApiSettings"));
builder.Services.AddScoped(sp => sp.GetRequiredService<IOptions<ApiData>>().Value);
// Bind JWT settings
var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<TokenSettings>(jwtSection);

var jwtSettings = jwtSection.Get<TokenSettings>();
var key = Encoding.UTF8.GetBytes(jwtSettings.Key);



// Configure authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero // immediate expiry
    };
    // Optional: Hook for debugging
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            switch (context.Exception)
            {
                case SecurityTokenExpiredException:
                    throw new SecurityTokenExpiredException();
                case SecurityTokenInvalidSignatureException:
                    throw new SecurityTokenInvalidSignatureException();
                case SecurityTokenInvalidAudienceException:
                    throw new SecurityTokenInvalidAudienceException();
                case SecurityTokenInvalidIssuerException:
                    throw new SecurityTokenInvalidIssuerException();
                default:
                    throw new SecurityTokenException();
            }
        },
        OnChallenge = context =>
        {
            context.HandleResponse();
            throw new SecurityTokenException();//"Token is missing or invalid"
        },
        OnForbidden = context =>
        {
            throw new UnauthorizedAccessException();
        }
    };
    //options.Events = new JwtBearerEvents
    //{
    //    OnAuthenticationFailed = async context =>
    //    {
    //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    //        context.Response.ContentType = "application/json";

    //        var message = context.Exception switch
    //        {
    //            SecurityTokenExpiredException => "Token expired",
    //            SecurityTokenInvalidSignatureException => "Invalid token signature",
    //            SecurityTokenInvalidAudienceException => "Invalid audience",
    //            SecurityTokenInvalidIssuerException => "Invalid issuer",
    //            _ => "Invalid or malformed token"
    //        };

    //        await context.Response.WriteAsJsonAsync(new
    //        {
    //            status = 401,
    //            error = "Unauthorized",
    //            message
    //        });
    //    },

    //    OnChallenge = async context =>
    //    {
    //        // Skip default behavior
    //        context.HandleResponse();
    //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    //        context.Response.ContentType = "application/json";

    //        await context.Response.WriteAsJsonAsync(new
    //        {
    //            status = 401,
    //            error = "Unauthorized",
    //            message = "Token is missing or invalid"
    //        });
    //    },

    //    OnForbidden = async context =>
    //    {
    //        context.Response.StatusCode = StatusCodes.Status403Forbidden;
    //        context.Response.ContentType = "application/json";

    //        await context.Response.WriteAsJsonAsync(new
    //        {
    //            status = 403,
    //            error = "Forbidden",
    //            message = "You do not have permission to access this resource"
    //        });
    //    }
    //};
    //options.Events = new JwtBearerEvents
    //{
    //    OnMessageReceived = context =>
    //    {
    //        Console.WriteLine($"Token received: {context.Token} {context.Request.Headers.Authorization}");
    //        return Task.CompletedTask;
    //    },
    //    OnTokenValidated = context =>
    //    {
    //        Console.WriteLine($"Token validated for: {context.Principal.Identity.Name}");
    //        return Task.CompletedTask;
    //    },
    //    OnAuthenticationFailed = context =>
    //    {
    //        Console.WriteLine($"Auth failed: {context.Exception.Message}. Token : {context.Request.Headers.Authorization}");
    //        return Task.CompletedTask;
    //    }
    //};
});



builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("Fixed", opt =>
    {
        opt.PermitLimit = 5;
        opt.Window = TimeSpan.FromSeconds(10);
        opt.QueueLimit = 2;
    });
});

// JWT Auth
builder.Services.AddAuthorization();


var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseRouting();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
