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

    public class LeistungsuchbegriffController : ControllerBase
    {
        private readonly ILeistungsuchbegriffInterface<DtoLeistunguchbegriff> _genericRepositoryInterface;
        private readonly ILogger<LeistungsuchbegriffController> _logger;

        public LeistungsuchbegriffController(ILeistungsuchbegriffInterface<DtoLeistunguchbegriff> genericRepositoryInterface,
              ILogger<LeistungsuchbegriffController> logger,
            IMemoryCache memoryCache)

        {
            _genericRepositoryInterface = genericRepositoryInterface;
            _logger = logger;
        }


        [HttpGet("{mandant}")]
        public async Task<ActionResult<List<DtoLeistunguchbegriff>>> GetAll(int mandant)
        {
            var result = await _genericRepositoryInterface.GetAll(mandant);
            return Ok(result);
        }


        [HttpGet("{mandant}/{suchbegriff}")]
        public async Task<ActionResult<List<DtoLeistunguchbegriff>>> Getbysuchbegriff(int mandant, string suchbegriff)
        {
            var result = await _genericRepositoryInterface.GetbySuchbegriff(mandant, suchbegriff);
            return Ok(result);
        }
    }

}