using Advanced_CSharp.DTO.Requests.Authentication;
using Advanced_CSharp.DTO.Responses.Authentication;
using Advanced_CSharp.Service.Helper;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Advanced_CSharp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IloggingService _loggingService;
        public AuthenticationController(IAuthenticationService authenticationService, IloggingService loggingService)
        {
            _authenticationService = authenticationService;
            _loggingService = loggingService;
        }
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromQuery] AuthenticationLoginRequest request)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                AuthenticationLoginResponse loginResponse = await _authenticationService.ValidateUser(request);
                return loginResponse.BaseResponse.Success ? Ok(loginResponse) : BadRequest(loginResponse.BaseResponse.Message);

            }
            catch (Exception ex)
            {
                _loggingService.LogError(ex);
                return StatusCode(500, ex.Message);

            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromQuery] AuthenticationRegisterRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                AuthenticationRegisterResponse RegisterResponse = await _authenticationService.RegisterUser(request);
                return RegisterResponse.BaseResponse.Success ?
                     Ok(RegisterResponse) : BadRequest(RegisterResponse.BaseResponse.Message);

            }
            catch (Exception ex)
            {
                _loggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }

        }
    }
}
