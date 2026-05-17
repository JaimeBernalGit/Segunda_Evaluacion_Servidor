public class UserInDto
{
    public string Nombre { get; set; }
    public string Nombre_Usuario { get; set; }
    public string Password { get; set; }
    public string Correo { get; set; }
    public string Rol { get; set; }
    public string FotoPerfilUrl { get; set; }

    public UserInDto() { }

    public UserInDto(string nombre, string nombre_Usuario, string password, string correo, string rol, string fotoPerfilUrl)
    {
        Nombre = nombre;
        Nombre_Usuario = nombre_Usuario;
        Password = password;
        Correo = correo;
        Rol = rol;
        FotoPerfilUrl = fotoPerfilUrl;
    }
}