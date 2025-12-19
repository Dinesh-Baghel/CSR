using Application.Common.Behaviors;
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
    public class GetAnswersDetailsHandler : IRequestHandler<GetAnswersDetails, IEnumerable<AnswerDetailsitem>>
    {
        private readonly IDapper _dapper;

        public GetAnswersDetailsHandler(IDapper dapper)
        {
            _dapper = dapper;

        }
        public async Task<IEnumerable<AnswerDetailsitem>> Handle(GetAnswersDetails request, CancellationToken cancellationToken)
        {
            object para = new
            {
                REFNUMBER = request.req.refernceNumber,
            };
            var products = await _dapper.QueryWithMappingAsync<AnswerDetailsitem>("PRO_GET_ANSWER_DETAILS", para);
           
            for (int i = 0; i < products.ToList().Count; i++)
            {
                List<string> imgres = new List<string>();
                var item = products.ToList()[i];
                string[] imgurl = item.imgfile.Trim().Split(',');
                for (int j = 0; j < imgurl.Length-1; j++)
                {
                    string imagebase64 = ImageBase.ImageToBase64(imgurl[j]);
                    imgres.Add(imagebase64);
                }
                products.ToList()[i].imgBase = imgres.ToArray();
            }


            return products;
        }
    }
}
