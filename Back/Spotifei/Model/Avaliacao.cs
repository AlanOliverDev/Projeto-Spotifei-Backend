
using SpotifeiProjeto.SpotifeiProjeto;

namespace Spotifei.Model;

public class Avaliacao
{
    private static int _proximoId = 1;
        
    public int Id { get; set; }
    public int Nota { get; set; }
    public string Comentario { get; set; }
    public DateTime DataAvaliacao { get; set; }
    public PerfilUsuario Autor { get; set; }
    public Conteudo Conteudo { get; set; }

    public Avaliacao(PerfilUsuario autor, Conteudo conteudo, int nota, string comentario)
    {
        Id = _proximoId++;
        Autor = autor;
        Conteudo = conteudo;
        Nota = nota;
        Comentario = comentario;
        DataAvaliacao = DateTime.Now;
    }

    public void AtualizarAvaliacao(int novaNota, string novoComentario)
    {
        Nota = novaNota;
        Comentario = novoComentario;
        Console.WriteLine($"Avaliação atualizada para {novaNota} estrelas");
    }
}
