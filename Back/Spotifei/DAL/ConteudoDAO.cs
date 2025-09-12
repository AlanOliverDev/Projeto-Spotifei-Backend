public class ConteudoDAO : IDAO<Conteudo>
{
    private List<Conteudo> musicas = new List<Conteudo>();
    private List<Conteudo> podcasts = new List<Conteudo>();

    public void Adicionar(Conteudo conteudo)
    {
        if (conteudo.Tipo == "Musica")
        {
            musicas.Add(conteudo);
        }
        else if (conteudo.Tipo == "Podcast")
        {
            podcasts.Add(conteudo);
        }
    }

    public Conteudo BuscarPorId(int id)
    {
        foreach (var musica in musicas)
        {
            if (musica.Id == id)
            {
                return musica;
            }
        }

        foreach (var podcast in podcasts)
        {
            if (podcast.Id == id)
            {
                return podcast;
            }
        }

        Console.WriteLine("O conteúdo não foi encontrado.");
        return null;
    }

    public bool Excluir(int id)
    {
        var conteudoExcluir = BuscarPorId(id);
        if (conteudoExcluir != null)
        {
            if (conteudoExcluir.Tipo == "Musica")
            {
                musicas.Remove(conteudoExcluir);
            }
            else if (conteudoExcluir.Tipo == "Podcast")
            {
                podcasts.Remove(conteudoExcluir);
            }

            Console.WriteLine("Conteúdo excluído com sucesso!");
            return true;
        }
        else
        {
            Console.WriteLine("Conteúdo não encontrado.");
            return false;
        }
    }

    public void Atualizar(int id, Conteudo novoConteudo)
    {
        var conteudoExistente = BuscarPorId(id);
        if (conteudoExistente != null)
        {
            if (conteudoExistente.Tipo == "Musica")
            {
                musicas.Remove(conteudoExistente);
                musicas.Add(novoConteudo);
            }
            else if (conteudoExistente.Tipo == "Podcast")
            {
                podcasts.Remove(conteudoExistente);
                podcasts.Add(novoConteudo);
            }

            Console.WriteLine("Conteúdo atualizado com sucesso!");
        }
        else
        {
            Console.WriteLine("Conteúdo não encontrado.");
        }
    }

    public List<Conteudo> Listar()
    {
        List<Conteudo> todos = new List<Conteudo>();
        todos.AddRange(musicas);
        todos.AddRange(podcasts);
        return todos;
    }
}