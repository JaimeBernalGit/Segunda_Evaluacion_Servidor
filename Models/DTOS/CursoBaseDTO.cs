namespace Models{
public class CursoBaseDTO
{

    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public string Categoria { get; set; }
    public string Nivel { get; set; }
    public double Precio { get; set; }
    public IFormFile Doc { get; set; }

    public CursoBaseDTO() { }

        public CursoBaseDTO(string titulo, string descripcion, string categoria, string nivel, double precio, IFormFile doc)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            Categoria = categoria;
            Nivel = nivel;
            Precio = precio;
            Doc = doc;
        }


    }
}
