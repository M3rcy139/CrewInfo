using Microsoft.AspNetCore.Mvc;
using CrewInfo.Persistence.Interfaces.Repositories;
using CrewInfo.Core.Models;
using CrewInfo.Dto;
using CrewInfo.Persistence.Repositories;

namespace CrewInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StewardController : ControllerBase
    {
        private readonly IStewardRepository _stewardRepository;

        public StewardController(IStewardRepository stewardRepository ) 
        {
            _stewardRepository = stewardRepository;
        }

        [HttpGet("get-all-steward")]
        public async Task<IActionResult> GetAllStewards()
        {
            try
            {
                var stewards = await _stewardRepository.GetAllStewards();

                return Ok(stewards);
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

        [HttpGet("get-steward/{fullName}")]
        public async Task<IActionResult> GetSteward([FromQuery] string? fullName, [FromQuery] string? passportNumber,
            [FromQuery] string? mobileNumber)
        {
            try
            {
                var steward = await _stewardRepository.GetSteward(fullName, passportNumber, mobileNumber);

                return Ok(steward);
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

        [HttpGet("get-steward-name/{fullName}")]
        public async Task<IActionResult> GetStewardByName(string fullName)
        {
            try
            {
                var steward = await _stewardRepository.GetStewardByName(fullName);

                return Ok(steward);
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

        [HttpGet("get-steward-passport/{filter}")]
        public async Task<IActionResult> GetStewardByPassport(string passportNumber)
        {
            try
            {
                var steward = await _stewardRepository.GetStewardByPassport(passportNumber);

                return Ok(steward);
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

        [HttpGet("get-steward-number/{filter}")]
        public async Task<IActionResult> GetStewardByNumber(string mobileNumber)
        {
            try
            {
                var steward = await _stewardRepository.GetStewardByNumber(mobileNumber);

                return Ok(steward);
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

        [HttpPost("add-steward")]
        public async Task<IActionResult> AddSteward(StewardRequest request)
        {
            try
            {
                var steward = new Steward
                {
                    StewardId = Guid.NewGuid(),
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

                    CrewNumber = request.CrewNumber,
                };

                await _stewardRepository.AddSteward(steward);

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

        [HttpPut("update-steward/{stewardId}")]
        public async Task<IActionResult> UpdateSteward(Guid stewardId, StewardRequest request)
        {
            try
            {
                var updatedSteward = new Steward
                {
                    StewardId = stewardId,
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

                    CrewNumber = request.CrewNumber,
                };

                await _stewardRepository.UpdateSteward(stewardId, updatedSteward);

                return Ok(new { message = "Стюарт успешно обновлен" });
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

        [HttpDelete("delete-steward/{stewardId}")]
        public async Task<IActionResult> DeleteSteward(Guid stewardId)
        {
            try
            {
                await _stewardRepository.DeleteSteward(stewardId);
                return Ok(new { message = "Стюарт успешно удален" });
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
