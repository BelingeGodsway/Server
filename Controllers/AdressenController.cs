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
    /*[Authorize] */ // Require authentication for all actions in this controller
    public class AdressenController : ControllerBase
    {
        private readonly Iadresseninterface<Adressen> _genericRepositoryInterface;
        private readonly IMemoryCache _memoryCache;
        private const string CachedAdressenKey = "CachedAdressenKey";
        private readonly ILogger<AdressenController> _logger;
        private readonly IConfiguration configuration;
        private bool enablePrivacyMode;

        public AdressenController(Iadresseninterface<Adressen> genericRepositoryInterface,
            ILogger<AdressenController> logger,
            IMemoryCache memoryCache)

        {
            _genericRepositoryInterface = genericRepositoryInterface;
            _memoryCache = memoryCache;
            _logger = logger;
         
        }

        [HttpGet("{mandant}")]
        public async Task<ActionResult<List<Adressen>>> GetAll(int mandant)
        {
            
                var result = await _genericRepositoryInterface.GetAll(mandant);
                return Ok(result);
  
        }
             
        [HttpGet("{mandant}/{adresse}")]
        public async Task<ActionResult<Adressen>> GetById(int mandant, string adresse)
        {
            var result = await _genericRepositoryInterface.GetById(mandant, adresse);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{mandant}/search/{suchbegriff}")]
        public async Task<ActionResult<List<Adressen>>> GetBySearch(int mandant, string suchbegriff)
        {
            var result = await _genericRepositoryInterface.GetBySearch(mandant, suchbegriff);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<GeneralResponse>> Update([FromBody] Adressen item)
        {
            var result = await _genericRepositoryInterface.Update(item);
            if (!result.Flag)
                return NotFound(result);
            return Ok(result);
        }
       

    }
}


