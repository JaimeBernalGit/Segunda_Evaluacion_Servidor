namespace Models;
public class CreateInscripcionDTO
{
    public int UsuarioId { get; set; }
    public int CursoId { get; set; }

    public CreateInscripcionDTO(){}

    public CreateInscripcionDTO(int usuarioId, int cursoId)
    {
        UsuarioId = usuarioId;
        CursoId = cursoId;
    }
}