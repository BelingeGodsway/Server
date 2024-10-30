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
    public class ArtikelController : ControllerBase
    {
        private readonly IArtikelinterface<Artikel> _genericRepositoryInterface;
        private readonly IMemoryCache _memoryCache;
        //private const string CachedAdressenKey = "CachedAdressenKey";
        private readonly ILogger<ArtikelController> _logger;

        public ArtikelController(IArtikelinterface<Artikel> genericRepositoryInterface,
            ILogger<ArtikelController> logger,
            IMemoryCache memoryCache)

        {
            _genericRepositoryInterface = genericRepositoryInterface;
            _memoryCache = memoryCache;
            _logger = logger;
        }
        [HttpGet("{mandant}")]
        public async Task<ActionResult<List<Artikel>>> GetAll(int mandant)
        {
            //if (view == "komplett")
            //{
            var result = await _genericRepositoryInterface.GetAll(mandant);
            return Ok(result);
         }

        [HttpGet("{mandant}/{artikelnummer}")]
        public async Task<ActionResult<Artikel>> GetById(int mandant, string artikelnummer)
        {
            var result = await _genericRepositoryInterface.GetById(mandant, artikelnummer);
            if (result == null)
                return NotFound();
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{mandant}/search/{suchbegriff}")]
        public async Task<ActionResult<List<Artikel>>> GetBySearch(int mandant, string suchbegriff)
        {
            var result = await _genericRepositoryInterface.GetBySearch(mandant, suchbegriff);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
