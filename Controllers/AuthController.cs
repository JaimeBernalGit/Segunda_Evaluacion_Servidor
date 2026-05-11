using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CursosAPI.Services;
using Models;


namespace CursosAPI.Controllers
{
[ApiController]
[Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(LoginDTO loginDTO)
        {
            try
            {
                if (!ModelState.IsValid)  {return BadRequest(ModelState); }

                var token = await _authService.Login(loginDTO);
                return Ok(token);
            }
            catch (KeyNotFoundException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest
                ("Error generating the token: " + ex.Message);
            }
        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register(RegisterDTO registerDTO)
        {
            try
            {
                if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

                var token = await _authService.Register(registerDTO);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest
                ("Error generating the token: " + ex.Message);
            }
        }

    }
}