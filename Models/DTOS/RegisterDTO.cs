namespace Models;

public class RegisterDTO
{

    public string Nombre { get; set; }
    public string Nombre_Usuario { get; set; }
    public string Password { get; set; }
    public string Correo { get; set; }
    public IFormFile FotoPerfil{ get; set; }

    public RegisterDTO(){}
    public RegisterDTO(string nombre, string nombre_Usuario, string password, string correo, IFormFile fotoPerfil)
    {
        Nombre = nombre;
        Nombre_Usuario = nombre_Usuario;
        Password = password;
        Correo = correo;
        FotoPerfil = fotoPerfil;
    }

}

