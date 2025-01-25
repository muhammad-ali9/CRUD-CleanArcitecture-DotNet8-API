using Application.Interfaces;
using Domain.ProductModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Product.Queries
{
    public class GetProductByIdQuery : IRequest<tbl_Product>
    {
        public int Id { get; set; }

        internal class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, tbl_Product>
        {
            private readonly IApplicationDbContext _context;

            public GetProductByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<tbl_Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var productExists = await _context.tbl_Products.Where(p => p.ID == request.Id).FirstOrDefaultAsync();

                if (productExists == null)
                {
                    return default;
                }
                return productExists;
            }
        }
    }
}
