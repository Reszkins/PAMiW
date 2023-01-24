using InpostApp.DataAccess.Services;
using InpostApp.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InpostApp.Controllers
{
    [ApiController]
    [Route("api/parcels")]
    public class ParcelController : ControllerBase
    {
        private readonly IParcelService _parcelService;
        public ParcelController(IParcelService parcelService) 
        { 
            _parcelService = parcelService;
        }

        [HttpGet]
        [Route("getuserparcels")]
        [Authorize]
        public async Task<IActionResult> GetUserParcels()
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier) is null) return BadRequest("Invalid user");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var parcels = await _parcelService.GetUserParcels(userId);

            return Ok(parcels);
        }

        [HttpGet]
        [Route("getparcellockers")]
        [Authorize]
        public async Task<IActionResult> GetParcelLockers()
        {
            var parcelLockers = await _parcelService.GetParcelLockers();

            return Ok(parcelLockers);
        }

        [HttpGet]
        [Route("getusers")]
        [Authorize]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _parcelService.GetUsers();

            return Ok(users);
        }

        [HttpPost]
        [Route("sendparcel")]
        [Authorize]
        public async Task<IActionResult> SendParcel([FromBody] SendParcelDto dto)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier) is null) return BadRequest("Invalid user");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await _parcelService.AddParcel(dto, userId);

            return Ok();
        }
    }
}
