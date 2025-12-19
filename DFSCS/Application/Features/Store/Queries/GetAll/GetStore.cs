using Application.DTOs;
using Domain.Entities.Model;
using Domain.Entities.Request;
using Domain.Entities.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Answer.Queries.GetAll
{
    public record GetStoreDetails(StoreDetailsRequest req) : IRequest<IEnumerable<StoreDetails>>;
   
}
