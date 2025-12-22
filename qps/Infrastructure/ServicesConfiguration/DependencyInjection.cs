using Application.Interfaces.V1;
using BSRApplication.Interfaces.V1;
using Domain.Entities.Common;
using Domain.Entities.Modals;
using Domain.Entities.Response;
using Infrastructure.Services;
using Infrastructure.Services.V1;
using Infrastructure.Utilitys;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ServicesConfiguration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, int useProxyValue, string logFilePath)
        {

            //Register ApiMasterService as IAPICall
            services.AddSingleton<IAPICall>(provider => new APICallService(useProxyValue));
            // Register SQLHelper (add it to the DI container)
            services.AddSingleton<SQLHelper>();
            // Register DapperHelper (add it to the DI container)
            services.AddSingleton<DapperHelper>();
            //// Register APIService as IAPI
            services.AddScoped<IAPI, APIService>();
            // Register APIService as IAPI
            services.AddScoped<IUser, UserService>();

            services.AddScoped<IVendor, VendorService>();
            // Register RoleService as IRole
            services.AddScoped<IRole, RoleService>();
            // Register PPSRequestService as IPPSRequest
            services.AddScoped<IPPSRequest, PPSRequestService>();
            // Register RoleService as IRole
            services.AddScoped<IMenu, MenuService>();
            // Register SetFlagsService as ISetFlags
            services.AddScoped<ISetFlags, SetFlagsService>();
            // Register AppsBsrService as IAppsBsr
            services.AddScoped<IAppsBsr, AppsBsrService>();
            // Register FiltersService as IFilters
            services.AddScoped<IFilters, FiltersService>();
            // Register AnyListService as IAnyList
            services.AddScoped<IAnyList, AnyListService>();
            


            return services;
        }
    }
}
