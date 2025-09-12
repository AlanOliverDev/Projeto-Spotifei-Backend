public class ArtistaDAO : IDAO<Artista>
{
    private List<Artista> artistas = new List<Artista>();

    public void Adicionar(Artista artista)
    {
        artistas.Add(artista);
    }

    public Artista BuscarPorId(int id)
    {
        foreach (var artista in artistas)
        {
            if (artistas.Id == id)
            {
                return artista;
            }
            Console.WriteLine("Artista não encontrado.");
            return null;
        }
    }

    public bool Excluir(int id)
    {
        var excluirArtista = BuscarPorId(id);
        if (excluirArtista != null)
        {
            artistas.Remove(excluirArtista);
            return true;
        }
        else
        {
            Console.WriteLine("Artista não encontrado.");
            return false;
        }

    }

    public List<Artista> Listar()
    {
        return artistas;
    }

    public void Atualizar(int id, Artista artistaAtualizado)
    {
        var excluirArtista = BuscarPorId(id);
        if (excluirArtista != null)
        {
            artistas.Remove(excluirArtista);
            artistas.Add(artistaAtualizado);

            Console.WriteLine("Artista atualizado com sucesso");

        }
        else
        {
            Console.WriteLine("Não foi possivel atualizar o artista.");
        }
    }
}