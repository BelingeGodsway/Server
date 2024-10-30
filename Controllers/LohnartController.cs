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
    public class LohnartController : ControllerBase
    {
        private readonly ILohnartInterface<Lohnarten> _genericRepositoryInterface;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<LohnartController> _logger;

        public LohnartController(ILohnartInterface<Lohnarten> genericRepositoryInterface,
            ILogger<LohnartController> logger,
            IMemoryCache memoryCache)

        {
            _genericRepositoryInterface = genericRepositoryInterface;
            _memoryCache = memoryCache;
            _logger = logger;
        }
        [HttpGet("{mandant}")]
        public async Task<ActionResult<List<Lohnarten>>> GetAll(int mandant)
        {     
            var result = await _genericRepositoryInterface.GetAll(mandant);
            return Ok(result);
        }

        [HttpGet("{mandant}/{lohnart}")]
        public async Task<ActionResult<Lohnarten>> GetById(int mandant, string lohnart)
        {
            var result = await _genericRepositoryInterface.GetById(mandant, lohnart);
            if (result == null)
                return NotFound();
            return NotFound();
            return Ok(result);
        }

        [HttpGet("{mandant}/search/{suchbegriff}")]
        public async Task<ActionResult<List<Lohnarten>>> GetBySearch(int mandant, string suchbegriff)
        {
            var result = await _genericRepositoryInterface.GetBySearch(mandant, suchbegriff);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
