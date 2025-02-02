using Application.Interfaces;
using Domain.ProductModel;
using Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repo
{
    public class ProductRepo : IProduct
    {
        private readonly IApplicationDbContext _context;

        public ProductRepo(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateProduct(tbl_Product product)
        {
            await _context.tbl_Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product.ID;
        }

        public async Task<string> DeleteProduct(int id)
        {
            var result = await _context.tbl_Products.Where(p => p.ID ==  id).FirstOrDefaultAsync();
            if (result == null)
            {
                return null;
            }
            _context.tbl_Products.Remove(result);
            await _context.SaveChangesAsync();
            return result.ID.ToString();

        }

        public async Task<IEnumerable<tbl_Product>> GetAllProducts()
        {
            var result = await _context.tbl_Products.ToListAsync();
            return result;
        }

        public async Task<tbl_Product> GetProductByIdInterface(int id)
        {
            return  await _context.tbl_Products.Where(x => x.ID == id).FirstOrDefaultAsync();
        }

        public async Task<string> UpdateProduct(tbl_Product product)
        {
            var productExist = await _context.tbl_Products.Where(p => p.ID == product.ID).FirstOrDefaultAsync();
            if (productExist != null)
            {
                productExist.Name = product.Name;
                productExist.Description = product.Description;
                productExist.Rate = product.Rate;

                await _context.SaveChangesAsync();
                return productExist.ID.ToString();
            }
            return null;
        }
    }
}
