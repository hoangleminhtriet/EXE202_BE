using EunDeParfum_Repository.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Repository.Interface
{
    public interface ICategoryRepository
    {
        // Tạo mới một Category
        Task<bool> CreateCategoryAsync(Category category);

        // Cập nhật thông tin của Category
        Task<bool> UpdateCategoryAsync(Category category);

        // Xóa một Category theo Id
        Task<bool> DeleteCategoryAsync(int categoryId);

        // Lấy thông tin Category theo Id
        Task<Category> GetCategoryByIdAsync(int id);

        // Lấy danh sách tất cả các Category
        Task<List<Category>> GetAllCategoriesAsync();

        // Lấy danh sách Category theo trạng thái (Status)
        Task<List<Category>> GetCategoriesByStatusAsync(bool status);
    }
}
