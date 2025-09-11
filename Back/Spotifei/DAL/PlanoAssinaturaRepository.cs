using MySql.Data.MySqlClient;
using SpotifeiProjeto;
using System;
using System.Collections.Generic;

namespace Spotifei.DAL
{
    public class PlanoAssinaturaRepository : IDisposable
    {
        private readonly DatabaseConnection _dbConnection;

        public PlanoAssinaturaRepository(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void Add(PlanoAssinatura plano)
        {
            var connection = _dbConnection.GetConnection();
            try
            {

                string query = @"INSERT INTO plano_assinatura_tb (tipo_plano, preco_mensal, quantidade_max_perfis, qualidade_musica, quantidade_anuncios)
                             VALUES (@tipo_plano, @preco_mensal, @quantidade_max_perfis, @qualidade_musica, @quantidade_anuncios)";
                using (MySqlCommand comando = new MySqlCommand(query, connection))
                {
                    comando.Parameters.AddWithValue("@tipo_plano", plano.TipoPlano);
                    comando.Parameters.AddWithValue("@preco_mensal", plano.PrecoMensal);
                    comando.Parameters.AddWithValue("@quantidade_max_perfis", plano.MaxPerfis);
                    comando.Parameters.AddWithValue("@qualidade_musica", plano.QualidadeAudio);
                    comando.Parameters.AddWithValue("@quantidade_anuncios", plano.QuantidadeAnuncios);


                    comando.ExecuteNonQuery();
                }

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public List<PlanoAssinatura> GetAll()
        {
            var lista = new List<PlanoAssinatura>();
            var connection = _dbConnection.GetConnection();
            try
            {
                string sql = "SELECT * FROM plano_assinatura_tb";

                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PlanoAssinatura plano = new PlanoAssinatura
                            {
                                Id = reader.GetInt32("id_plano"),
                                TipoPlano = reader.GetString("tipo_plano"),
                                PrecoMensal = reader.GetDecimal("preco_mensal"),
                                MaxPerfis = reader.GetInt32("quantidade_max_perfis"),
                                QualidadeAudio = reader.GetString("qualidade_musica"),
                                QuantidadeAnuncios = reader.GetString("quantidade_anuncios"),
                            };
                            System.Console.WriteLine($"Id: {plano.Id}, Tipo: {plano.TipoPlano}, Preço Mensal: {plano.PrecoMensal}, Perfis: {plano.MaxPerfis}, Qualidade: {plano.QualidadeAudio}, Anúncios: {plano.QuantidadeAnuncios}");
                            lista.Add(plano);
                        }
                    }
                }

            }
            catch (System.Exception e)
            {
                Console.WriteLine($"Erro ao obter planos de assinatura: {e.Message}");
                throw;
            }

            return lista;
        }

