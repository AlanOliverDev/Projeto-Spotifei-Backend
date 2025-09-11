using System.Runtime.InteropServices.JavaScript;
using SpotifeiProjeto.SpotifeiProjeto;

namespace Spotifei.Model;

public class Biblioteca
{
    public PerfilUsuario Perfil { get; set; }
    public string Tipo { get; set; }
    public DateTime DataAdicao { get; set; }
    public List<Conteudo> Itens { get; set; }

    public Biblioteca(PerfilUsuario perfil, string tipo)
    {
        Perfil = perfil;
        Tipo = tipo;
        DataAdicao = DateTime.Now;
        Itens = new List<Conteudo>();
    }

    public void AdicionarItem(Conteudo conteudo)
    {
        Itens.Add(conteudo);
        Console.WriteLine($"'{conteudo.Titulo}' adicionado à biblioteca ({Tipo})");
    }

    public void OrganizarPorTipo()
    {
        Console.WriteLine($"organizando biblioteca por tipo ({Tipo}):");
        var musicas = new List<Conteudo>();
        var podcasts = new List<Conteudo>();

        foreach (var item in Itens)
        {
            if (item.TipoConteudo == "musica")
                musicas.Add(item);
            else if (item.TipoConteudo == "podcast")
                podcasts.Add(item);
        }

        Console.WriteLine($"músicas: {musicas.Count}");
        Console.WriteLine($"odcasts: {podcasts.Count}");
    }
}
