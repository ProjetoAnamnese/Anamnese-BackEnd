using Anamnese.API.Application.Services.Profissional;
using Anamnese.API.Application.Services.Token;
using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.ProfissionalModel;
using CatalogAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Anamnese.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfissionalController : ControllerBase
    {
        private readonly IProfissionalService _profissionalService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public ProfissionalController(IProfissionalService profissionalService, ITokenService tokenService, IConfiguration configuration)
        {
            _profissionalService = profissionalService;
            _tokenService = tokenService;
            _configuration = configuration;
        }
        [HttpPost("login")]
        //[AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] ProfissionalRequestModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest("Credenciais inválidas");
            }

            bool isAuthenticated = await _profissionalService.ValidateCredentials(loginModel.Email, loginModel.Password);

            if (isAuthenticated)
            {
                ProfissionalModel profissional = await _profissionalService.GetUserByEmailAsync(loginModel.Email);

                var tokenString = _tokenService.GenerateToken(
                    _configuration["Jwt:Key"],
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    profissional
                );

                return Ok(new { token = tokenString, username = profissional.Username });
            }
            else
            {
                return BadRequest("Credenciais inválidas");
            }
        }

        [HttpGet("get-profissional-by-token")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetProfissionaltsById()
        {
            var profissional = _profissionalService.GetProfissionalById();

            if (profissional != null)
            {
                return Ok(profissional);
            }
            else
            {
                return BadRequest("Profissional não encontrado");
            }
        }

        [HttpPost("create-profissional")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateUser([FromBody] CreateProfissionalModel profissionalModel)
        {
            if (profissionalModel == null)
            {
                return BadRequest("Dados do usuário inválidos");
            }
       
            ProfissionalModel createdProfissional = _profissionalService.CreateProfissional(profissionalModel);

            if (createdProfissional != null)
            {
                return Ok(new { message = "Usuário criado com sucesso"});
            }
            else
            {
                return BadRequest("Falha ao criar usuário");
            }
        }
    }
}
