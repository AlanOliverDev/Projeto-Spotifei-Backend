public class PlanosDAO : IDAO<Plano>
{
    private List<Plano> planos = new List<Plano>();

    public void Adicionar(Plano plano)
    {
        planos.Add(plano);
    }

    public Plano BuscarPorId(int id)
    {
        foreach (var plano in planos)
        {
            if (plano.Id == id)
            {
                return plano;
            }
        }

        Console.WriteLine("Plano não encontrado.");
        return null;
    }

    public bool Excluir(int id)
    {
        var planoExcluir = BuscarPorId(id);
        if (planoExcluir != null)
        {
            planos.Remove(planoExcluir);
            Console.WriteLine("Plano excluído com sucesso!");
            return true;
        }
        else
        {
            Console.WriteLine("Plano não encontrado.");
            return false;
        }
    }

    public void Atualizar(int id, Plano novoPlano)
    {
        var planoExistente = BuscarPorId(id);
        if (planoExistente != null)
        {
            planos.Remove(planoExistente);
            planos.Add(novoPlano);
            Console.WriteLine("Plano atualizado com sucesso!");
        }
        else
        {
            Console.WriteLine("Não foi possível atualizar o plano.");
        }
    }

    public List<Plano> Listar()
    {
        return planos;
    }
}