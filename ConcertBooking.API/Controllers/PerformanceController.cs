using AutoMapper;
using ConcertBooking.Data.DTO;
using ConcertBooking.Data.Entity;
using ConcertBooking.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using ConcertBooking.Data.Repository;
using ConcertBooking.DTO;

namespace ConcertBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerformanceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PerformanceController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Performance
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var performances = await _unitOfWork.Performances.GetAllPerformancesAsync();
            return Ok(_mapper.Map<IEnumerable<PerformanceDTO>>(performances));
        }

        // GET: api/Performance/concert/{id}
        [HttpGet("concert/{id}")]
        public async Task<IActionResult> GetByConcertId(int id)
        {
            var performances = await _unitOfWork.Performances.GetPerformancesByConcertIdAsync(id);
            if (!performances.Any())
            {
                return NotFound("Performance ID not found.");
            }
            return Ok(_mapper.Map<IEnumerable<PerformanceDTO>>(performances));
        }

        // GET: api/Performance/available/{concertId}/{customerId}
        [HttpGet("available/{concertId}/{customerId}")]
        public async Task<IActionResult> GetAvailablePerformances(int concertId, int customerId)
        {
            var performances = await _unitOfWork.Performances.GetAvailablePerformancesForCustomerAsync(concertId, customerId);
            return Ok(_mapper.Map<IEnumerable<PerformanceDTO>>(performances));
        }

        // GET: api/Performance/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerformanceById(int id)
        {
            var performance = await _unitOfWork.Performances.GetPerformanceByIdAsync(id);
            if (performance == null)
            {
                return NotFound("Performance ID not found.");
            }
            return Ok(_mapper.Map<PerformanceDTO>(performance));
        }
    }
}
