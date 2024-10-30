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
    [Route("api/Mitarbeiterdevice")]
    [ApiController]
    [Authorize]  // Require authentication for all actions in this controller
    public class MitarbeiterdeviceController : ControllerBase
    {
        private readonly Imitarbeiterdeviceinterface<Mitarbeiterdevice> _genericRepositoryInterface;

        public MitarbeiterdeviceController(Imitarbeiterdeviceinterface<Mitarbeiterdevice> genericRepositoryInterface)
        {
            _genericRepositoryInterface = genericRepositoryInterface;
        }

        [HttpGet("{Bezeichnung}")]
        public async Task<ActionResult<Projekt>> GetById(string Bezeichnung)
        {
            var result = await _genericRepositoryInterface.GetById(Bezeichnung);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

       
    }
}
