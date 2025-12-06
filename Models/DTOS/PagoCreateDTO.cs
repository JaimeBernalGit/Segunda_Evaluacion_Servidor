namespace Models;
public class PagoCreateDTO{

    public int UsuarioId { get; set; }
    public int CursoId { get; set; }
    public double Cantidad { get; set; }
    public string Nombre_Titular{ get; set; }
    public string Numero_Titular { get; set; }
    public string CVV { get; set; }

    public PagoCreateDTO(){}

    public PagoCreateDTO( int usuarioId, int cursoId, double cantidad, string nombre_Titular, string numero_Titular, string cVV)
    {
        UsuarioId = usuarioId;
        CursoId = cursoId;
        Cantidad = cantidad;
        Nombre_Titular = nombre_Titular;
        Numero_Titular = numero_Titular;
        CVV = cVV;
    }
}