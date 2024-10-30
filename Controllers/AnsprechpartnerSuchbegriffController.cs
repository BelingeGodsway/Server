using Shared.Project.Entities;
using Shared.Project.Responses;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;
using Server.Data;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Server.Repositories.Contrats;


namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
/*    [Authorize] */ // Require authentication for all actions in this controller

    public class AnsprechpartnerSuchbegriffController : Controller
    {
        private readonly iAnsprechpartnersuchbegriffinterface<DtoAnsprechpartnersuchbegriff> _genericRepositoryInterface;
        private readonly ILogger<AnsprechpartnerSuchbegriffController> _logger;

        public AnsprechpartnerSuchbegriffController(iAnsprechpartnersuchbegriffinterface<DtoAnsprechpartnersuchbegriff> genericRepositoryInterface,
              ILogger<AnsprechpartnerSuchbegriffController> logger,
            IMemoryCache memoryCache)

        {
            _genericRepositoryInterface = genericRepositoryInterface;
            _logger = logger;
        }


        [HttpGet("{mandant}")]
        public async Task<ActionResult<List<DtoAnsprechpartnersuchbegriff>>> GetAll(int mandant)
        {
            var result = await _genericRepositoryInterface.GetAll(mandant);
            return Ok(result);
        }


        [HttpGet("{mandant}/{suchbegriff}")]
        public async Task<ActionResult<List<DtoAnsprechpartnersuchbegriff>>> Getbysuchbegriff(int mandant, string suchbegriff)
        {
            var result = await _genericRepositoryInterface.GetbySuchbegriff(mandant, suchbegriff);
            return Ok(result);
        }
    }

}