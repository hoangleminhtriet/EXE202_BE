using EunDeParfum_Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Repository.Interface
{
    public interface ICustomerRepository
    {
        public Task<bool> CreateCustomer(Customer customer);
        public Task<bool> UpdateCustomer(Customer customer);
        public Task<bool> DeleteCustomer(Customer customer);
        public Task<List<Customer>> GetAllCustomers();
        public Task<Customer> GetCustomerById(int id);
        public Task<Customer> GetCustomerByEmail(string email);
    }
}
