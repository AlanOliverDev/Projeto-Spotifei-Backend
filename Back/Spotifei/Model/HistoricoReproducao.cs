using System.Runtime.InteropServices.JavaScript;
using SpotifeiProjeto.SpotifeiProjeto;

namespace Spotifei.Model;

public class HistoricoReproducao
{
    private static int _proximoId = 1;

    public int Id { get; set; }
    public DateTime DataReproducao { get; set; }
    public int DuracaoReproduzida { get; set; }
    public string Status { get; set; }
    public PerfilUsuario Perfil { get; set; }
    public Conteudo Conteudo { get; set; }

    // public HistoricoReproducao(PerfilUsuario perfil, Conteudo conteudo, int duracaoReproduzida)
    // {
    //     Id = _proximoId++;
    //     Perfil = perfil;
    //     Conteudo = conteudo;
    //     DuracaoReproduzida = duracaoReproduzida;
    //     DataReproducao = DateTime.Now;
    //     Status = >= conteudo.Duracao ? "completo" : "parcial";
    // }

    // public void AdicionarRegistro()
    // {
    //     Console.WriteLine($"registro adicionado: {Conteudo.Titulo} - {Status}");
    // }

}

