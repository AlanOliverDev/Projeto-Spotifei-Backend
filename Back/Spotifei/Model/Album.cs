using System.Runtime.InteropServices.JavaScript;

namespace Spotifei.Model;

public class Album
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public DateTime DataLancamento { get; set; }
    public List<Musica> Musicas { get; set; }

    public Album(int id, string nome, DateTime dataLancamento)
    {
        Id = id;
        Nome = nome;
        DataLancamento = dataLancamento;
        Musicas = new List<Musica>();
    }

    public void AdicionarMusica(Musica musica)
    {
        Musicas.Add(musica);
        Console.WriteLine($"Música '{musica.Titulo}' adicionada ao álbum {Nome}");
    }

    // public int ObterDuracaoTotal()
    // {
    //     int duracaoTotal = 0;
    //     foreach (var musica in Musicas)
    //     {
    //         duracaoTotal += musica.Duracao;
    //     }
    //     Console.WriteLine($"Duração total do álbum {Nome}: {duracaoTotal} segundos");
    //     return duracaoTotal;
    // }
}
