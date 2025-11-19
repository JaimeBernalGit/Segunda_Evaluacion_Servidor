public enum EstadoI{Activo,Completado,Cancelado}
public class Inscripcion
{
    public int Id { get; set; }
    public Usuario Usuario { get; set; }
    public Curso Curso { get; set; }
    public DateTime FechaInscripcion { get; set; }
    public int ProgresoPorcentaje { get; set; }
    public EstadoI Estado { get; set; }

    public Inscripcion(){}

    public Inscripcion(Usuario usuario, Curso curso, DateTime fechaInscripcion, int progresoPorcentaje, EstadoI estado)
    {
        Usuario = usuario;
        Curso = curso;
        FechaInscripcion = fechaInscripcion;
        ProgresoPorcentaje = progresoPorcentaje;
        Estado = estado;
    }
}