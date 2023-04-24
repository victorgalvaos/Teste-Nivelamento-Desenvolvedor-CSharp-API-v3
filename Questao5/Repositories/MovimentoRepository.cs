using Microsoft.Data.Sqlite;
using Questao5.Models;
using System.Data;

namespace Questao5.Repositories
{
    public class MovimentoRepository : IMovimentoRepository
    {
        private readonly string connectionString;

        public MovimentoRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public long InserirMovimento(Movimento Movimento)
        {
            using IDbConnection db = new SQLiteConnection(connectionString);
            db.Open();

            // Verifica se a conta corrente existe e está ativa
            var conta = db.QueryFirstOrDefault<ContaCorrente>("SELECT * FROM ContaCorrente WHERE Id = @Id AND Ativo = 1", new { Id = Movimento.IdContaCorrente });
            if (conta == null)
            {
                throw new ArgumentException("A conta corrente não existe ou não está ativa.", nameof(Movimento));
            }

            // Verifica se o valor é válido
            if (Movimento.Valor <= 0)
            {
                throw new ArgumentException("O valor da movimentação deve ser positivo.", nameof(Movimento));
            }

            // Verifica se o tipo de movimento é válido
            if (Movimento.TipoMovimento != "C" && Movimento.TipoMovimento != "D")
            {
                throw new ArgumentException("O tipo de movimento deve ser 'C' (crédito) ou 'D' (débito).", nameof(Movimento));
            }

            var id = db.ExecuteScalar<long>(
                "INSERT INTO Movimento (IdContaCorrente, TipoMovimento, Valor) VALUES (@IdContaCorrente, @TipoMovimento, @Valor); SELECT last_insert_rowid();",
                new { IdContaCorrente = Movimento.IdContaCorrente, TipoMovimento = Movimento.TipoMovimento, Valor = Movimento.Valor }
            );

            return id;
        }

        public async Task<ContaCorrente> GetContaById(int contaId)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<ContaCorrente>("SELECT * FROM ContaCorrente WHERE Id = @ContaId", new { ContaId = contaId });
            }
        }

        public async Task<decimal> GetSomaCreditos(int contaId)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.ExecuteScalarAsync<decimal>("SELECT SUM(Valor) FROM MovimentacaoConta WHERE ContaCorrenteId = @ContaId AND TipoMovimento = 'C'", new { ContaId = contaId });
            }
        }

        public async Task<decimal> GetSomaDebitos(int contaId)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.ExecuteScalarAsync<decimal>("SELECT SUM(Valor) FROM MovimentacaoConta WHERE ContaCorrenteId = @ContaId AND TipoMovimento = 'D'", new { ContaId = contaId });
            }
        }

    }
}
