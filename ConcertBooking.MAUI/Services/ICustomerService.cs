using ConcertBooking.MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.MAUI.Services
{
    public interface ICustomerService
    {
        Task<bool> RegisterCustomerAccountAsync(Customer customer);
        Task<Customer> CustomerLoginAsync(string email, string password);
    }
}
