namespace Models{
public class CursoCreateDTO
{

    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public string Categoria { get; set; }
    public string Nivel { get; set; }
    public double Precio { get; set; }

    public CursoCreateDTO() { }

    public CursoCreateDTO(string titulo, string descripcion, string categoria, string nivel, double precio)
    {
        Titulo = titulo;
        Descripcion = descripcion;
        Categoria = categoria;
        Nivel = nivel;
        Precio = precio;
    }

    
}
}
