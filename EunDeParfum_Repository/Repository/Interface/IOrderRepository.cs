using EunDeParfum_Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Repository.Interface
{
    public interface IOrderRepository
    {
        Task<bool> CreateOrderAsync(Order order);
        Task<bool> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int orderId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order> GetCartOrderByCustomerIdAsync(int customerId);
        Task<List<Order>> GetOrdersByCustomerIdAsync(int customerId);
    }
}
