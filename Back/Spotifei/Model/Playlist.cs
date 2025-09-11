using SpotifeiProjeto.SpotifeiProjeto;

namespace Spotifei.Model;

public class Playlist
{
    private static int _proximoId = 1;
        
    public int Id { get; set; }
    public string Nome { get; set; }
    public PerfilUsuario Criador { get; set; }
    public DateTime DataCriacao { get; set; }
    public List<Conteudo> Conteudos { get; set; }
    public bool IsPublica { get; set; }

    public Playlist(string nome, PerfilUsuario criador)
    {
        Id = _proximoId++;
        Nome = nome;
        Criador = criador;
        DataCriacao = DateTime.Now;
        Conteudos = new List<Conteudo>();
        IsPublica = false;
    }

    // public void AdicionarConteudo(Conteudo conteudo)
    // {
    //     Conteudos.Add(conteudo);
    //     Console.WriteLine($"'{conteudo.Titulo}' adicionado à playlist '{Nome}'");
    // }

    // public void RemoverConteudo(Conteudo conteudo)
    // {
    //     if (Conteudos.Remove(conteudo))
    //     {
    //         Console.WriteLine($"'{conteudo.Titulo}' removido da playlist '{Nome}'");
    //     }
    // }

    // public void Reproduzir()
    // {
    //     Console.WriteLine($"reproduzindo playlist: {Nome}");
    //     foreach (var conteudo in Conteudos)
    //     {
    //         conteudo.Reproduzir();
    //     }
    // }

    // public int ObterDuracaoTotal()
    // {
    //     int total = 0;
    //     foreach (var conteudo in Conteudos)
    //     {
    //         total += conteudo.Duracao;
    //     }
    //     return total;
    // }
}
