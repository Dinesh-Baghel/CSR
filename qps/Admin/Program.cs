
using Admin.Components;
using Application.AutoMapper;
using Application.Interfaces.V1;
using Application.Services;
using Domain.Entities.Common;
using Domain.Entities.Modals;
using Domain.Entities.Response;
using EmailService;
using Infrastructure.Services.V1;
using Infrastructure.ServicesConfiguration;
using Microsoft.Extensions.Options;
using MudBlazor;
using MudBlazor.Services;
using MudExtensions.Services;
using MyNewEncDec;



var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
// Add MudBlazor services
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});
builder.Services.AddControllers();
builder.Services.AddMudExtensions();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<Application.Services.MenuService>();
builder.Services.AddScoped<SessionService>();
builder.Services.AddScoped<SharedStateService>();
builder.Services.AddSingleton<NavMenuService>();
builder.Services.AddSingleton<New_Enc_Dec>();
builder.Services.AddSingleton<EncryptedQueryString>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
int MaxSessionTime = builder.Configuration.GetValue<int>("AppConfigurationSettings:MaxSessionTime");
builder.Services.AddAuthentication(Cons.AuthScheme)
            .AddCookie(Cons.AuthScheme, Options =>
            {
                Options.Cookie.Name = Cons.AuthCookie;
                Options.LoginPath = "/Account/Login";
                Options.LogoutPath = "/Account/User-Logout";
                Options.AccessDeniedPath = "/auth/access-denied";

                Options.Cookie.HttpOnly = true;
                Options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                Options.Cookie.SameSite = SameSiteMode.Strict;

                Options.ExpireTimeSpan = TimeSpan.FromMinutes(MaxSessionTime);
                Options.SlidingExpiration = true;
                //Options.SlidingExpiration = true;
            });


builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options =>
    {
        options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(MaxSessionTime); // Keep circuit for 30 min
        options.JSInteropDefaultCallTimeout = TimeSpan.FromMinutes(2);
    });
builder.Services.AddSignalR(options =>
{
    options.ClientTimeoutInterval = TimeSpan.FromMinutes(MaxSessionTime); // Wait for client
    options.KeepAliveInterval = TimeSpan.FromMinutes(2);     // Send keepalive ping
});
// Register New_Enc_Dec (assuming it has its own constructor, you can add it like so)
builder.Services.AddSingleton<New_Enc_Dec>();
//
//builder.Services.AddScoped<ILocalStorageService,LocalStorageService>();
ConfigurationInitializer.InitializeApiSettings(builder.Configuration);
// Read ProxyValue from appsettings.json
int useProxyValue = builder.Configuration.GetValue<int>("AppConfigurationSettings:ProxyValue");
string logFilePath = builder.Configuration.GetValue<string>("ErrorLoggingSettings:LogFilePath")!;
builder.Services.AddInfrastructure(useProxyValue, logFilePath!);
// Load BSRSettings from appsettings.json
builder.Services.Configure<BSRSettings>(builder.Configuration.GetSection("BSRSettings"));
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<BSRSettings>>().Value);
// Bind ServerSettings once from appsettings.json
builder.Services.Configure<EmailServerSettings>(builder.Configuration.GetSection("EmailServerSettings"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<EmailServerSettings>>().Value);

// If you prefer singleton (direct instance): another way
//builder.Services.AddSingleton(sp =>
//    builder.Configuration.GetSection("ServerSettings").Get<EmailServerSettings>()!);
builder.Services.AddAutoMapper(typeof(ServerSettingsProfile));
builder.Services.AddAutoMapper(typeof(ApplicationProfile));
builder.Services.AddTransient<MailSender>();

//Set Data
var ApiMasterService = new APICallService(useProxyValue);
var apiMasterResponse = await ApiMasterService.CallingAPI<ApiMasterRes, string>(AllApiNames.MasterApi, "");
if (apiMasterResponse.responseCode == 0 && apiMasterResponse.responseMessage!.ToUpper() == "SUCCESS" && apiMasterResponse.ApiData?.Count > 0)
{
    AllApi.ApiDetails = apiMasterResponse.ApiData;
}
// Add services to the container.
var app = builder.Build();

app.Use(async (context, next) =>
{
    // Block /robots.txt for all bots
    if (context.Request.Path == "/robots.txt")
    {
        context.Response.StatusCode = 403;
        await context.Response.WriteAsync("Access Denied");
        return;
    }
    // Security headers
    var headers = context.Response.Headers;
    headers.Remove("X-Powered-By");
    headers.Add("X-Powered-By", "VMM");
    headers["Server"] = "VMM";
    headers["X-Frame-Options"] = "SAMEORIGIN";
    headers["X-XSS-Protection"] = "1; mode=block";
    headers["X-Content-Type-Options"] = "nosniff";
    headers["Content-Security-Policy"] =
        "default-src 'self'; " +
        "script-src 'self' 'unsafe-inline' 'unsafe-eval' blob:; " +
        "style-src 'self' 'unsafe-inline'; " +
        "img-src 'self' data:; " +
        "font-src 'self'; " +
        "connect-src 'self' wss:; " +
        "frame-src 'self'; " +
        "media-src 'self';";
    headers["Strict-Transport-Security"] = "max-age=31536000; includeSubDomains";
    headers["Feature-Policy"] = "vibrate 'none'";
    headers["Referrer-Policy"] = "strict-origin";
    await next();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.MapControllers();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
//app.UseMiddleware<AUserMiddleware>();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
// Add additional endpoints required by the Identity /Account Razor components.
app.Run();
