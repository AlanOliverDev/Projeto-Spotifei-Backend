
using System;

namespace Spotifei.Model
{
    public abstract class Conteudo
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public string Classificacao { get; set; }
        public TimeSpan Duracao { get; set; }
        public int? ArtistaId { get; set; }
        public string TipoConteudo { get; set; }

        public Artista Artista { get; set; }
        
        protected Conteudo() { }
        
    protected Conteudo(int id, string titulo, string categoria, string classificacao, TimeSpan duracao, int? artistaId, string tipoConteudo)
        {
            Id = id;
            Titulo = titulo;
            Categoria = categoria;
            Classificacao = classificacao;
            Duracao = duracao;
            ArtistaId = artistaId;
            TipoConteudo = tipoConteudo;
        }
}  

        // protected Conteudo(int id, string titulo, string categoria, string classificacao,
        //     int duracao, Artista artista, string tipoConteudo)
        // {
        //     Id = id;
        //     Titulo = titulo;
        //     Categoria = categoria;
        //     Classificacao = classificacao;
        //     Duracao = duracao;
        //     Artista = artista;
        //     TipoConteudo = tipoConteudo;
        // }

        //     public abstract void Reproduzir();

        //     public virtual void ObterDetalhes()
        //     {
        //         Console.WriteLine($"título: {Titulo}");
        //         Console.WriteLine($"artista: {Artista?.Nome}");
        //         Console.WriteLine($"categoria: {Categoria}");
        //         Console.WriteLine($"classificação: {Classificacao}");
        //         Console.WriteLine($"duração: {Duracao} segundos");
        //     }
    }
