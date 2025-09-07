using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Spotifei.DAL
{
    public class AlbumRepository : IDisposable
    {
        private readonly DatabaseConnection _dbConnection;

        public AlbumRepository(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void Add(Album album, Artista artista)
        {
            var conn = _dbConnection.GetConnection();
            string query = @"INSERT INTO album_tb (nome, data_lancamento, artista_tb_id_artista)
                             VALUES (@nome, @dataLancamento, @artistaId);
                             SELECT LAST_INSERT_ID();";

            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@nome", album.Nome);
                cmd.Parameters.AddWithValue("@dataLancamento", album.DataLancamento);
                cmd.Parameters.AddWithValue("@artistaId", artista.Id);

                album.Id = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public Album GetById(int id)
        {
            var conn = _dbConnection.GetConnection();
            string query = @"SELECT a.*, ar.nome AS nome_artista, ar.nacionalidade, ar.data_nascimento 
                             FROM album_tb a
                             JOIN artista_tb ar ON a.artista_tb_id_artista = ar.id_artista
                             WHERE a.id_album = @id";

            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var artista = new Artista(
                            reader.GetInt32("artista_tb_id_artista"),
                            reader.GetString("nome_artista"),
                            reader.GetString("nacionalidade"),
                            reader.GetDateTime("data_nascimento")
                        );

                        var album = new Album(
                            reader.GetInt32("id_album"),
                            reader.GetString("nome"),
                            reader.GetDateTime("data_lancamento")
                        );

                      
                        return album;
                    }
                }
            }

            return null;
        }

        public void Update(Album album, Artista artista)
        {
            var conn = _dbConnection.GetConnection();
            string query = @"UPDATE album_tb SET 
                                nome = @nome,
                                data_lancamento = @dataLancamento,
                                artista_tb_id_artista = @artistaId
                             WHERE id_album = @id";

            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", album.Id);
                cmd.Parameters.AddWithValue("@nome", album.Nome);
                cmd.Parameters.AddWithValue("@dataLancamento", album.DataLancamento);
                cmd.Parameters.AddWithValue("@artistaId", artista.Id);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            var conn = _dbConnection.GetConnection();
            string query = "DELETE FROM album_tb WHERE id_album = @id";

            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Album> GetAll()
        {
            var conn = _dbConnection.GetConnection();
            var lista = new List<Album>();

            string query = @"SELECT a.*, ar.nome AS nome_artista, ar.nacionalidade, ar.data_nascimento 
                             FROM album_tb a
                             JOIN artista_tb ar ON a.artista_tb_id_artista = ar.id_artista";

            using (var cmd = new MySqlCommand(query, conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var artista = new Artista(
                        reader.GetInt32("artista_tb_id_artista"),
                        reader.GetString("nome_artista"),
                        reader.GetString("nacionalidade"),
                        reader.GetDateTime("data_nascimento")
                    );

                    var album = new Album(
                        reader.GetInt32("id_album"),
                        reader.GetString("nome"),
                        reader.GetDateTime("data_lancamento")
                    );

                    

                    lista.Add(album);
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
