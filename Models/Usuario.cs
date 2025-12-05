public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Nombre_Usuario { get; set; }
    public string Password { get; set; }
    public string Correo { get; set; }
    public DateTime Fecha_Registro { get; set; }
    public string Estado { get; set; }

    public Usuario() { }

    public Usuario(string nombre, string nombre_Usuario, string password, string correo, DateTime fecha_Registro, string estado)
    {
        Nombre = nombre;
        Nombre_Usuario = nombre_Usuario;
        Password = password;
        Correo = correo;
        Fecha_Registro = fecha_Registro;
        Estado = estado;
    }

}