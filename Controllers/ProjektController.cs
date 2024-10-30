using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories.Contracts;
using Shared.Project.Entities;
using Shared.Project.Responses;


namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProjektController : ControllerBase
    {
        private readonly Iprojektinterface<Projekt> _genericRepositoryInterface;

        public ProjektController(Iprojektinterface<Projekt> genericRepositoryInterface)
        {
            _genericRepositoryInterface = genericRepositoryInterface;
        }

        [HttpGet("{mandant}")]
        public async Task<ActionResult<List<Projekt>>> GetAll(int mandant)
        {
            var result = await _genericRepositoryInterface.GetAll(mandant);
            return Ok(result);
        }

        [HttpGet("{mandant}/{projektnr}")]
        public async Task<ActionResult<Projekt>> GetById(int mandant, string projektnr)
        {
            var result = await _genericRepositoryInterface.GetById(mandant, projektnr);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpGet("{mandant}/search/{suchbegriff}")]
        public async Task<ActionResult<List<Projekt>>> GetBySearch(int mandant, string suchbegriff)
        {
            var result = await _genericRepositoryInterface.GetBySearch(mandant, suchbegriff);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<GeneralResponse>> Update([FromBody] Projekt item)
        {
            var result = await _genericRepositoryInterface.Update(item);
            if (!result.Flag)
                return NotFound(result);
            return Ok(result);
        }

        
    }
}

