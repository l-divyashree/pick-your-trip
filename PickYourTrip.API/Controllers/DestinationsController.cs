using Microsoft.AspNetCore.Mvc;
using PickYourTrip.DataAccess.Repositories;
using PickYourTrip.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PickYourTrip.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DestinationsController : ControllerBase
    {
        private readonly DestinationRepository _destinationRepository;

        public DestinationsController(DestinationRepository destinationRepository)
        {
            _destinationRepository = destinationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Destination>>> GetAll()
        {
            var destinations = await _destinationRepository.GetAllAsync();
            return Ok(destinations);
        }
    }
}