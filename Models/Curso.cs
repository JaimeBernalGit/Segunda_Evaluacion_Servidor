namespace Models;
public class Curso
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public string Categoria { get; set; }
    public string Nivel { get; set; }
    public DateTime Fecha_Creacion { get; set; }

    public double Precio { get; set; }

    public Curso() { }

    public Curso(string titulo, string descripcion, string categoria, string nivel, DateTime fecha_Creacion, double precio)
    {
        Titulo = titulo;
        Descripcion = descripcion;
        Categoria = categoria;
        Nivel = nivel;
        Fecha_Creacion = fecha_Creacion;
        Precio = precio;
    }

    
}