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
        if (usuarios.Id == id)
        {
            return usuario;
        }
        else
        {
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
}