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

        public async Task<bool> DeleteOrderDetailAsync(int orderDetailId)
        {
            try
            {
                var orderDetail = await _context.OrderDetails
                    .FirstOrDefaultAsync(od => od.OrderDetailId == orderDetailId);

                if (orderDetail == null)
                {
                    throw new Exception($"Không tìm thấy chi tiết đơn hàng với OrderDetailId {orderDetailId} để xóa");
                }

                _context.OrderDetails.Remove(orderDetail);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa chi tiết đơn hàng với OrderDetailId {orderDetailId}: " + ex.Message, ex);
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

        public async Task<OrderDetail> GetOrderDetailByIdAsync(int orderDetailId)
        {
            try
            {
                var orderDetail = await _context.OrderDetails
                    .FirstOrDefaultAsync(od => od.OrderDetailId == orderDetailId);

                if (orderDetail == null)
                {
                    throw new Exception($"Không tìm thấy chi tiết đơn hàng với OrderDetailId {orderDetailId}");
                }

                return orderDetail;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy chi tiết đơn hàng với OrderDetailId {orderDetailId}: " + ex.Message, ex);
            }
        }

        public async Task<bool> UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            try
            {
                var existingOrderDetail = await _context.OrderDetails
                    .FirstOrDefaultAsync(od => od.OrderDetailId == orderDetail.OrderDetailId);

                if (existingOrderDetail == null)
                {
                    throw new Exception($"Không tìm thấy chi tiết đơn hàng với OrderDetailId {orderDetail.OrderDetailId} để cập nhật");
                }

                // Cập nhật các thuộc tính
                existingOrderDetail.Quantity = orderDetail.Quantity;
                existingOrderDetail.UnitPrice = orderDetail.UnitPrice;
                existingOrderDetail.ProductId = orderDetail.ProductId;
                existingOrderDetail.OrderId = orderDetail.OrderId;

                _context.OrderDetails.Update(existingOrderDetail);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật chi tiết đơn hàng với OrderDetailId {orderDetail.OrderDetailId}: " + ex.Message, ex);
            }
        }
    }
}
