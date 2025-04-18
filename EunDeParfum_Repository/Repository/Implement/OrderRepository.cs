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
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateOrderAsync(Order order)
        {
            try
            {
                _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            try
            {
                var order = await _context.Orders.FindAsync(orderId);
                if (order == null)
                {
                    return false;
                }
                _context.Orders.Remove(order);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.Where(o => o.IsDeleted == false).ToListAsync();
        }

        public async Task<Order> GetCartOrderByCustomerIdAsync(int customerId)
        {
            try
            {
                return await _context.Orders
                    .FirstOrDefaultAsync(o => o.CustomerId == customerId && o.Status == "Cart" && !o.IsDeleted);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy giỏ hàng của khách hàng {customerId}: {ex.Message}", ex);
            }
        }

        public Task<Order> GetOrderByIdAsync(int orderId)
        {
            try
            {
                return _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            try
            {
                return await _context.Orders
                    .Where(o => o.CustomerId == customerId && o.IsDeleted == false)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách đơn hàng của khách hàng {customerId}: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            try
            {
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
