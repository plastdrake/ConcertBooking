using AutoMapper;
using ConcertBooking.Data.Repository;
using ConcertBooking.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ConcertBooking.API.Controllers
{
    public enum ConcertErrorCode
    {
        ConcertIDNotFound,
    }

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
                return NotFound(ConcertErrorCode.ConcertIDNotFound.ToString());
            }

            return Ok(_mapper.Map<ConcertDTO>(concert));
        }
    }
}
