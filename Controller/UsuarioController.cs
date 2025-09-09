using System;
using Spotifei.Model;
using Spotifei.DAL;

namespace Spotifei.Controller
{
    public class UsuarioController
    {
        private readonly UsuarioRepository _usuarioRepo = new UsuarioRepository();

        public void CadastrarUsuario(Usuario usuario)
        {
            _usuarioRepo.Adicionar(usuario);
            Console.WriteLine(" usuario cadastrado com sucesso!");
        }

        public List<Usuario> ListarUsuarios()
        {
            return _usuarioRepo.Listar();
        }

        public void RemoverUsuario(int id)
        {
            var usuario = _usuarioRepo.BuscarPorId(id);
            if (usuario != null)
            {
                _usuarioRepo.Remover(id);
                Console.WriteLine(" usuario removido com sucesso!");
            }
            else
            {
                Console.WriteLine(" usuario não encontrado!");
            }
        }
    }
}
