namespace dotnet_rpg.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;

        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> Get()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
            return Ok(await _characterService.GetAllCharacters(userId));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> GetSingle(int id)
        {
            var response = await _characterService.GetCharacterById(id);
            if(response.Data is null){
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> AddCharacter(AddCharacterRequestDto newCharacter)
        {
            return Ok(await _characterService.AddCharater(newCharacter));
        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> UpdateCharacter(UpdateCharacterRequestDto updatedCharacter)
        {
            var response = await _characterService.UpdateCharacter(updatedCharacter);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> DeleteCharacter(int id)
        {
            var response = await _characterService.DeleteCharacter(id);
            if(response.Data is null){
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}