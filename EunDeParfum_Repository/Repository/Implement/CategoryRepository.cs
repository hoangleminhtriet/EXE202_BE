using EunDeParfum_Repository.DbContexts;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Repository.Implement
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Tạo mới một Category
        public async Task<bool> CreateCategoryAsync(Category category)
        {
            try
            {
                await _context.Categories.AddAsync(category); // Thêm category vào DbSet
                await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
                return true; // Trả về true nếu thành công
            }
            catch (Exception ex)
            {
                throw ex; // Ném lại lỗi nếu có lỗi
            }
        }

        // Cập nhật thông tin của Category
        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            try
            {
                _context.Categories.Update(category); // Cập nhật Category trong DbSet
                await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
                return true; // Trả về true nếu thành công
            }
            catch (Exception ex)
            {
                throw ex; // Ném lại lỗi nếu có lỗi
            }
        }

        // Xóa một Category theo Id
        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            try
            {
                var category = await _context.Categories.FindAsync(categoryId); // Tìm category theo Id
                if (category == null)
                {
                    return false; // Trả về false nếu không tìm thấy category
                }

                _context.Categories.Remove(category); // Xóa category khỏi DbSet
                return await _context.SaveChangesAsync() > 0; // Lưu thay đổi và trả về true nếu thành công
            }
            catch (Exception ex)
            {
                return false; // Trả về false nếu có lỗi
            }
        }

        // Lấy thông tin Category theo Id
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            try
            {
                return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id); // Lấy Category theo Id
            }
            catch (Exception ex)
            {
                throw ex; // Ném lại lỗi nếu có lỗi
            }
        }

        // Lấy danh sách tất cả các Category
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            try
            {
                return await _context.Categories.ToListAsync(); // Lấy tất cả Category
            }
            catch (Exception ex)
            {
                throw ex; // Ném lại lỗi nếu có lỗi
            }
        }

        // Lấy danh sách Category theo trạng thái (Status)
        public async Task<List<Category>> GetCategoriesByStatusAsync(bool status)
        {
            try
            {
                return await _context.Categories
                    .Where(c => c.Status == status) // Lọc theo trạng thái
                    .ToListAsync(); // Trả về danh sách Category có trạng thái tương ứng
            }
            catch (Exception ex)
            {
                throw ex; // Ném lại lỗi nếu có lỗi
            }
        }
    }
}
