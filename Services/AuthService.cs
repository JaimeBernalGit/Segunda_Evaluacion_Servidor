using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using CursosAPI.Repositories;

namespace CursosAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepository _repository;

        public AuthService(IConfiguration configuration, IUsuarioRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
        }

        public async Task<string?> Login(LoginDTO loginDTO) {
            var usuario = await _repository.GetUserFromCredentials(loginDTO);
            if (usuario == null){
                throw new UnauthorizedAccessException("Credenciales inválidas");
            }
            return GenerateToken(usuario);
        }

        public async Task<string> Register(RegisterDTO registerDTO) {
            var usuario = await _repository.AddUserFromCredentials(registerDTO);
            return GenerateToken(usuario);
        }

        public string GenerateToken(UserDtoOut userDtoOut) {
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]); 
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                Subject = new ClaimsIdentity(new Claim[] 
                    {
                        new Claim(ClaimTypes.NameIdentifier, Convert.ToString(userDtoOut.Id)),
                        new Claim(ClaimTypes.Name, userDtoOut.Nombre_Usuario),
                        new Claim(ClaimTypes.Role, userDtoOut.Rol),
                        new Claim(ClaimTypes.Email, userDtoOut.Correo),
                    }),
                Expires = DateTime.UtcNow.AddDays(7), // AddMinutes(60)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        } 
        public bool HasAccessToResource(int requestedUserID, ClaimsPrincipal user) 
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !int.TryParse(userIdClaim.Value, out int userId)) 
            { 
                return false; 
            }
            var isOwnResource = userId == requestedUserID;

            var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim == null) return false;
            var isAdmin = roleClaim!.Value == Roles.Admin;
            
            var hasAccess = isOwnResource || isAdmin;
            return hasAccess;
        }
     

    }
}
