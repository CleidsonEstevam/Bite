#region Manutenção
/*
PROGRAMADOR: CLSILVA
DATA: 26/09/2024
AÇÃO: Implementação inicial
*/
#endregion

using Bite.DAL;
using Npgsql;

namespace Bite.Bo
{
    public class BaseBo : IDisposable
    {
        protected BaseDAL _dal; // Referência à camada DAL
        private NpgsqlTransaction _transaction; // Transação
        private bool _inTransaction; // Indica se estamos em uma transação

        public BaseBo()
        {
            _dal = new BaseDAL(); // Inicializa a DAL
        }

        // Método de validação genérica
        protected virtual bool Validate()
        {
            // Implementar validações comuns aqui
            return true; // Retornar true se válido
        }

        // Método para iniciar uma transação
        protected void BeginTransaction()
        {
            if (_inTransaction)
                throw new InvalidOperationException("Uma transação já está em andamento.");

            _dal.OpenConnection(); // Abre a conexão
            _transaction = _dal.Connection.BeginTransaction(); // Inicia a transação
            _inTransaction = true; // Marca que estamos em uma transação
        }

        // Método para confirmar a transação
        protected void CommitTransaction()
        {
            if (!_inTransaction)
                throw new InvalidOperationException("Nenhuma transação está em andamento.");

            _transaction.Commit(); // Confirma a transação
            _inTransaction = false; // Marca que a transação foi encerrada
            _dal.CloseConnection(); // Fecha a conexão
        }

        // Método para reverter a transação
        protected void RollbackTransaction()
        {
            if (!_inTransaction)
                throw new InvalidOperationException("Nenhuma transação está em andamento.");

            _transaction.Rollback(); // Reverte a transação
            _inTransaction = false; // Marca que a transação foi encerrada
            _dal.CloseConnection(); // Fecha a conexão
        }

        public void Dispose()
        {
            if (_inTransaction)
            {
                RollbackTransaction(); // Reverte a transação se estiver em andamento
            }

            _dal?.Dispose(); // Descartar a DAL se não for nula
        }

        // Outros métodos comuns para manipulação de dados podem ser adicionados aqui
    }
}
