using Anamnese.API.Application.Services.Referral;
using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.PacientModel;
using Microsoft.AspNetCore.Mvc;

namespace Anamnese.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferralController  : ControllerBase
    {
        private readonly IReferralService _referralService;
        public ReferralController(IReferralService referralService)
        {
            _referralService = referralService;
        }

        [HttpPost("send-referral/{pacientId}")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SendMedicalSpeciality(int pacientId, [FromBody] ReferralRequestModel referralRequest)
        {
            var sendReferral = _referralService.SendPacientReferral(pacientId, referralRequest);
            return Ok(sendReferral);
            //var existingPacient = _pacientService.GetPacientById(pacientId);
            //if (existingPacient != null)
            //{
            //    if (medicalSpeciality == null)
            //    {
            //        return BadRequest("Dados inválidos");
            //    }
            //    var specialityToSend = _pacientService.SendMedicalSpeciality(pacientId, medicalSpeciality);
            //    if (specialityToSend != null)
            //    {
            //        return Ok();
            //    }
            //    else
            //    {
            //        return BadRequest();
            //    }
            //}            
        }
    }
}
