using Application.Common.Interfaces;
using Domain.Entities.Model;
using MediatR;

namespace Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var rows = await _unitOfWork.Repository<Product>().DeleteAsync(request.Id);
            await _unitOfWork.CommitAsync();
            return rows > 0;
        }
    }
}
