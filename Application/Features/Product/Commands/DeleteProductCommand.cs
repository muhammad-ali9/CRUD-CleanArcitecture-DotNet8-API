using Application.Exceptions;
using Application.Interfaces;
using Application.Wrapper;
using Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Product.Commands
{
    public class DeleteProductCommand : IRequest<ApiResponse<string>>
    {
        public int Id { get; set; }

        internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ApiResponse<string>>
        {
            private readonly IProduct _context;

            public DeleteProductCommandHandler(IProduct context)
            {
                _context = context;
            }

            public async Task<ApiResponse<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var productExists = await _context.DeleteProduct(request.Id);
                
                if (productExists == null)
                {
                    throw new ApiException("Product Does not Exist.");
                }
                return new ApiResponse<string>(productExists, "Product Deleted Successfully.");
                


            }
        }
    }
}
