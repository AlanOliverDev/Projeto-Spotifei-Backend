using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Spotifei.DAL
{
    public class UsuarioRepository : IDisposable
    {
        private readonly DatabaseConnection _dbConnection;

        public UsuarioRepository(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Usuario Login(string email, string senha)
        {
            var conn = _dbConnection.GetConnection();

            try
            {
                string query = @"SELECT id_usuarios, nome, email, senha, tipo_usuario 
                         FROM usuario_tb 
                         WHERE email = @Email AND senha = @Senha AND status = 'ativo'";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Senha", senha);


                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario
                            {
                                Id = reader.GetInt32("id_usuarios"),
                                Nome = reader.GetString("nome"),
                                Email = reader.GetString("email"),
                                Senha = reader.GetString("senha"),
                                TipoUsuario = reader.GetString("tipo_usuario")
                            };
                        }
                    }
                }

                return null; // login inválido
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar logar: {ex.Message}");
                throw;
            }
        }


        public void Add(Usuario usuario)
        {
            var conn = _dbConnection.GetConnection();
            try
            {
                string query = @"INSERT INTO usuario_tb 
                                (nome, email, data_nascimento, status, data_cadastro, pais, estado, genero, plano_assinatura_tb_id_plano, senha, tipo_usuario) 
                                VALUES (@nome, @email, @dataNascimento, @status, @dataCadastro, @pais, @estado, @genero, @planoId, @senha, @tipoUsuario);
                                SELECT LAST_INSERT_ID();";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                    cmd.Parameters.AddWithValue("@email", usuario.Email);
                    cmd.Parameters.AddWithValue("@dataNascimento", usuario.DataNascimento);
                    cmd.Parameters.AddWithValue("@status", usuario.Status);
                    cmd.Parameters.AddWithValue("@dataCadastro", usuario.DataCadastro);
                    cmd.Parameters.AddWithValue("@pais", usuario.Pais);
                    cmd.Parameters.AddWithValue("@estado", usuario.Estado);
                    cmd.Parameters.AddWithValue("@genero", usuario.Genero);
                    cmd.Parameters.AddWithValue("@planoId", usuario.PlanoAssinaturaId);
                    cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                    cmd.Parameters.AddWithValue("@tipoUsuario", usuario.TipoUsuario);

                    usuario.Id = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar usuário: {ex.Message}");
                throw;
            }
        }

        public Usuario GetById(int id)
        {
            var conn = _dbConnection.GetConnection();
            Usuario usuario = null;

            try
            {
                string query = @"SELECT u.*, p.* FROM usuario_tb u
                                JOIN plano_assinatura_tb p ON u.plano_assinatura_tb_id_plano = p.id_plano
                                WHERE u.id_usuarios = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuario(
                                reader.GetInt32("id_usuarios"),
                                reader.GetString("nome"),
                                reader.GetString("email"),
                                reader.GetDateTime("data_nascimento"),
                                reader.GetString("pais"),
                                reader.GetString("estado"),
                                reader.GetString("genero"),
                                reader.GetString("senha"),
                                reader.GetInt32("plano_assinatura_tb_id_plano"),
                                reader.GetString("tipo_usuario")

                            )
                            {
                                Status = reader.GetString("status"),
                                DataCadastro = reader.GetDateTime("data_cadastro")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar usuário: {ex.Message}");
                throw;
            }

            return usuario;
        }

        public void Update(Usuario usuario)
        {
            var conn = _dbConnection.GetConnection();
            try
            {
                string query = @"UPDATE usuario_tb SET 
                                nome = @nome,
                                email = @email,
                                data_nascimento = @dataNascimento,
                                status = @status,
                                pais = @pais,
                                estado = @estado,
                                genero = @genero,
                                plano_assinatura_tb_id_plano = @planoId,
                                senha = @senha,
                                tipo_usuario = @tipoUsuario
                                WHERE id_usuarios = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", usuario.Id);
                    cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                    cmd.Parameters.AddWithValue("@email", usuario.Email);
                    cmd.Parameters.AddWithValue("@dataNascimento", usuario.DataNascimento);
                    cmd.Parameters.AddWithValue("@status", usuario.Status);
                    cmd.Parameters.AddWithValue("@pais", usuario.Pais);
                    cmd.Parameters.AddWithValue("@estado", usuario.Estado);
                    cmd.Parameters.AddWithValue("@genero", usuario.Genero);
                    cmd.Parameters.AddWithValue("@planoId", usuario.PlanoAssinaturaId);
                    cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                    cmd.Parameters.AddWithValue("@tipoUsuario", usuario.TipoUsuario);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar usuário: {ex.Message}");
                throw;
            }
        }

        public void Delete(int id)
        {
            var conn = _dbConnection.GetConnection();
            try
            {
                string query = "DELETE FROM usuario_tb WHERE id_usuarios = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao deletar usuário: {ex.Message}");
                throw;
            }
        }

        public List<Usuario> GetAll()
        {
            var conn = _dbConnection.GetConnection();
            var usuarios = new List<Usuario>();

            try
            {
                string query = @"SELECT u.*, p.* FROM usuario_tb u
                                JOIN plano_assinatura_tb p ON u.plano_assinatura_tb_id_plano = p.id_plano";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(new Usuario(
                                reader.GetInt32("id_usuarios"),
                                reader.GetString("nome"),
                                reader.GetString("email"),
                                reader.GetDateTime("data_nascimento"),
                                reader.GetString("pais"),
                                reader.GetString("estado"),
                                reader.GetString("genero"),
                                reader.GetString("senha"),
                                reader.GetInt32("plano_assinatura_tb_id_plano"),
                                reader.GetString("tipo_usuario")
                            )
                            {
                                Status = reader.GetString("status"),
                                DataCadastro = reader.GetDateTime("data_cadastro")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar usuários: {ex.Message}");
                throw;
            }

            return usuarios;
        }

        public void Dispose()
        {
            _dbConnection.Dispose();
        }
    }
}