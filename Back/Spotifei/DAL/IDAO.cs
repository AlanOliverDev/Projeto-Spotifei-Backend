public interface IDAO<T>
{
    public void Adicionar(T objeto);

    public void Atualizar(T objeto);

    public bool Excluir(int id);

    public List<T> Listar();

    public T BuscarPorId(int Id);
}