        public void Update()
        {
            try
            {
                List<PlanoAssinatura> planos = GetAll();
            System.Console.WriteLine("Selecione o ID do plano que deseja atualizar:");
            int idPlano = int.Parse(Console.ReadLine());

            PlanoAssinatura plano = null;
            foreach (var p in planos)
            {
                if (p.Id == idPlano)
                {
                    plano = p;
                    break;
                }
            }
            if (plano == null)
            {
                Console.WriteLine("Plano não encontrado.");
                return;
            }

            bool rodando = true;
            while (rodando)
            {
                Console.WriteLine($"\nBem vindo ao menu de Escolha!\nVocê escolheu Alterar [{plano.TipoPlano}] [ID: {plano.Id}]");
                Console.WriteLine("\n[1] - Atualizar tipo de plano \n[2] - Atualizar Preço mensal \n[3] - Atualizar Quantidade Maxima de perfil \n[4] - Atualizar qualidade de Áudio \n[5] - Atualizar quantidade de Anúncios \n [6] - Sair");
                int escolha = int.Parse(Console.ReadLine());
                string sql = null;

                var connection = _dbConnection.GetConnection();
                MySqlCommand comando;

                switch (escolha)
                {
                    case 1:
                        sql = "UPDATE plano_assinatura_tb SET tipo_plano = @tipo_plano where id_plano = @id";
                        comando = new MySqlCommand(sql, connection);
                        Console.Write("Novo Tipo de Plano: ");
                        plano.TipoPlano = Console.ReadLine();
                        comando.Parameters.AddWithValue("@Id", idPlano);
                        comando.Parameters.AddWithValue("@tipo_plano", plano.TipoPlano);
                        comando.ExecuteNonQuery();
                        break;
                    case 2:
                        sql = "UPDATE plano_assinatura_tb SET preco_mensal = @preco_mensal where id_plano = @id";
                        comando = new MySqlCommand(sql, connection);
                        Console.Write("Novo Preco Mensal: ");
                        plano.PrecoMensal = decimal.Parse(Console.ReadLine());
                        comando.Parameters.AddWithValue("@Id", idPlano);
                        comando.Parameters.AddWithValue("@preco_mensal", plano.PrecoMensal);
                        comando.ExecuteNonQuery();
                        break;
                    case 3:
                        sql = "UPDATE plano_assinatura_tb SET quantidade_max_perfis = @quantidade_max_perfis where id_plano = @id";
                        comando = new MySqlCommand(sql, connection);
                        Console.Write("Nova Quantidade Maxima de Perfis: ");
                        plano.MaxPerfis = int.Parse(Console.ReadLine());
                        comando.Parameters.AddWithValue("@Id", idPlano);
                        comando.Parameters.AddWithValue("@quantidade_max_perfis", plano.MaxPerfis);
                        comando.ExecuteNonQuery();
                        break;
                    case 4:
                        sql = "UPDATE plano_assinatura_tb SET qualidade_musica = @qualidade_musica where id_plano = @id";
                        comando = new MySqlCommand(sql, connection);
                        Console.Write("Nova qualidade de musica: ");
                        plano.QualidadeAudio = Console.ReadLine();
                        comando.Parameters.AddWithValue("@Id", idPlano);
                        comando.Parameters.AddWithValue("@qualidade_musica", plano.QualidadeAudio);
                        comando.ExecuteNonQuery();
                        break;
                    case 5:
                        sql = "UPDATE plano_assinatura_tb SET quantidade_anuncios  = @quantidade_anuncios where id_plano = @id";
                        comando = new MySqlCommand(sql, connection);
                        Console.Write("Nova Quantidade de Anuncios: ");
                        plano.QuantidadeAnuncios = Console.ReadLine();
                        comando.Parameters.AddWithValue("@Id", idPlano);
                        comando.Parameters.AddWithValue("@quantidade_anuncios", plano.QuantidadeAnuncios);
                        comando.ExecuteNonQuery();
                        break;
                    case 6:
                        Console.WriteLine("Saindo do menu de atualização.");
                        rodando = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida, tente novamente!");
                        continue;
                }
                Console.WriteLine("\nPlano alterado com sucesso!");
                Console.WriteLine("\nDeseja alterar algo novamente? (sim/não)");
                string escolhaMenu = Console.ReadLine();
                if (!escolhaMenu.Equals("sim", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\nFechando sistema de alteração!");
                    rodando = false;
                }
            }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine($"Erro ao atualizar plano de assinatura: {e.Message}");
                throw;
            }
            
        }   

        public void Delete()
        {
            List<PlanoAssinatura> planos = GetAll();

            System.Console.WriteLine("Selecione o ID do plano que deseja deletar:");
            int idPlano = int.Parse(Console.ReadLine());

            PlanoAssinatura planoAssinatura = null;

            foreach (var plano in planos)
            {
                if (plano.Id == idPlano)
                {
                    planoAssinatura = plano;
                    break;
                }
            }
            if (planoAssinatura == null)
            {
                Console.WriteLine("Plano não encontrado.");
                return;
            }

            System.Console.WriteLine("Você escolheu deletar o plano: " + planoAssinatura.TipoPlano);
            var connection = _dbConnection.GetConnection();
            try
            {

                string sql = "DELETE FROM plano_assinatura_tb WHERE id_plano = @id";
                using (MySqlCommand comando = new MySqlCommand(sql, connection))
                {
                    comando.Parameters.AddWithValue("@id", idPlano);
                    comando.ExecuteNonQuery();
                    System.Console.WriteLine("Plano de assinatura deletado com sucesso.");
                }


            }
            catch (System.Exception e)
            {
                Console.WriteLine($"Erro ao deletar plano de assinatura: {e.Message}");
                throw;
            }

        }



    public void Dispose()
        {
            _dbConnection.Dispose();
        }

    }
}