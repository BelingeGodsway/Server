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

    public class FremdleistungsuchbegriffController : ControllerBase
    {
        private readonly IFremdleistungInterface<DtoFremdleistungSuchbegriff> _genericRepositoryInterface;
        private readonly ILogger<FremdleistungsuchbegriffController> _logger;

        public FremdleistungsuchbegriffController(IFremdleistungInterface<DtoFremdleistungSuchbegriff> genericRepositoryInterface,
              ILogger<FremdleistungsuchbegriffController> logger,
            IMemoryCache memoryCache)

        {
            _genericRepositoryInterface = genericRepositoryInterface;
            _logger = logger;
        }


        [HttpGet("{mandant}")]
        public async Task<ActionResult<List<DtoFremdleistungSuchbegriff>>> GetAll(int mandant)
        {
            var result = await _genericRepositoryInterface.GetAll(mandant);
            return Ok(result);
        }


        [HttpGet("{mandant}/{suchbegriff}")]
        public async Task<ActionResult<List<DtoFremdleistungSuchbegriff>>> Getbysuchbegriff(int mandant, string suchbegriff)
        {
            var result = await _genericRepositoryInterface.GetBySearch(mandant, suchbegriff);
            return Ok(result);
        }
    }

}