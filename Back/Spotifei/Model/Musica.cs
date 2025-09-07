namespace Spotifei.Model;

public class Musica : Conteudo
{
    public int IdMusica { get; set; }
    public int? AlbumId { get; set; }

    // Relacionamento (opcional, se você tiver uma classe Album)
    public Album Album { get; set; }

        public Musica() { } 

        public Musica(int id, string titulo, string categoria, string classificacao, TimeSpan duracao,
                      int? artistaId, string tipoConteudo, int? albumId)
            : base(id, titulo, categoria, classificacao, duracao, artistaId, tipoConteudo)
        {
                AlbumId = albumId;
        }
}