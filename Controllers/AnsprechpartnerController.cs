using Shared.Project.Entities;
using Shared.Project.Responses;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories.Contracts;
using Microsoft.Extensions.Caching.Memory;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
/*    [Authorize] */ // Require authentication for all actions in this controller
    public class AnsprechpartnerController : ControllerBase
    {
        private readonly iansprechpartnerinterface<Ansprechpartner> _genericRepositoryInterface;
        private readonly IMemoryCache _memoryCache;
        private const string CachedAdressenKey = "CachedAdressenKey";
        private readonly ILogger<AnsprechpartnerController> _logger;

        public AnsprechpartnerController(iansprechpartnerinterface<Ansprechpartner> genericRepositoryInterface,
            ILogger<AnsprechpartnerController> logger,
            IMemoryCache memoryCache)

        {
            _genericRepositoryInterface = genericRepositoryInterface;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        
        [HttpGet("{mandant}")]
        public async Task<ActionResult<List<Ansprechpartner>>> GetAll(int mandant)
        {
           
            var result = await _genericRepositoryInterface.GetAll(mandant);
            return Ok(result);
        }

        [HttpGet("{mandant}/{pos}")]
        public async Task<ActionResult<Ansprechpartner>> GetById(int mandant, int pos)
        {
            var result = await _genericRepositoryInterface.GetById(mandant, pos);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpGet("{mandant}/search/{suchbegriff}")]
        public async Task<ActionResult<List<Ansprechpartner>>> GetBySearch(int mandant, string suchbegriff)
        {
            var result = await _genericRepositoryInterface.GetBySearch(mandant, suchbegriff);
            if (result == null)
                return NotFound();
            return Ok(result);
        }


        [HttpPut]
        public async Task<ActionResult<GeneralResponse>> Update([FromBody] Ansprechpartner item)
        {
            var result = await _genericRepositoryInterface.Update(item);
            if (!result.Flag)
                return NotFound(result);
            return Ok(result);
        }




    }
}


