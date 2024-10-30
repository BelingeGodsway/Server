using Shared.Project.Entities;
using Shared.Project.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using Server.Repositories.Contrats;


namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]  // Require authentication for all actions in this controller

    public class ArtikelSuchbegriffController : Controller
    {
        private readonly IArtikelsuchbegriffinterface<DtoArtikelsuchbegriff> _genericRepositoryInterface;
        private readonly ILogger<ArtikelSuchbegriffController> _logger;

        public ArtikelSuchbegriffController(IArtikelsuchbegriffinterface<DtoArtikelsuchbegriff> genericRepositoryInterface,
              ILogger<ArtikelSuchbegriffController> logger,
            IMemoryCache memoryCache)

        {
            _genericRepositoryInterface = genericRepositoryInterface;
            _logger = logger;
        }


        [HttpGet("{mandant}")]
        public async Task<ActionResult<List<DtoArtikelsuchbegriff>>> GetAll(int mandant)
        {
            var result = await _genericRepositoryInterface.GetAll(mandant);
            return Ok(result);
        }


        [HttpGet("{mandant}/{suchbegriff}")]
        public async Task<ActionResult<List<DtoArtikelsuchbegriff>>> Getbysuchbegriff(int mandant, string suchbegriff)
        {
            var result = await _genericRepositoryInterface.GetbySuchbegriff(mandant, suchbegriff);
            return Ok(result);
        }
    }

}