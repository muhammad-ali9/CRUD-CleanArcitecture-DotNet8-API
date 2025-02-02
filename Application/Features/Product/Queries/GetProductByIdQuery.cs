using Application.Exceptions;
using Application.Interfaces;
using Application.Wrapper;
using Domain.ProductModel;
using Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Product.Queries
{
    public class GetProductByIdQuery : IRequest<ApiResponse<tbl_Product>>
    {
        public int Id { get; set; }

        internal class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ApiResponse<tbl_Product>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IProduct _product;
            public GetProductByIdQueryHandler(IApplicationDbContext context, IProduct product)
            {
                _context = context;
                _product = product;
            }

            public async Task<ApiResponse<tbl_Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var productExists = await _product.GetProductByIdInterface(request.Id);

                if (productExists == null)
                {
                    throw new ApiException("Product Not Found");
                }
                return new ApiResponse<tbl_Product>{Data = productExists, Message = "Successfully Fetched."};
            }
        }
    }
}
