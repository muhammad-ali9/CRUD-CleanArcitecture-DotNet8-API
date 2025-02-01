using Application.Exceptions;
using Application.Interfaces;
using Application.Wrapper;
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
            private readonly IApplicationDbContext _context;

            public DeleteProductCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ApiResponse<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var productExists = await _context.tbl_Products.Where(p => p.ID == request.Id).FirstOrDefaultAsync();
                
                if (productExists == null)
                {
                    throw new ApiException("Product Does not Exist.");
                }

                _context.tbl_Products.Remove(productExists);
                await _context.SaveChangesAsync();
                return new ApiResponse<string>(productExists.ID.ToString(), "Product Deleted Successfully.");
                


            }
        }
    }
}
