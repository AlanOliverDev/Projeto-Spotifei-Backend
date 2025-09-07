using System.Runtime.InteropServices.JavaScript;
using SpotifeiProjeto.SpotifeiProjeto;


namespace Spotifei.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Genero { get; set; }
         public int PlanoAssinaturaId { get; set; } // FK
        public string Senha { get; set; }
        public string TipoUsuario { get; set; }

        public Usuario() { }

        public Usuario(int id, string nome, string email, DateTime dataNascimento,
            string pais, string estado, string genero, string senha, int planoId, string tipoUsuario)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            Pais = pais;
            Estado = estado;
            Genero = genero;
            Senha = senha;
            PlanoAssinaturaId = planoId;
            Status = "ativo";
            DataCadastro = DateTime.Now;
            TipoUsuario = tipoUsuario;
        }

        public PerfilUsuario CriarPerfil(string nomePerfil)
        {
            return new PerfilUsuario(0, nomePerfil, this);
        }

        public void AtualizarDados(string novoEmail, string novaSenha)
        {
            Email = novoEmail;
            Senha = novaSenha;
        }

        public void CancelarAssinatura()
        {
            Status = "cancelado";
        }
    }
}