namespace src.Models;

public class Contrato
{

    public Contrato()
    {
        this.DataCriacao = DateTime.Now;
        this.TokenID = "000000";
        this.Valor = 0;
        this.Pago = false;
    }

    public Contrato(string TokenID, double Valor)
    {
        this.DataCriacao = DateTime.Now;
        this.TokenID = TokenID;
        this.Valor = Valor;
        this.Pago = false;
    }

    public DateTime DataCriacao { get; set; }

    public string TokenID { get; set; }

    public double Valor { get; set; }

    public bool Pago { get; set; }
    
}