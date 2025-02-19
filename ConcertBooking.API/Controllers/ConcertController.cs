using AutoMapper;
using ConcertBooking.DTO;
using ConcertBooking.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using ConcertBooking.Data.Repository;

namespace ConcertBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConcertController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConcertController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Get all concerts
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var concerts = await _unitOfWork.Concerts.GetAllConcertsAsync();
            return Ok(_mapper.Map<IEnumerable<ConcertDTO>>(concerts));
        }

        // Get a specific concert by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var concert = await _unitOfWork.Concerts.GetConcertAsync(id);
            if (concert == null)
            {
                return NotFound("Concert ID not found.");
            }
            return Ok(_mapper.Map<ConcertDTO>(concert));
        }
    }
}
