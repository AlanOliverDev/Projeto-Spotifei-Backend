using System.Runtime.InteropServices.JavaScript;

namespace Spotifei.Model;

public class Pagamento
{
    private static int _proximoId = 1;
        
    public int Id { get; set; }
    public DateTime DataPagamento { get; set; }
    public decimal Valor { get; set; }
    public string FormaPagamento { get; set; }
    public string Status { get; set; }
    public Usuario Usuario { get; set; }

    public Pagamento(Usuario usuario, decimal valor, string formaPagamento)
    {
        Id = _proximoId++;
        Usuario = usuario;
        Valor = valor;
        FormaPagamento = formaPagamento;
        DataPagamento = DateTime.Now;
        Status = "pendente";
    }

    public bool ProcessarPagamento()
    {
        Console.WriteLine($"processando pagamento de R${Valor} via {FormaPagamento}...");
        Status = "concluido";
        Console.WriteLine("pagamento aprovado!");
        return true;
    }

    public void GerarRecibo()
    {
        Console.WriteLine("RECIBO:");
        Console.WriteLine($"ID Pagamento: {Id}");
        Console.WriteLine($"Usuário: {Usuario.Nome}");
        Console.WriteLine($"Valor: R${Valor}");
        Console.WriteLine($"Forma: {FormaPagamento}");
        Console.WriteLine($"Data: {DataPagamento}");
        Console.WriteLine($"Status: {Status}");
        Console.WriteLine("==================");
    }
}
