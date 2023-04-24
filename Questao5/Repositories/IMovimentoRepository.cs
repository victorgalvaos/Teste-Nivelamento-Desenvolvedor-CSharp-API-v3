using Questao5.Models;

namespace Questao5.Repositories
{
    public interface IMovimentoRepository
    {
        Movimento InserirMovimento(Movimento Movimento);
        Task<ContaCorrente> GetContaById(int contaId);
        Task<decimal> GetSomaCreditos(int contaId);
        Task<decimal> GetSomaDebitos(int contaId);
        Task<bool> AdicionarMovimentacao(Movimento movimentacao);
    }

}
