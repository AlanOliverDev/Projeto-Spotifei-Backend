using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Spotifei.DAL
{
    public class ArtistaRepository : IDisposable
    {
        private readonly DatabaseConnection _dbConnection;

        public ArtistaRepository(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void Add(Artista artista)
        {
            var conn = _dbConnection.GetConnection();
            string query = @"INSERT INTO artista_tb (nome, nacionalidade, data_nascimento)
                             VALUES (@nome, @nacionalidade, @dataNascimento);
                             SELECT LAST_INSERT_ID();";

            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@nome", artista.Nome);
                cmd.Parameters.AddWithValue("@nacionalidade", artista.Nacionalidade);
                cmd.Parameters.AddWithValue("@dataNascimento", artista.DataNascimento);

                artista.Id = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public Artista GetById(int id)
        {
            var conn = _dbConnection.GetConnection();
            string query = @"SELECT * FROM artista_tb WHERE id_artista = @id";

            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Artista(
                            reader.GetInt32("id_artista"),
                            reader.GetString("nome"),
                            reader.GetString("nacionalidade"),
                            reader.GetDateTime("data_nascimento")
                        );
                    }
                }
            }

            return null;
        }

        public void Update(Artista artista)
        {
            var conn = _dbConnection.GetConnection();
            string query = @"UPDATE artista_tb SET 
                                nome = @nome,
                                nacionalidade = @nacionalidade,
                                data_nascimento = @dataNascimento
                             WHERE id_artista = @id";

            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", artista.Id);
                cmd.Parameters.AddWithValue("@nome", artista.Nome);
                cmd.Parameters.AddWithValue("@nacionalidade", artista.Nacionalidade);
                cmd.Parameters.AddWithValue("@dataNascimento", artista.DataNascimento);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            var conn = _dbConnection.GetConnection();
            string query = "DELETE FROM artista_tb WHERE id_artista = @id";

            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Artista> GetAll()
        {
            var conn = _dbConnection.GetConnection();
            var lista = new List<Artista>();

            string query = @"SELECT * FROM artista_tb";

            using (var cmd = new MySqlCommand(query, conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var artista = new Artista(
                        reader.GetInt32("id_artista"),
                        reader.GetString("nome"),
                        reader.GetString("nacionalidade"),
                        reader.GetDateTime("data_nascimento")
                    );

                    lista.Add(artista);
                }
            }

            return lista;
        }

        public void Dispose()
        {
            _dbConnection.Dispose();
        }
    }
}
