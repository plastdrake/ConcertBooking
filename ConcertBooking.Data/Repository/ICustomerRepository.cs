using ConcertBooking.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.Data.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<Customer?> GetCustomerByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
        Task AddCustomerAsync(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);

    }
}
