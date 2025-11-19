public enum Nivel{Basico,Intermedio,Avanzado}
public class Curso{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descripccion { get; set; }
    public string Categoria { get; set; }
    public Nivel Nivel { get; set; }
    public DateTime Fecha_Creacion { get; set; }

    public Curso(){}

    public Curso(string titulo, string descripccion, string categoria, Nivel nivel, DateTime fecha_Creacion)
    {
        Titulo = titulo;
        Descripccion = descripccion;
        Categoria = categoria;
        Nivel = nivel;
        Fecha_Creacion = fecha_Creacion;
    }
}