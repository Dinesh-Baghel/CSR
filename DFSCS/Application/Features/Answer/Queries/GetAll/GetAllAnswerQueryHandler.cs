using Application.Common.Interfaces;
using Application.DTOs;
using Application.Features.Products.Queries.GetAll;
using AutoMapper;
using Domain.Entities.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Answer.Queries.GetAll
{
    public class GetStoreHandler : IRequestHandler<GetAllAnswersQuery, IEnumerable<Answerdata>>
    {
        private readonly IDapper _dapper;
       
        public GetStoreHandler(IDapper dapper)
        {
            _dapper = dapper;
          
        }
        public async Task<IEnumerable<Answerdata>> Handle(GetAllAnswersQuery request, CancellationToken cancellationToken)
        {
            object para = new
            {
                PAGENUMBER = request.req.pageNumber,
                PAGESIZE = request.req.pageSize,
                FROMDATE = request.req.fromDate,
                UPTODATE = request.req.uptoDate
            };
            var products = await _dapper.QueryWithMappingAsync<Answerdata>("PRO_GET_ANSWER_MASTER", para);
            return products;
        }
    }
}
