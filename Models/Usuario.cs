public enum EstadoU{Activo, Inactivo}
public class Usuario{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Correo { get; set; }
    public string Direccion { get; set; }
    public DateTime Fecha_Registro { get; set; }
    public EstadoU Estado { get; set; }

    public Usuario(){}

    public Usuario(string nombre, string correo, string direccion, DateTime fecha_Registro, EstadoU estado)
    {
        Nombre = nombre;
        Correo = correo;
        Direccion = direccion;
        Fecha_Registro = fecha_Registro;
        Estado = estado;
    }
}