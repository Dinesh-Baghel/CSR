using AutoMapper;
using Application.Common.Interfaces;
using Domain.Entities.Model;
using MediatR;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            // Check if product exists
            var existing = await _unitOfWork.Repository<Product>().GetByIdAsync(request.Id);
            if (existing == null)
                return false;

            // Map command to entity
            var product = _mapper.Map<Product>(request);

            // Update in DB
            var rows = await _unitOfWork.Repository<Product>().UpdateAsync(product);
            if (rows <= 0)
                return false;

            await _unitOfWork.CommitAsync();
            return rows > 0;
        }
    }
}
