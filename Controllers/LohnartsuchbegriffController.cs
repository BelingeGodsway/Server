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

    public class LohnartsuchbegriffController : ControllerBase
    {
        private readonly ILohnartsuchbegriffInterface<DtoLohnartSuchbegriff> _genericRepositoryInterface;
        private readonly ILogger<LohnartsuchbegriffController> _logger;

        public LohnartsuchbegriffController(ILohnartsuchbegriffInterface<DtoLohnartSuchbegriff> genericRepositoryInterface,
              ILogger<LohnartsuchbegriffController> logger,
            IMemoryCache memoryCache)

        {
            _genericRepositoryInterface = genericRepositoryInterface;
            _logger = logger;
        }


        [HttpGet("{mandant}")]
        public async Task<ActionResult<List<DtoLohnartSuchbegriff>>> GetAll(int mandant)
        {
            var result = await _genericRepositoryInterface.GetAll(mandant);
            return Ok(result);
        }


        [HttpGet("{mandant}/{suchbegriff}")]
        public async Task<ActionResult<List<DtoLohnartSuchbegriff>>> Getbysuchbegriff(int mandant, string suchbegriff)
        {
            var result = await _genericRepositoryInterface.GetbySuchbegriff(mandant, suchbegriff);
            return Ok(result);
        }
    }

}