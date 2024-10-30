using Shared.Project.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Server.Repositories.Contrats;


namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  /*  [Authorize]*/  // Require authentication for all actions in this controller

    public class ProjektSuchbegriffController : Controller
    {
        private readonly iprojektsuchbegriffinterface<DtoProjektsuchbegriff> _genericRepositoryInterface;
        private readonly ILogger<ProjektSuchbegriffController> _logger;

        public ProjektSuchbegriffController(iprojektsuchbegriffinterface<DtoProjektsuchbegriff> genericRepositoryInterface,
              ILogger<ProjektSuchbegriffController> logger,
            IMemoryCache memoryCache)

        {
            _genericRepositoryInterface = genericRepositoryInterface;
            _logger = logger;
        }


        [HttpGet("{mandant}")]
        public async Task<ActionResult<List<DtoProjektsuchbegriff>>> GetAll(int mandant)
        {
            var result = await _genericRepositoryInterface.GetAll(mandant);
            return Ok(result);
        }


        [HttpGet("{mandant}/{suchbegriff}")]
        public async Task<ActionResult<List<DtoProjektsuchbegriff>>> Getbysuchbegriff(int mandant, string suchbegriff)
        {
            var result = await _genericRepositoryInterface.GetbySuchbegriff(mandant, suchbegriff);
            return Ok(result);
        }
    }

}