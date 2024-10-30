using Shared.Project.Entities;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]  // Require authentication for all actions in this controller
    public class FremdleistungController : ControllerBase
    {
        private readonly IFremdleistungInterface<Fremdleistung> _genericRepositoryInterface;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<FremdleistungController> _logger;

        public FremdleistungController(IFremdleistungInterface<Fremdleistung> genericRepositoryInterface,
            ILogger<FremdleistungController> logger,
            IMemoryCache memoryCache)

        {
            _genericRepositoryInterface = genericRepositoryInterface;
            _memoryCache = memoryCache;
            _logger = logger;
        }
        [HttpGet("{mandant}")]
        public async Task<ActionResult<List<Fremdleistung>>> GetAll(int mandant)
        {
            var result = await _genericRepositoryInterface.GetAll(mandant);
            return Ok(result);
        }

        [HttpGet("{mandant}/{nummer}")]
        public async Task<ActionResult<Fremdleistung>> GetById(int mandant, string nummer)
        {
            var result = await _genericRepositoryInterface.GetById(mandant, nummer);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{mandant}/search/{suchbegriff}")]
        public async Task<ActionResult<List<Fremdleistung>>> GetBySearch(int mandant, string suchbegriff)
        {
            var result = await _genericRepositoryInterface.GetBySearch(mandant, suchbegriff);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
