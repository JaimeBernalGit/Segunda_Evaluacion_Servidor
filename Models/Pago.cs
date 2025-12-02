public class Pago{

    public int Id { get; set; }
    public Usuario Usuario { get; set; }
    public Curso Curso { get; set; }
    public int Cantidad { get; set; }
    public string Holder_Name { get; set; }
    public string Holder_Number { get; set; }
    public int CVV { get; set; }

    public Pago(){}

    public Pago(int id, Usuario usuario, Curso curso, int cantidad, string holder_Name, string holder_Number, int cVV)
    {
        Id = id;
        Usuario = usuario;
        Curso = curso;
        Cantidad = cantidad;
        Holder_Name = holder_Name;
        Holder_Number = holder_Number;
        CVV = cVV;
    }
}