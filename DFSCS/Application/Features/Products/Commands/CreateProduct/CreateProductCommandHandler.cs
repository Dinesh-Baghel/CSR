using AutoMapper;
using Application.Common.Interfaces;
using Domain.Entities.Model;
using MediatR;

namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Map DTO to domain entity
            var product = _mapper.Map<Product>(request);

            // Insert into DB (via stored procedure)
            await _unitOfWork.Repository<Product>().AddAsync(product);

            // Commit transaction
            await _unitOfWork.CommitAsync();

            return product.Id;
        }
    }

}
