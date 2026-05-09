namespace Models;
public class Inscripcion
{
    public int Id { get; set; }
    public UserDtoOut? Usuario { get; set; }
    public Curso? Curso { get; set; }
    public DateTime FechaInscripcion { get; set; }
    public int ProgresoPorcentaje { get; set; }
    public string Estado { get; set; }

    public Inscripcion(){}

    public Inscripcion(UserDtoOut usuario, Curso curso, DateTime fechaInscripcion, int progresoPorcentaje, string estado)
    {
        Usuario = usuario;
        Curso = curso;
        FechaInscripcion = fechaInscripcion;
        ProgresoPorcentaje = progresoPorcentaje;
        Estado = estado;
    }
}