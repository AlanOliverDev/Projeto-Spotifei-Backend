using MySql.Data.MySqlClient;
using System;

namespace Spotifei.DAL
{
    public class DatabaseConnection : IDisposable
    {
        private static string _connectionString = "Server=localhost;Database=Spotifei;Uid=root;Pwd=admin;";
        private MySqlConnection _connection;

        public DatabaseConnection()
        {
            _connection = new MySqlConnection(_connectionString);
        }

        public MySqlConnection GetConnection()
        {
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    _connection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao conectar ao banco: {ex.Message}");
                    throw;
                }
            }
            return _connection;
        }

        public void CloseConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public void Dispose()
        {
            CloseConnection();
            _connection.Dispose();
        }
    }
}