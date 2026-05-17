namespace Models;

public class UserDtoOut
    {
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Nombre_Usuario { get; set; }
    public string Correo { get; set; }
    public DateTime Fecha_Registro { get; set; }
    public string Estado { get; set; }
    public string Rol { get; set; }
    public string FotoPerfilUrl { get; set; }

    public UserDtoOut(){}
    public UserDtoOut(int id, string nombre, string nombre_Usuario, string correo, DateTime fecha_Registro, string estado, string rol, string fotoPerfilUrl)
    {
        Id = id;
        Nombre = nombre;
        Nombre_Usuario = nombre_Usuario;
        Correo = correo;
        Fecha_Registro = fecha_Registro;
        Estado = estado;
        Rol = rol;
        FotoPerfilUrl = fotoPerfilUrl;
    }
}

