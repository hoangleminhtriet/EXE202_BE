using EunDeParfum_Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Repository.Interface
{
    public interface IProductCateRepository
    {
        Task<bool> CreateProductCateAsync(ProductCategory productCategory);
        Task<bool> UpdateProductCateAsync(ProductCategory productCategory);
        Task<bool> DeleteProductCateAsync(int productCateId );
        Task<ProductCategory> GetProducCateByIdAsync(int id);
        Task<List<ProductCategory>> GetAllProductCateAsync();
    }
}
