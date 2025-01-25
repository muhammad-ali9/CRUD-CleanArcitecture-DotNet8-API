using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Product.Commands
{
    public class DeleteProductCommand : IRequest<int>
    {
        public int Id { get; set; }

        internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public DeleteProductCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var productExists = await _context.tbl_Products.Where(p => p.ID == request.Id).FirstOrDefaultAsync();
                
                if (productExists == null)
                {
                    return default;
                }

                _context.tbl_Products.Remove(productExists);
                await _context.SaveChangesAsync();
                return request.Id;
                


            }
        }
    }
}
