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
    public class UpdateProductCommand : IRequest<int>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }

        internal class UpdateProductCommadHandler : IRequestHandler<UpdateProductCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public UpdateProductCommadHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var productExist = await _context.tbl_Products.Where(p => p.ID == request.ID).FirstOrDefaultAsync();
                if (productExist != null)
                {
                    productExist.Name = request.Name;
                    productExist.Description = request.Description;
                    productExist.Rate = request.Rate;

                    await _context.SaveChangesAsync();
                    return productExist.ID;
                }
                return default;
            }
        }
    }
}
