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
        private readonly IUploadDocService _uploadDocService;
        private readonly IFactService _factService;

        public AuthService(IConfiguration configuration, IUsuarioRepository repository, IUploadDocService uploadDocService, IFactService factService)
        {
            _configuration = configuration;
            _repository = repository;
            _uploadDocService = uploadDocService;
            _factService = factService;
        }

        public async Task<LoginResponseDTO> Login(LoginDTO loginDTO) {
            var usuario = await _repository.GetUserFromCredentials(loginDTO);
            if (usuario == null){
                throw new UnauthorizedAccessException("Credenciales inválidas");
            }
            var token = GenerateToken(usuario);
            var fact = await _factService.GetRandomFactAsync();
            return new LoginResponseDTO { Token = token, Fact = fact };
        }

        public async Task<string> Register(RegisterDTO registerDTO) {

            var fotoPerfilUrl = await _uploadDocService.UploadImageAsync(registerDTO.FotoPerfil);

            var usuarioIn = new UserInDto
            {
                FotoPerfilUrl = fotoPerfilUrl,
                Nombre = registerDTO.Nombre,
                Nombre_Usuario = registerDTO.Nombre_Usuario,
                Password = registerDTO.Password,
                Correo = registerDTO.Correo,
                Rol = Roles.User
            };
            var usuario = await _repository.AddUserFromCredentials(usuarioIn);
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
         public bool HasAccessToOwnResource(int requestedUserID, ClaimsPrincipal user) 
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !int.TryParse(userIdClaim.Value, out int userId)) 
            { 
                return false; 
            }
            var isOwnResource = userId == requestedUserID;
      
            var hasAccess = isOwnResource;
            return hasAccess;
        }
     

    }
}
