﻿using EunDeParfum_Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Repository.Interface
{
    public interface IOrderDetailRepository
    {
        Task<bool> CreateListOrderDetailAsync(List<OrderDetail> list);
        Task<List<OrderDetail>> GetListOrderDetailAsyncByOrderId(int orderId);
        Task<List<OrderDetail>> GetAll();
        Task<OrderDetail> GetOrderDetailByIdAsync(int orderDetailId);
        Task<bool> UpdateOrderDetailAsync(OrderDetail orderDetail);
        Task<bool> DeleteOrderDetailAsync(int orderDetailId);
    }
}
