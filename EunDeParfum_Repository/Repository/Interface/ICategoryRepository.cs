using EunDeParfum_Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<bool> CreateCategoryAsync(Category category);
        Task<bool> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int categoryId);
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task<List<Category>> GetAllCategoriesAsync();
    }
}
