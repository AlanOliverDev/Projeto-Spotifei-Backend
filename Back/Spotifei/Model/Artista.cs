
namespace Spotifei.Model;

public class Artista
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Nacionalidade { get; set; }
    public DateTime DataNascimento { get; set; }
    public List<Conteudo> Obras { get; set; }

    public Artista()
    { 
        
    }

    public Artista(int id, string nome, string nacionalidade, DateTime dataNascimento)
    {
        Id = id;
        Nome = nome;
        Nacionalidade = nacionalidade;
        DataNascimento = dataNascimento;
        Obras = new List<Conteudo>();
    }

    public void AdicionarObra(Conteudo conteudo)
    {
        Obras.Add(conteudo);
        Console.WriteLine($"Obra '{conteudo.Titulo}' adicionada ao artista {Nome}");
    }

    public List<Conteudo> ObterDiscografia()
    {
        Console.WriteLine($"Discografia de {Nome}:");
        foreach (var obra in Obras)
        {
            Console.WriteLine($"- {obra.Titulo} ({obra.TipoConteudo})");
        }
        return Obras;
    }
}
