using Anamnese.API.Application.Services.Profissional;
using Anamnese.API.Application.Services.ProfissionalAvailable;
using Anamnese.API.Application.Services.Token;
using Anamnese.API.ORM.Model.ProfissionalModel;
using CatalogAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Anamnese.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfissionalAvailableController : ControllerBase
    {
        private readonly IProfissionalService _profissionalService;
        private readonly IProfissionalAvailableService _profissionalAvailableService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public ProfissionalAvailableController(IProfissionalService profissionalService, ITokenService tokenService, IConfiguration configuration, IProfissionalAvailableService profissionalAvailableService)
        {
            _profissionalAvailableService = profissionalAvailableService;
            _profissionalService = profissionalService;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        [HttpGet("profissional-available/{profissionalId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProfissionalAvailabilities(int profissionalId)
        {
            if (profissionalId <= 0)
                return BadRequest();

            var availabilities = _profissionalAvailableService.GetProfissionalAvailabilities(profissionalId);

            if (availabilities == null || !availabilities.Any())
                return NotFound();

            return Ok(availabilities);
        }


        [HttpPost("create-profissional-available/{profissionalId}")]
        //[AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Available(int profissionalId,[FromBody] ProfissionalAvailableRequest availableRequest)
        {
            if(availableRequest == null)
            {
                return BadRequest();
            }
            var sucess = _profissionalAvailableService.SetProfissionalAvailability(profissionalId, availableRequest);
            if (sucess) return Ok();
            return BadRequest();
        }
    }
}
