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
    public class GetAllProductQuery : IRequest<ApiResponse<IEnumerable<tbl_Product>>>
    {
        internal class GetAllProductCommandHandler : IRequestHandler<GetAllProductQuery, ApiResponse<IEnumerable<tbl_Product>>>
        {
          private readonly IProduct _context;

            public GetAllProductCommandHandler(IProduct context)
            {
                _context = context;
            }

            public async Task<ApiResponse<IEnumerable<tbl_Product>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
            {
                var product = await _context.GetAllProducts();
                if(product == null)
                {
                    throw new ApiException("Product Not Available.");
                }
                return new ApiResponse<IEnumerable<tbl_Product>>(product, "Product Fetch Succesfully");

            }
        }
    }

}
