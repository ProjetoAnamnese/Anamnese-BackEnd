using Anamnese.API.Application.Services.Referral;
using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.PacientModel;
using Microsoft.AspNetCore.Components.Forms;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SendMedicalSpeciality(int pacientId, [FromBody] ReferralRequestModel referralRequest)
        {
            var sendReferral = _referralService.SendPacientReferral(pacientId, referralRequest);
            return Ok(sendReferral);                 
        }        
        [HttpGet("get-referral-by-pacient/{pacientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetReferralByPacient(int pacientId)
        {
            var pacient = _referralService.GetReferralByPacientId(pacientId);
            return Ok(pacient);
        }

        [HttpGet("count-referrals-by-specialty")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CountReferralsBySpecialty()
        {
            var referralCounts = _referralService.CountReferralsBySpecialty();
            return Ok(referralCounts);
        }
    }
}
