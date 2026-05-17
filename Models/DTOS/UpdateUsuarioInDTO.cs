namespace Models;
public class UpdateUsuarioInDTO
{
    public string Nombre { get; set; }
    public string Nombre_Usuario { get; set; }
    public string Password { get; set; }
    public string Correo { get; set; }
    public string Estado { get; set; }
    public string FotoPerfilUrl { get; set; }

    public UpdateUsuarioInDTO() { }

    public UpdateUsuarioInDTO(string nombre, string nombre_Usuario, string password, string correo, string estado, string fotoPerfilUrl)
    {
        Nombre = nombre;
        Nombre_Usuario = nombre_Usuario;
        Password = password;
        Correo = correo;
        Estado = estado;
        FotoPerfilUrl = fotoPerfilUrl;
    }
}