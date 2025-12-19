using Application.DTOs;
using Domain.Entities.Model;
using Domain.Entities.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Answer.Queries.GetAll
{
    public record GetAnswersDetails(AnswerDetailsRequest req) : IRequest<IEnumerable<AnswerDetailsitem>>;
   
}
