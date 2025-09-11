using System.Collections.Generic;

namespace Spotifei.Model
{

    namespace SpotifeiProjeto
    {
        public class PerfilUsuario
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public Usuario Usuario { get; set; }
            public List<Conteudo> HistoricoReproducao { get; set; }
            public List<Playlist> Playlists { get; set; }

            public PerfilUsuario(int id, string nome, Usuario usuario)
            {
                Id = id;
                Nome = nome;
                Usuario = usuario;
                HistoricoReproducao = new List<Conteudo>();
                Playlists = new List<Playlist>();
            }

            public void CriarPlaylist(string nomePlaylist)
            {
                var playlist = new Playlist(nomePlaylist, this);
                Playlists.Add(playlist);
                Console.WriteLine($"playlist '{nomePlaylist}' criada com sucesso!");
            }

            public void AdicionarConteudoHistorico(Conteudo conteudo)
            {
                HistoricoReproducao.Add(conteudo);
            }
        }
    }
}