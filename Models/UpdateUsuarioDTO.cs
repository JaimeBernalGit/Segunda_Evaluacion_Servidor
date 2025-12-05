namespace Models;
public class UpdateUsuarioDTO
{
    public string Nombre { get; set; }
    public string Nombre_Usuario { get; set; }
    public string Password { get; set; }
    public string Correo { get; set; }
    public string Estado { get; set; }

    public UpdateUsuarioDTO() { }

    public UpdateUsuarioDTO(string nombre, string nombre_Usuario, string password, string correo)
    {
        Nombre = nombre;
        Nombre_Usuario = nombre_Usuario;
        Password = password;
        Correo = correo;
    }
}