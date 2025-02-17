using EunDeParfum_Repository.DbContexts;
using EunDeParfum_Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Repository.Implement
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CustomerRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<bool> CreateCustomer(Customer customer)
        {
            try
            {
                await _applicationDbContext.AddAsync(customer);
                await _applicationDbContext.SaveChangesAsync();
                var fullCustomer = await _applicationDbContext.Customers
                    .Include(x => x.Reviews)
                    .Include(x => x.Orders)
                    .FirstOrDefaultAsync(x => x.CustomerId == customer.CustomerId);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteCustomer(Customer customer)
        {
            try
            {
                _applicationDbContext.Customers.Remove(customer);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            try
            {
                return await _applicationDbContext.Customers.ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            try
            {
                return await _applicationDbContext.Customers.FirstOrDefaultAsync(e => e.Email.Equals(email));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            try
            {
                return await _applicationDbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            try
            {
                _applicationDbContext.Customers.Update(customer);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
