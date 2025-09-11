namespace Spotifei.Model;

public class Podcast : Conteudo
{
    public int IdPodcast { get; set; }
    public string Apresentador { get; set; }
    public string NumeroEpisodio { get; set; }

    public Podcast() { }

    public Podcast(int id, string titulo, string categoria, string classificacao, TimeSpan duracao,
                   int? artistaId, string tipoConteudo, string apresentador, string numeroEpisodio)
        : base(id, titulo, categoria, classificacao, duracao, artistaId, tipoConteudo)
    {
        Apresentador = apresentador;
        NumeroEpisodio = numeroEpisodio;
    }
}

