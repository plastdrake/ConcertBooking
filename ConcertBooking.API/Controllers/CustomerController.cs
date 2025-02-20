using AutoMapper;
using ConcertBooking.Data.Repository;
using ConcertBooking.DTO;
using ConcertBooking.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ConcertBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // POST: api/Customer/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCustomerDTO registerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Check if email already exists
                if (await _unitOfWork.Customers.EmailExistsAsync(registerDTO.Email))
                {
                    return BadRequest("Email is already in use.");
                }

                // Map DTO to Entity
                var customer = _mapper.Map<Customer>(registerDTO);

                // Create customer
                await _unitOfWork.Customers.AddCustomerAsync(customer);
                await _unitOfWork.SaveChangesAsync();

                // Return the created customer
                var customerDTO = _mapper.Map<CustomerDTO>(customer);
                return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customerDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Could not register customer", Message = ex.Message });
            }
        }

        // POST: api/Customer/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Get customer by email
                var customer = await _unitOfWork.Customers.GetCustomerByEmailAsync(loginDTO.Email);

                if (customer == null || customer.Password != loginDTO.Password) // Enkel lösenordskontroll
                {
                    return Unauthorized("Invalid email or password.");
                }

                // Create and return customer DTO
                var customerDTO = _mapper.Map<CustomerDTO>(customer);
                return Ok(customerDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Could not log in", Message = ex.Message });
            }
        }

        // GET: api/Customer/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                Debug.WriteLine($"Received CustomerId: {id}");

                var customer = await _unitOfWork.Customers.GetCustomerByIdAsync(id);
                if (customer == null)
                {
                    Debug.WriteLine("Customer not found.");
                    return NotFound();
                }

                var customerDTOResult = _mapper.Map<CustomerDTO>(customer);
                Debug.WriteLine($"Returning CustomerDTO: {customerDTOResult.CustomerID}");
                return Ok(customerDTOResult);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in GetCustomerById: {ex.Message}");
                return StatusCode(500, new { Error = "Could not retrieve customer", Message = ex.Message });
            }
        }



        // POST: api/Customer/getBookings
        [HttpPost("getBookings")]
        public async Task<IActionResult> GetCustomerBookings([FromBody] CustomerDTO customerDTO)
        {
            if (customerDTO == null || customerDTO.CustomerID <= 0)
            {
                return BadRequest("Invalid customer data.");
            }

            try
            {
                var customer = await _unitOfWork.Customers.GetCustomerByIdAsync(customerDTO.CustomerID);
                if (customer == null)
                    return NotFound();

                var bookings = await _unitOfWork.Bookings.GetAllBookingsByCustomerIdAsync(customerDTO.CustomerID);
                var bookingDTOs = _mapper.Map<IEnumerable<BookingDTO>>(bookings);
                return Ok(bookingDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Could not retrieve customer bookings", Message = ex.Message });
            }
        }


        // PUT: Update customer
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerDTO updateDTO)
        {
            try
            {

                var customer = await _unitOfWork.Customers.GetCustomerByIdAsync(updateDTO.Id);
                if (customer == null)
                    return NotFound();

                // Update customer
                _mapper.Map(updateDTO, customer);
                _unitOfWork.Customers.UpdateCustomer(customer);
                await _unitOfWork.SaveChangesAsync();

                return Ok("Customer updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Could not update profile", Message = ex.Message });
            }
        }
    }
}
