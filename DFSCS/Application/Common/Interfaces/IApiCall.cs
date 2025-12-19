using Domain.Entities.Common;

namespace Application.Common.Interfaces
{
    public interface IApiCall
    {
        public Task<RepT> CallApi<RepT, ReqT>(ReqT requestBody, ApiData apiData);
    }
}
