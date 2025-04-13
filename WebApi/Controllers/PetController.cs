using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using web_api.Models;

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
        public async Task<IActionResult> RegisterPet([FromBody] PetRegistrationRequest request)
        {
            var response = await _petService.RegisterPetWithOwner(request.ToEntity());
            if ((bool)!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}