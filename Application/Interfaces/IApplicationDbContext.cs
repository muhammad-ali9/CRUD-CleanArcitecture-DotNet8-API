using Domain.ProductModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<tbl_Product> tbl_Products { get; set; }
        Task<int> SaveChangesAsync();
    }
}
