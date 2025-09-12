using ProjetoSpotifei.DAL;
public class UsuarioDAO : IDAO<Usuario>
{

    private List<Usuario> usuarios = new List<Usuario>();

    public void Adicionar(Usuario objeto)
    {
        usuarios.Add(objeto);
    }

    public Usuario BuscarPorId(int id)
    {
        foreach (var usuario in usuarios)
        {
            if (usuarios.Id == id)
            {
                return usuario;
            }
            return null;
        }
    }

    public bool Excluir(int id)
    {
        var usuario = BuscarPorId(id);
        {
            if (usuario != null)
            {
                usuarios.Remove(usuario);
            }
            else
            {
                return false;
            }
        }
    }

    public List<Usuario> Listar()
    {
        return usuarios;
    }

    public void Atualiza(int id, Usuario novoUsuario)
    {
        var idExcluir = BuscarPorId(id);
        if (idExcluir != null)
        {
            usuarios.Remove(id);
            usuarios.Add(novoUsuario);

            Console.WriteLine("Artista atualizado com sucesso");

        }
        else
        {
            Console.WriteLine("Não foi possivel atualizar o artista.");
        }

    }
}