using EunDeParfum_Repository.DbContexts;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Repository.Implement
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePaymentAsync(Payment payment)
        {
            try
            {
                await _context.Payments.AddAsync(payment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Payment>> GetAllPaymentsAsync()
        {
            return await _context.Payments
                .Include(p => p.Order)
                .ToListAsync();
        }

        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            return await _context.Payments
                .Include(p => p.Order)
                .FirstOrDefaultAsync(p => p.PaymentId == paymentId);
        }

        public async Task<Payment> GetPaymentByTransactionIdAsync(string transactionId)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(p => p.TransactionId == transactionId);
        }

        // Thay vì truyền string status, ta nên truyền bool?
        public async Task<List<Payment>> GetPaymentsByStatusAsync(string status)
        {
            var query = _context.Payments.AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(p => p.Status == status);
            }

            return await query.Include(p => p.Order).ToListAsync();
        }



        public async Task<bool> UpdatePaymentAsync(Payment payment)
        {
            try
            {
                _context.Payments.Update(payment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
