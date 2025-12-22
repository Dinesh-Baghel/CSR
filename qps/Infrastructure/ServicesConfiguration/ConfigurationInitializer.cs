using Domain.Entities.Common;
using Microsoft.Extensions.Configuration;
using MyNewEncDec;
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
            New_Enc_Dec _Enc_Dec = new();
            ApiMasterData.apiData = configuration.GetSection("MasterApiSettings").Get<ApiData>() ?? new ApiData();
            if (!string.IsNullOrEmpty(ApiMasterData.apiData.Api_Authorization))
            {

                var xxx = _Enc_Dec.My_Encode(ApiMasterData.apiData.Api_Authorization);
                ApiMasterData.apiData.Api_Authorization = _Enc_Dec.My_Decode(ApiMasterData.apiData.Api_Authorization);
            }
            var apiNamesSettings = configuration.GetSection("ApiNamesSettings").Get<ApiNames>() ?? new ApiNames();
            AllApiNames.Initialize(apiNamesSettings);
        }
    }
}
