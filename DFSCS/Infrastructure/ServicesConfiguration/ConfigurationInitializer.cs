using Domain.Entities.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.ServicesConfiguration
{
    public static class ConfigurationInitializer
    {
        public static void InitializeApiSettings(IConfiguration configuration)
        {
            ApiMasterData.apiData = configuration.GetSection("MasterApiSettings").Get<ApiData>() ?? new ApiData();
            var apiNamesSettings = configuration.GetSection("ApiNamesSettings").Get<ApiNames>() ?? new ApiNames();
            AllApiNames.Initialize(apiNamesSettings);
        }
    }
}
