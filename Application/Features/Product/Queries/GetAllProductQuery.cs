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
    public class GetAllProductQuery : IRequest<IEnumerable<tbl_Product>>
    {
        internal class GetAllProductCommandHandler : IRequestHandler<GetAllProductQuery, IEnumerable<tbl_Product>>
        {
            private readonly IApplicationDbContext _context;

            public GetAllProductCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<tbl_Product>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
            {
                var product = await _context.tbl_Products.ToListAsync();

                return product;

            }
        }
    }

}
