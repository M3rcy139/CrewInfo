using Microsoft.AspNetCore.Mvc;
using CrewInfo.Persistence.Interfaces.Repositories;
using CrewInfo.Core.Models;
using CrewInfo.Dto;
using System.Numerics;
using Microsoft.AspNetCore.Routing.Template;

namespace CrewInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PilotController : ControllerBase
    {
        private readonly IPilotRepository _pilotRepository;

        public PilotController(IPilotRepository pilotRepository)
        {
            _pilotRepository = pilotRepository;
        }

        [HttpGet("get-all-pilots")]
        public async Task<IActionResult> GetAllPilots()
        {
            try
            {
                var pilots = await _pilotRepository.GetAllPilots();

                return Ok(pilots);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("get-pilot")]
        public async Task<IActionResult> GetPilot([FromQuery] string? fullName, [FromQuery] string? passportNumber, 
            [FromQuery] string? mobileNumber)
        {
            try
            {
                var pilot = await _pilotRepository.GetPilot(fullName, passportNumber, mobileNumber);

                return Ok(pilot);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("get-pilot-name/{fullName}")]
        public async Task<IActionResult> GetPilotByName(string fullName)
        {
            try
            {
                var pilot = await _pilotRepository.GetPilotByName(fullName);

                return Ok(pilot);
            }
            catch(ArgumentException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("get-pilot-passport/{filter}")]
        public async Task<IActionResult> GetPilotByPassport(string passportNumber)
        {
            try
            {
                var pilot = await _pilotRepository.GetPilotByPassport(passportNumber);

                return Ok(pilot);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("get-pilot-number/{filter}")]
        public async Task<IActionResult> GetPilotByNumber(string mobileNumber)
        {
            try
            {
                var pilot = await _pilotRepository.GetPilotByNumber(mobileNumber);

                return Ok(pilot);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPost("add-pilot")]
        public async Task<IActionResult> AddPilot(PilotRequest request)
        {
            try
            {
                var pilot = new Pilot
                {
                    PilotId = Guid.NewGuid(),
                    FullName = request.FullName,
                    ResidenceAddress = request.ResidenceAddress,
                    MobileNumber = request.MobileNumber,

                    PassportNumber = request.PassportNumber,
                    PassportIssueDate = request.PassportIssueDate,
                    PassportIssuedBy = request.PassportIssuedBy,
                    BirthDate = request.BirthDate,
                    RegistrationAddress = request.RegistrationAddress,

                    InnNumber = request.InnNumber,
                    InsurancePolicyNumber = request.InsurancePolicyNumber,
                    MaritalStatus = request.MaritalStatus,

                    FlightHours = request.FlightHours,
                    Qualification = request.Qualification,
                    LastTrainingLocation = request.LastTrainingLocation,
                    LastTrainingDate = request.LastTrainingDate,

                    CrewNumber = request.CrewNumber,
                };

                await _pilotRepository.AddPilot(pilot);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPut("update-pilot/{pilotId}")]
        public async Task<IActionResult> UpdatePilot(Guid pilotId, [FromBody] PilotRequest request)
        {
            try
            {
                var updatedPilot = new Pilot
                {
                    PilotId = pilotId,
                    FullName = request.FullName,
                    ResidenceAddress = request.ResidenceAddress,
                    MobileNumber = request.MobileNumber,

                    PassportNumber = request.PassportNumber,
                    PassportIssueDate = request.PassportIssueDate,
                    PassportIssuedBy = request.PassportIssuedBy,
                    BirthDate = request.BirthDate,
                    RegistrationAddress = request.RegistrationAddress,

                    InnNumber = request.InnNumber,
                    InsurancePolicyNumber = request.InsurancePolicyNumber,
                    MaritalStatus = request.MaritalStatus,

                    FlightHours = request.FlightHours,
                    Qualification = request.Qualification,
                    LastTrainingLocation = request.LastTrainingLocation,
                    LastTrainingDate = request.LastTrainingDate,

                    CrewNumber = request.CrewNumber,
                };

                await _pilotRepository.UpdatePilot(pilotId, updatedPilot);

                return Ok(new {message = "Пилот успешно обновлен"});
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpDelete("delete-pilot/{pilotId}")]
        public async Task<IActionResult> DeletePilot(Guid pilotId)
        {
            try
            {
                await _pilotRepository.DeletePilot(pilotId);
                return Ok(new { message = "Пилот успешно удален" });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}
