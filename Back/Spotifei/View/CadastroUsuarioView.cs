using System;
using System.Collections.Generic;
using Spotifei.Model;
using Spotifei.Controller;

class CadastroUsuarioView
{
    private static UsuarioController _usuarioController = new UsuarioController();
    public static void Exibir(Usuario usuario)
    {
        System.Console.WriteLine("\nDigite seu nome de Usuario: ");
        usuario.Nome = Console.ReadLine();
        System.Console.WriteLine("Digite seu email: ");
        usuario.Email = Console.ReadLine();
        System.Console.WriteLine("Digite sua senha: ");
        usuario.Senha = Console.ReadLine();
        _usuarioController.CadastrarUsuario(usuario);
    }

    public static void ExibirUsuarios()
    {
        var usuarios = _usuarioController.ListarUsuarios();

        if (usuarios.Count == 0)
        {
            Console.WriteLine("\nNenhum usuário cadastrado.");
        }
        else
        {
            Console.WriteLine("\n--- Lista de Usuários ---");
            foreach (var usuario in usuarios)
            {
                Console.WriteLine($"ID: {usuario.Id}, Nome: {usuario.Nome}, Email: {usuario.Email}");
            }
        }
    }
    
     public static void RemoverUsuario()
    {
        Console.WriteLine("\nDigite o ID do usuário que deseja remover: ");
        int id = int.Parse(Console.ReadLine());

        _usuarioController.RemoverUsuario(id);
    }
}