namespace Models;
public class Pago{

    public int Id { get; set; }
    public Usuario Usuario { get; set; }
    public Curso Curso { get; set; }
    public int Cantidad { get; set; }
    public string Nombre_Titular{ get; set; }
    public string Numero_Titular { get; set; }
    public int CVV { get; set; }

    public Pago(){}

    public Pago(int id, Usuario usuario, Curso curso, int cantidad, string nombre_Titular, string numero_Titular, int cVV)
    {
        Id = id;
        Usuario = usuario;
        Curso = curso;
        Cantidad = cantidad;
        Nombre_Titular = nombre_Titular;
        Numero_Titular = numero_Titular;
        CVV = cVV;
    }
}