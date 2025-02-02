using Domain.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IProduct
    {
        Task<tbl_Product> GetProductByIdInterface(int id);
        Task<int> CreateProduct(tbl_Product product);
        Task<string> UpdateProduct(tbl_Product product);
        Task<IEnumerable<tbl_Product>> GetAllProducts();
        Task<string> DeleteProduct(int id);
    }
}
