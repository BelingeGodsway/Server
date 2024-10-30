using Shared.Project.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using Server.Repositories.Contrats;


namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]  // Require authentication for all actions in this controller

    public class AdressenSuchbegriffController : Controller
    {
        private readonly IAdressensuchbegriffInterface<DtoAdressensuchbegriff> _genericRepositoryInterface;
        private readonly ILogger<AdressenSuchbegriffController> _logger;

        public AdressenSuchbegriffController(IAdressensuchbegriffInterface<DtoAdressensuchbegriff> genericRepositoryInterface,
              ILogger<AdressenSuchbegriffController> logger,
            IMemoryCache memoryCache)

        {
            _genericRepositoryInterface = genericRepositoryInterface;
            _logger = logger;
        }


        [HttpGet("{mandant}")]
        public async Task<ActionResult<List<DtoAdressensuchbegriff>>> GetAll(int mandant)
        {
            var result = await _genericRepositoryInterface.GetAll(mandant);
            return Ok(result);
        }


        [HttpGet("{mandant}/{suchbegriff}")]
        public async Task<ActionResult<List<DtoAdressensuchbegriff>>> Getbysuchbegriff(int mandant,string suchbegriff)
        {
            var result = await _genericRepositoryInterface.GetbySuchbegriff(mandant,suchbegriff);
            return Ok(result);
        }
    }

}