public class Leccion
{
    public int Id { get; set; }
    public Curso Curso { get; set; }
    public string Titulo { get; set; }
    public string ContenidoUrl { get; set; }
    public int DuracionMin { get; set; }
    public string Descripción { get; set; }

    public Leccion(){}

    public Leccion(Curso curso, string titulo, string contenidoUrl, int duracionMin, string descripción)
    {
        Curso = curso;
        Titulo = titulo;
        ContenidoUrl = contenidoUrl;
        DuracionMin = duracionMin;
        Descripción = descripción;

    }
}