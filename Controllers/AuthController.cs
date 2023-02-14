namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        public AuthController(IAuthRepository repository)
        {
            _repository = repository;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var response = await _repository.Register(new User() { Username = request.Username }, request.Password);
            if(!response.Success){
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(UserLoginDto request)
        {
            var response = await _repository.Login(request.Username, request.Password);
            if(!response.Success){
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}