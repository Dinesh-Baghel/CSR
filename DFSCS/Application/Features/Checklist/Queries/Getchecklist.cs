using API.Models.Common;
using Application.Common.Interfaces;
using AutoMapper;
using Dapper;
using Domain.Entities.Request;
using Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Checklist.Queries
{
    public class Getchecklist
    {
        private readonly IDapper _dapper;

        public Getchecklist(IDapper dapper)
        {
            _dapper = dapper;
        }
        public async Task<IEnumerable<ChecklistResponse>> getchecklistdata(ChecklistRequest request)
        {
            var parameters = new DynamicParameters();
            //Input parameters
            parameters.Add("@PAGENUMBER", request.pageNumber, DbType.Int32);
            parameters.Add("@PAGESIZE", request.pageSize, DbType.Int32);
            return await _dapper.QueryWithMappingAsync<ChecklistResponse>("PRO_GET_CHECK_LIST_MASTER", parameters);
        }

       

    }
}
