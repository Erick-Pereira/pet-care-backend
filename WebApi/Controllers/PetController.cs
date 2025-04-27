using BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterPet([FromBody] Pet request)
        {
            var response = await _petService.RegisterPetWithOwner(request);
            if ((bool)!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}