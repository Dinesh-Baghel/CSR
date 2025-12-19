using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Security;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyNewEncDec;


namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config, int useProxyValue)
        {
            // Bind TokenSettings
            services.Configure<TokenSettings>(config.GetSection("Jwt"));
            services.AddSingleton<IApiCall>(provider => new ApiCallService(useProxyValue));

            // Register JwtService
            services.AddScoped<IJwtService, JwtService>();

            // Dapper Context
            services.AddScoped<IDapper,DapperContext>();

            // Unit of Work + Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<New_Enc_Dec>();


            return services;
        }
    }
}
