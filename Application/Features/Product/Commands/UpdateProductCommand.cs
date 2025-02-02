using Application.Exceptions;
using Application.Interfaces;
using Application.Wrapper;
using AutoMapper;
using Domain.ProductModel;
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
    public class UpdateProductCommand : IRequest<ApiResponse<string>>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }

        internal class UpdateProductCommadHandler : IRequestHandler<UpdateProductCommand, ApiResponse<string>>
        {
            private readonly IProduct _context;
            private readonly IMapper _mapper;

            public UpdateProductCommadHandler(IProduct context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ApiResponse<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var productExist = await _context.UpdateProduct(_mapper.Map<tbl_Product>(request));
                if (productExist == null)
                {
                    throw new ApiException("Product not found");
                }
                return new ApiResponse<string>(productExist, "Product Updated Succesfully");
            }
        }
    }
}
