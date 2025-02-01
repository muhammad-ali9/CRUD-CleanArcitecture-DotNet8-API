using Application.Interfaces;
using AutoMapper;
using Domain.ProductModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Product.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        internal class CreateProductCommandHanlder : IRequestHandler<CreateProductCommand, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public CreateProductCommandHanlder(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var product =  _mapper.Map<tbl_Product>(request);
                //var product = new tbl_Product();
                //product.Name = request.Name;
                //product.Description = request.Description;
                //product.Rate = request.Rate;

                await _context.tbl_Products.AddAsync(product);
                await _context.SaveChangesAsync();

                return product.ID;
            }
        }
    }
}
