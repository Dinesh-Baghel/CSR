using Domain.Entities.Modals;
using Domain.Entities.Request;
using Domain.Entities.Response;

namespace BSRApplication.Interfaces.V1
{
    public interface IAnyList
    {
        public Task<GetListRes> GetList(GetAnyListReq req);
    }
}
