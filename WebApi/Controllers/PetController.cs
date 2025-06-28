using AutoMapper;
using BLL.Interfaces;
using Commons.Extensions;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_api.Controllers;
using web_api.Models.Pet;
using web_api.Services;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ControllerBase, IController<Pet>
    {
        private readonly IPetService _petService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public PetController(IPetService petService, IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _petService = petService ?? throw new ArgumentNullException(nameof(petService));
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int skip = 0, [FromQuery] int take = 100, [FromQuery] string? filter = null)
        {
            try
            {
                var response = await _petService.Get(skip, take, filter);
                return response.Success == true
                    ? Ok(new { response.Success, response.Message, response.Data })
                    : BadRequest(new { response.Success, response.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var response = await _petService.Get(id);
                return response.Success == true
                    ? Ok(new { response.Success, response.Message, response.Item })
                    : BadRequest(new { response.Success, response.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Pet request)
        {
            try
            {
                var response = await _petService.Insert(request);
                return response.Success == true
                    ? Ok(new { response.Success, response.Message })
                    : BadRequest(new { response.Success, response.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        [HttpPut("{id}")]
        [Permission("Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Pet request)
        {
            try
            {
                request.Id = id;
                var response = await _petService.Update(request);
                return response.Success == true
                    ? Ok(new { response.Success, response.Message })
                    : BadRequest(new { response.Success, response.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        [HttpDelete("{id}")]
        [Permission("Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var response = await _petService.Delete(id);
                return response.Success == true
                    ? Ok(new { response.Success, response.Message })
                    : BadRequest(new { response.Success, response.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        [HttpPatch("{id}/toggle-active")]
        [Permission("Admin")]
        public async Task<IActionResult> ToggleActive(Guid id)
        {
            try
            {
                var response = await _petService.ToggleActive(id);
                return response.Success == true
                    ? Ok(new
                    {
                        response.Success,
                        response.Message
                    })
                    : BadRequest(new { response.Success, response.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterPet([FromBody] PetRegistrationDTO request)
        {
            try
            {
                Pet pet = _mapper.Map<Pet>(request);
                pet.Owner.CPF = pet.Owner.CPF.StringCleaner();
                pet.Owner.PhoneNumber = pet.Owner.PhoneNumber.StringCleaner();
                pet.Owner.Address.ZipCode = pet.Owner.Address.ZipCode.StringCleaner();
                var response = await _petService.RegisterPetWithOwner(pet);
                return response.Success == true
                    ? Ok(new { response.Success, response.Message })
                    : BadRequest(new { response.Success, response.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Success = false, Message = "Internal server error", e });
            }
        }

        [HttpPost("registerPet")]
        [Authorize]
        public async Task<IActionResult> RegisterPet([FromBody] PetWithoutOwnerRegistrationDTO request)
        {
            try
            {
                var email = User.GetEmailFromToken();

                if (string.IsNullOrEmpty(email))
                {
                    return Unauthorized(new { Success = false, Message = "Email not found in token." });
                }

                var userResponse = await _userService.GetByEmail(email);
                if (!(userResponse.Success ?? false) || userResponse.Item == null)
                {
                    return NotFound(new { Success = false, Message = "User not found." });
                }

                Pet pet = _mapper.Map<Pet>(request);
                pet.OwnerId = userResponse.Item.Id;

                var response = await _petService.Insert(pet);
                return response.Success == true
                    ? Ok(new { response.Success, response.Message })
                    : BadRequest(new { response.Success, response.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Success = false, e.Message });
            }
        }
    }
}