#region Manutenção
/*
PROGRAMADOR: CLSILVA
DATA: 26/09/2024
AÇÃO: Implementação inicial
*/
#endregion

using Microsoft.Extensions.Configuration;
using Npgsql;  // Mudança do MySql.Data.MySqlClient para Npgsql
using System.Data;

namespace Bite.DAL
{
    public class BaseDAL : IDisposable
    {
        private readonly string _connectionString;
        private NpgsqlConnection _connection; // Mudança para NpgsqlConnection
        public NpgsqlConnection Connection => _connection;

        public BaseDAL()
        {
            // Configuração para ler o appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Define o diretório base
                .AddJsonFile("Config/appsettings.json", optional: false, reloadOnChange: true); // Lê o arquivo de configuração

            var configuration = builder.Build();
            _connectionString = configuration.GetConnectionString("DefaultConnection"); // Acessa a string de conexão
        }

        // Método para executar comandos SQL (Stored Procedures)
        protected int ExecuteNonQuery(string storedProcedureName, params NpgsqlParameter[] parameters) // Mudança para NpgsqlParameter
        {
            using (_connection = new NpgsqlConnection(_connectionString)) // Cria a conexão
            using (var command = new NpgsqlCommand(storedProcedureName, _connection) // Mudança para NpgsqlCommand
            {
                CommandType = CommandType.StoredProcedure // Define o tipo de comando como Stored Procedure
            })
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters); // Adiciona os parâmetros, se existirem

                _connection.Open(); // Abre a conexão
                return command.ExecuteNonQuery(); // Executa o comando e retorna o número de linhas afetadas
            } // A conexão será fechada automaticamente aqui
        }

        // Método para executar uma consulta SQL que retorna um DataTable
        protected DataTable ExecuteQuery(string storedProcedureName, params NpgsqlParameter[] parameters) // Mudança para NpgsqlParameter
        {
            using (_connection = new NpgsqlConnection(_connectionString)) // Cria a conexão
            using (var command = new NpgsqlCommand(storedProcedureName, _connection) // Mudança para NpgsqlCommand
            {
                CommandType = CommandType.StoredProcedure // Define o tipo de comando como Stored Procedure
            })
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters); // Adiciona os parâmetros, se existirem

                _connection.Open(); // Abre a conexão
                using (var adapter = new NpgsqlDataAdapter(command)) // Mudança para NpgsqlDataAdapter
                {
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable); // Preenche o DataTable com os resultados da consulta
                    return dataTable; // Retorna o DataTable preenchido
                }
            } // A conexão será fechada automaticamente aqui
        }

        // Método para executar uma consulta SQL que retorna um único valor
        protected object ExecuteValue(string storedProcedureName, params NpgsqlParameter[] parameters) // Mudança para NpgsqlParameter
        {
            using (_connection = new NpgsqlConnection(_connectionString)) // Cria a conexão
            using (var command = new NpgsqlCommand(storedProcedureName, _connection) // Mudança para NpgsqlCommand
            {
                CommandType = CommandType.StoredProcedure // Define o tipo de comando como Stored Procedure
            })
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters); // Adiciona os parâmetros, se existirem

                _connection.Open(); // Abre a conexão
                return command.ExecuteScalar(); // Executa o comando e retorna o primeiro valor da primeira linha
            } // A conexão será fechada automaticamente aqui
        }

        // Método para abrir a conexão
        public void OpenConnection()
        {
            if (_connection == null)
            {
                _connection = new NpgsqlConnection(_connectionString); // Cria a nova conexão
            }

            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open(); // Abre a conexão se estiver fechada
            }
        }

        // Método para fechar a conexão
        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close(); // Fecha a conexão se estiver aberta
            }
        }

        // Implementa IDisposable para liberar recursos
        public void Dispose()
        {
            _connection?.Dispose(); // Descartar a conexão se não for nula
        }
    }
}
