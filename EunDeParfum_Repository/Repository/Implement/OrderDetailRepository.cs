using EunDeParfum_Repository.DbContexts;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Repository.Implement
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateListOrderDetailAsync(List<OrderDetail> list)
        {
            try
            {
                await _context.OrderDetails.AddRangeAsync(list);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex; // Ném lại lỗi nếu có lỗi
            }
        }

        public async Task<List<OrderDetail>> GetAll()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        public async Task<List<OrderDetail>> GetListOrderDetailAsyncByOrderId(int orderId)
        {
            return await _context.OrderDetails.Where(od => od.OrderId == orderId).ToListAsync();
        }
    }
}
