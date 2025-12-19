using Application.Common.Interfaces;
using Application.DTOs;
using Application.Features.Answer.Queries.GetAll;
using Application.Features.Products.Queries.GetAll;
using AutoMapper;
using Domain.Entities.Model;
using Domain.Entities.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Store.Queries.GetAll
{
    public class GetStoreHandler : IRequestHandler<GetStoreDetails, IEnumerable<StoreDetails>>
    {
        private readonly IDapper _dapper;
       
        public GetStoreHandler(IDapper dapper)
        {
            _dapper = dapper;
          
        }
        public async Task<IEnumerable<StoreDetails>> Handle(GetStoreDetails request, CancellationToken cancellationToken)
        {
            object para = new
            {
                @STORE_CODE = request.req.storeCode,
                @LAT = request.req.lattitude,
                @LONG = request.req.longitude               
            };
            var products = await _dapper.QueryWithMappingAsync<StoreDetails>("PRO_GET_STORE_DTL", para);
            return products;
        }
    }
}
