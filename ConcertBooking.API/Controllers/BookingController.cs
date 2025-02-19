using AutoMapper;
using ConcertBooking.DTO;
using ConcertBooking.Data.Entity;
using ConcertBooking.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace ConcertBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var bookings = await _unitOfWork.Bookings.GetAllBookingDetailsAsync();
            return Ok(_mapper.Map<IEnumerable<BookingDTO>>(bookings));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListById(int id)
        {
            var booking = await _unitOfWork.Bookings.GetAllBookingDetailsByIdAsync(id);
            if (booking == null || !booking.Any())
            {
                return NotFound("Booking ID not found.");
            }
            return Ok(_mapper.Map<BookingDTO>(booking.First()));
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> ListByCustomerId(int customerId)
        {
            var bookings = await _unitOfWork.Bookings.GetAllBookingsByCustomerIdAsync(customerId);
            if (bookings == null || !bookings.Any())
            {
                return NotFound("No bookings found for this Customer ID.");
            }
            return Ok(_mapper.Map<IEnumerable<BookingDTO>>(bookings));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingCreateDTO bookingDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var customer = await _unitOfWork.Customers.GetByIdAsync(bookingDTO.CustomerId);
                if (customer == null)
                {
                    ModelState.AddModelError("CustomerId", "Invalid Customer ID");
                    return BadRequest(ModelState);
                }

                var performance = await _unitOfWork.Performances.GetByIdAsync(bookingDTO.PerformanceId);
                if (performance == null)
                {
                    ModelState.AddModelError("PerformanceId", "Invalid Performance ID");
                    return BadRequest(ModelState);
                }

                var existingBooking = await _unitOfWork.Bookings.FindByCustomerAndPerformanceAsync(bookingDTO.CustomerId, bookingDTO.PerformanceId);
                if (existingBooking != null)
                {
                    ModelState.AddModelError("Booking", "Booking already exists");
                    return BadRequest(ModelState);
                }

                var booking = new Booking
                {
                    CustomerId = bookingDTO.CustomerId,
                    PerformanceId = bookingDTO.PerformanceId
                };

                await _unitOfWork.Bookings.AddAsync(booking);
                await _unitOfWork.SaveChangesAsync();

                return Ok(_mapper.Map<BookingCreateDTO>(booking));
            }
            catch
            {
                return BadRequest("Could not create booking.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
                if (booking == null)
                {
                    return NotFound("Record not found.");
                }

                _unitOfWork.Bookings.Delete(id);
                await _unitOfWork.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return BadRequest("Could not delete booking.");
            }
        }
    }
}