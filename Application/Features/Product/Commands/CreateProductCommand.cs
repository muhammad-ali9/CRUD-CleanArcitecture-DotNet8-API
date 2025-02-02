using Application.Interfaces;
using AutoMapper;
using Domain.ProductModel;
using Domain.Repository;
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
            private readonly IProduct _product;
            private readonly IMapper _mapper;

            public CreateProductCommandHanlder(IMapper mapper, IProduct product)
            {
                _mapper = mapper;
                _product = product;
            }

            public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var prod =  _mapper.Map<tbl_Product>(request);
                //var product = new tbl_Product();
                //product.Name = request.Name;
                //product.Description = request.Description;
                //product.Rate = request.Rate;

              var saveRecord =   await _product.CreateProduct(prod);

                return saveRecord;
            }
        }
    }
}
