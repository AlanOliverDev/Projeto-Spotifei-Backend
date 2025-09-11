using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
namespace Spotifei.DAL
{
    public class ConteudoRepository : IDisposable
    {
        private readonly DatabaseConnection _dbConnection;

        public ConteudoRepository(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

       public int Add(Conteudo conteudo)
{
    var conn = _dbConnection.GetConnection();
    if (conn.State != System.Data.ConnectionState.Open)
        conn.Open();

    using (var tx = conn.BeginTransaction())
    {
        try
        {
            string query = @"INSERT INTO conteudo_tb (titulo, categoria, classificacao, duracao, artista_tb_id_artista, tipo_conteudo)
                VALUES (@titulo, @categoria, @classificacao, @duracao, @artistaId, @tipoConteudo);
                SELECT LAST_INSERT_ID();";
            int idConteudo;

            using (MySqlCommand cmd = new MySqlCommand(query, conn, tx))
            {
                cmd.Parameters.AddWithValue("@titulo", conteudo.Titulo);
                cmd.Parameters.AddWithValue("@categoria", conteudo.Categoria);
                cmd.Parameters.AddWithValue("@classificacao", conteudo.Classificacao ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@duracao", conteudo.Duracao.ToString(@"hh\:mm\:ss"));
                cmd.Parameters.AddWithValue("@artistaId", conteudo.ArtistaId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@tipoConteudo", conteudo.TipoConteudo);
                idConteudo = Convert.ToInt32(cmd.ExecuteScalar());
            }
            if (conteudo is Musica musica)
            {
                string musicaQuery = @"INSERT INTO musica_tb (conteudo_tb_id_conteudo, album_tb_id_album) 
                                       VALUES (@conteudoId, @albumId);";

                using (MySqlCommand cmdMusica = new MySqlCommand(musicaQuery, conn, tx))
                {
                    cmdMusica.Parameters.AddWithValue("@conteudoId", idConteudo);
                    cmdMusica.Parameters.AddWithValue("@albumId", musica.AlbumId ?? (object)DBNull.Value);
                    cmdMusica.ExecuteNonQuery();
                }
            }
            else if (conteudo is Podcast podcast)
            {
                string podcastQuery = @"INSERT INTO podcast_tb (conteudo_tb_id_conteudo, apresentador, numero_episodio) 
                                        VALUES (@conteudoId, @apresentador, @numeroEpisodio);";

                using (MySqlCommand cmdPodcast = new MySqlCommand(podcastQuery, conn, tx))
                {
                    cmdPodcast.Parameters.AddWithValue("@conteudoId", idConteudo);
                    cmdPodcast.Parameters.AddWithValue("@apresentador", podcast.Apresentador);
                    cmdPodcast.Parameters.AddWithValue("@numeroEpisodio", podcast.NumeroEpisodio ?? (object)DBNull.Value);
                    cmdPodcast.ExecuteNonQuery();
                }
            }
            else
            {
                tx.Rollback();
                throw new Exception("Tipo de conteúdo desconhecido.");
            }
            tx.Commit();
            return idConteudo;
        }
        catch (System.Exception)
        {
            tx.Rollback();
            throw;
        }
        finally
        {
            conn.Close();
        }
    }
}

        public Conteudo GetById(int id)
        {
            var conn = _dbConnection.GetConnection();

            try
            {
                string sqlCategoria = "SELECT categoria FROM conteudo_tb WHERE id_conteudo = @id";
                string categoria;
                using (MySqlCommand cmdCategoria = new MySqlCommand(sqlCategoria, conn))
                {
                    cmdCategoria.Parameters.AddWithValue("@id", id);
                    categoria = cmdCategoria.ExecuteScalar()?.ToString();
                    if (categoria == null)
                    {
                        return null;
                    }
                }
                if (categoria.ToLower() == "musica")
                {
                    string sqlMusica = @"SELECT c.*, m.album_tb_id_album
                  FROM conteudo_tb c
                  JOIN musica_tb m ON m.conteudo_tb_id_conteudo = c.id_conteudo
                WHERE c.id_conteudo = @id";
                   using (MySqlCommand cmdMusica = new MySqlCommand(sqlMusica, conn))
                {
                    cmdMusica.Parameters.AddWithValue("@id", id); // ← FALTAVA ISSO
                    using var reader = cmdMusica.ExecuteReader();
                    if (reader.Read())
                        return new Musica
                        {
                            Id = id,
                            Titulo = reader.GetString("titulo"),
                            Categoria = reader.GetString("categoria"),
                            Classificacao = reader.IsDBNull("classificacao") ? null : reader.GetString("classificacao"),
                            Duracao = reader.GetTimeSpan("duracao"),
                            ArtistaId = reader.IsDBNull("artista_tb_id_artista") ? null : reader.GetInt32("artista_tb_id_artista"),
                            TipoConteudo = reader.GetString("tipo_conteudo"),
                            AlbumId = reader.IsDBNull("album_tb_id_album") ? null : reader.GetInt32("album_tb_id_album")
                        };
                }

                }
                else if (categoria.ToLower() == "podcast")
                {

                    string sqlPodcast = @"SELECT c.*, p.apresentador, p.numero_episodio
                  FROM conteudo_tb c
                  JOIN podcast_tb p ON p.conteudo_tb_id_conteudo = c.id_conteudo
                WHERE c.id_conteudo = @id";
                    using (MySqlCommand cmdPodcast = new MySqlCommand(sqlPodcast, conn))
            {
                cmdPodcast.Parameters.AddWithValue("@id", id); // ← FALTAVA ISSO
                using var reader = cmdPodcast.ExecuteReader();
                if (reader.Read())
                    return new Podcast
                    {
                        Id = id,
                        Titulo = reader.GetString("titulo"),
                        Categoria = reader.GetString("categoria"),
                        Classificacao = reader.IsDBNull("classificacao") ? null : reader.GetString("classificacao"),
                        Duracao = reader.GetTimeSpan("duracao"),
                        ArtistaId = reader.IsDBNull("artista_tb_id_artista") ? null : reader.GetInt32("artista_tb_id_artista"),
                        TipoConteudo = reader.GetString("tipo_conteudo"),
                        Apresentador = reader.GetString("apresentador"),
                        NumeroEpisodio = reader.IsDBNull("numero_episodio") ? null : reader.GetString("numero_episodio")
                    };
            }
                }
                throw new Exception("Tipo de conteúdo desconhecido.");
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine($"Erro ao obter conteúdo por ID: {e.Message}");
                throw;
            }
        }

            public void Update(Conteudo conteudo)
{
    var conn = _dbConnection.GetConnection();
    var tx = conn.BeginTransaction();

    try
    {
        Console.WriteLine("Atualizando conteúdo com ID: " + conteudo.Id);

        string categoriaQuery = "SELECT categoria FROM conteudo_tb WHERE id_conteudo = @id";
        string categoria;
        using (var cmd = new MySqlCommand(categoriaQuery, conn, tx))
        {
            cmd.Parameters.AddWithValue("@id", conteudo.Id);
            categoria = cmd.ExecuteScalar()?.ToString();
        }

        if (categoria == null)
        {
            Console.WriteLine("Conteúdo não encontrado.");
            tx.Rollback();
            return;
        }

        if (conteudo is Musica musica)
        {
            string updateMusica = "UPDATE musica_tb SET album_tb_id_album = @albumId WHERE conteudo_tb_id_conteudo = @conteudoId";
            using (var cmd = new MySqlCommand(updateMusica, conn, tx))
            {
                cmd.Parameters.AddWithValue("@albumId", musica.AlbumId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@conteudoId", conteudo.Id);
                cmd.ExecuteNonQuery();
            }
        }
        else if (conteudo is Podcast podcast)
        {
            string updatePodcast = "UPDATE podcast_tb SET apresentador = @apresentador, numero_episodio = @numeroEpisodio WHERE conteudo_tb_id_conteudo = @conteudoId";
            using (var cmd = new MySqlCommand(updatePodcast, conn, tx))
            {
                cmd.Parameters.AddWithValue("@apresentador", podcast.Apresentador);
                cmd.Parameters.AddWithValue("@numeroEpisodio", podcast.NumeroEpisodio ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@conteudoId", conteudo.Id);
                cmd.ExecuteNonQuery();
            }
        }
        else
        {
            throw new Exception("Tipo de conteúdo desconhecido.");
        }

        string updateConteudo = @"UPDATE conteudo_tb SET 
                                titulo = @titulo,
                                categoria = @categoria,
                                classificacao = @classificacao,
                                duracao = @duracao,
                                artista_tb_id_artista = @artistaId,
                                tipo_conteudo = @tipoConteudo
                            WHERE id_conteudo = @id";
        using (MySqlCommand cmd = new MySqlCommand(updateConteudo, conn, tx))
        {
            cmd.Parameters.AddWithValue("@id", conteudo.Id);
            cmd.Parameters.AddWithValue("@titulo", conteudo.Titulo);
            cmd.Parameters.AddWithValue("@categoria", conteudo.Categoria ?? categoria);
            cmd.Parameters.AddWithValue("@classificacao", conteudo.Classificacao ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@duracao", conteudo.Duracao.ToString(@"hh\:mm\:ss"));
            cmd.Parameters.AddWithValue("@artistaId", conteudo.ArtistaId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@tipoConteudo", conteudo.TipoConteudo);
            cmd.ExecuteNonQuery();
        }

        tx.Commit();
        Console.WriteLine("Conteúdo atualizado com sucesso.");
    }
    catch (Exception ex)
    {
        tx.Rollback();
        Console.WriteLine("Erro ao atualizar conteúdo: " + ex.Message);
        throw;
    }
    finally
    {
        conn.Close();
    }
}


public void Delete(int id)
{
    var conn = _dbConnection.GetConnection();
    var tx = conn.BeginTransaction();

    try
    {

        string categoriaQuery = "SELECT categoria FROM conteudo_tb WHERE id_conteudo = @id";
        string categoria;

        using (var cmd = new MySqlCommand(categoriaQuery, conn, tx))
        {
            cmd.Parameters.AddWithValue("@id", id);
            categoria = cmd.ExecuteScalar()?.ToString();
        }

        if (categoria == null)
        {
            Console.WriteLine("Conteúdo não encontrado.");
            tx.Rollback();
            return;
        }

        if (categoria.ToLower().Trim() == "musica")
        {
            string deleteMusica = "DELETE FROM musica_tb WHERE conteudo_tb_id_conteudo = @id";
            using (var cmd = new MySqlCommand(deleteMusica, conn, tx))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
        else if (categoria.ToLower().Trim() == "podcast")
        {
            string deletePodcast = "DELETE FROM podcast_tb WHERE conteudo_tb_id_conteudo = @id";
            using (var cmd = new MySqlCommand(deletePodcast, conn, tx))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
        else
        {
            throw new Exception("Tipo de conteúdo desconhecido.");
        }

        string deleteConteudo = "DELETE FROM conteudo_tb WHERE id_conteudo = @id";
        using (var cmd = new MySqlCommand(deleteConteudo, conn, tx))
        {
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        tx.Commit();
        Console.WriteLine("Conteúdo deletado com sucesso.");
    }
    catch (Exception ex)
    {
        tx.Rollback();
        Console.WriteLine("Erro ao deletar conteúdo: " + ex.Message);
        throw;
    }
}

        public List<Conteudo> ListarTodos()
    {
        var lista = new List<Conteudo>();
        var conn = _dbConnection.GetConnection();

        if (conn.State != System.Data.ConnectionState.Open)
            conn.Open();

        const string sqlMusicas = @"
            SELECT c.*, m.album_tb_id_album
            FROM conteudo_tb c
            JOIN musica_tb m ON m.conteudo_tb_id_conteudo = c.id_conteudo";
        const string sqlPodcasts = @"
            SELECT c.*, p.apresentador, p.numero_episodio
            FROM conteudo_tb c
            JOIN podcast_tb p ON p.conteudo_tb_id_conteudo = c.id_conteudo";

        using (var cmd = new MySqlCommand(sqlMusicas, conn))
        using (var r = cmd.ExecuteReader())
        {
            while (r.Read())
                lista.Add(new Musica
                {
                    Id = r.GetInt32("id_conteudo"),
                    Titulo = r.GetString("titulo"),
                    Categoria = r.GetString("categoria"),
                    Classificacao = r.IsDBNull("classificacao") ? null : r.GetString("classificacao"),
                    Duracao = r.GetTimeSpan("duracao"),
                    ArtistaId = r.IsDBNull("artista_tb_id_artista") ? null : r.GetInt32("artista_tb_id_artista"),
                    TipoConteudo = r.GetString("tipo_conteudo"),
                    AlbumId = r.IsDBNull("album_tb_id_album") ? null : r.GetInt32("album_tb_id_album")
                });
        }

        using (var cmd = new MySqlCommand(sqlPodcasts, conn))
        using (var r = cmd.ExecuteReader())
        {
            while (r.Read())
                lista.Add(new Podcast
                {
                    Id = r.GetInt32("id_conteudo"),
                    Titulo = r.GetString("titulo"),
                    Categoria = r.GetString("categoria"),
                    Classificacao = r.IsDBNull("classificacao") ? null : r.GetString("classificacao"),
                    Duracao = r.GetTimeSpan("duracao"),
                    ArtistaId = r.IsDBNull("artista_tb_id_artista") ? null : r.GetInt32("artista_tb_id_artista"),
                    TipoConteudo = r.GetString("tipo_conteudo"),
                    Apresentador = r.GetString("apresentador"),
                    NumeroEpisodio = r.IsDBNull("numero_episodio") ? null : r.GetString("numero_episodio")
                });
        }

        conn.Close();
        return lista;
    }
    

        public void Dispose()
        {
            _dbConnection.Dispose();
        }
    }
}
