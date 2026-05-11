using System.Security.Claims;
using Models;

namespace CursosAPI.Services
{
    public interface IAuthService
    {
        public Task<string> Login(LoginDTO loginDTO);
        public Task<string> Register(RegisterDTO registerDTO);
        public string GenerateToken(UserDtoOut userDtoOut);
        public bool HasAccessToResource(int requestedUserID, ClaimsPrincipal user);

    }
}
