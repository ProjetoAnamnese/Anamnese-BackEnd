using Anamnese.API.Application.Services.Anotation;
using Anamnese.API.ORM.Model.Annotation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Anamnese.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AnnotationController : ControllerBase
    {
        private readonly IAnotationService _anotationService;

        public AnnotationController(IAnotationService anotationService)
        {
            _anotationService = anotationService;
        }

        [HttpPost("create-annotation/{pacientId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateAnnotation(int pacientId, [FromBody] AnnotationCreateModel model)
        {
            var annotation = _anotationService.CreateAnotation(pacientId, model);
            return Ok(annotation);
        }


        [HttpGet("get-annotation/{pacientId}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetPacientAnotation(int pacientId)
        {
            var annotation = _anotationService.GetPacientAnotation(pacientId);
            return Ok(annotation);
        }
    }
}
