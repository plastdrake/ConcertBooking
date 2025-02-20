using ConcertBooking.MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.MAUI.Services
{
    public class CustomerService : ICustomerService
    {
        IRestService _restService;

        public CustomerService(IRestService service)
        {
            _restService = service;
        }

        public Task<bool> RegisterCustomerAccountAsync(Customer customer)
        {
            return _restService.RegisterCustomerDataAsync(customer);
        }

        public Task<Customer?> CustomerLoginAsync(string email, string password)
        {
            return _restService.LoginAsync(email, password);
        }
    }
}
