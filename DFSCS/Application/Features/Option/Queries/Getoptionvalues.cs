using Application.Common.Interfaces;
using Dapper;
using Domain.Entities.Request;
using Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Option.Queries
{
    public class Getoptionvalues
    {
        private readonly IDapper _dapper;

        public Getoptionvalues(IDapper dapper)
        {
            _dapper = dapper;
        }
        public async Task<IEnumerable<OptionValuesResponses>> getvalueslistdata()
        {
           return await _dapper.QueryWithMappingAsync<OptionValuesResponses>("PRO_GET_OPTION");
        }

    }
}
