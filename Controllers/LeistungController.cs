using Shared.Project.Entities;
using Shared.Project.Responses;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;
using Server.Data;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;


namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]  // Require authentication for all actions in this controller
    public class LeistungController : ControllerBase
    {
        private readonly ILeistungInterface<Leistungen> _genericRepositoryInterface;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<LeistungController> _logger;

        public LeistungController(ILeistungInterface<Leistungen> genericRepositoryInterface,
            ILogger<LeistungController> logger,
            IMemoryCache memoryCache)

        {
            _genericRepositoryInterface = genericRepositoryInterface;
            _memoryCache = memoryCache;
            _logger = logger;
        }
        [HttpGet("{mandant}")]
        public async Task<ActionResult<List<Leistungen>>> GetAll(int mandant)
        {
            var result = await _genericRepositoryInterface.GetAll(mandant);
            return Ok(result);
        }

        [HttpGet("{mandant}/{leistung}")]
        public async Task<ActionResult<Leistungen>> GetById(int mandant, string leistung)
        {
            var result = await _genericRepositoryInterface.GetById(mandant, leistung);
            if (result == null)
                return NotFound();
            return NotFound();
            return Ok(result);
        }

        [HttpGet("{mandant}/search/{suchbegriff}")]
        public async Task<ActionResult<List<Leistungen>>> GetBySearch(int mandant, string suchbegriff)
        {
            var result = await _genericRepositoryInterface.GetBySearch(mandant, suchbegriff);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
