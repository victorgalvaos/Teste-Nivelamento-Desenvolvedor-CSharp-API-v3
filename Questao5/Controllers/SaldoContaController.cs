using Microsoft.AspNetCore.Mvc;
using Questao5.Repositories;

namespace Questao5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaldoContaController : ControllerBase
    {
        private readonly IMovimentoRepository _repository;

        public SaldoContaController(IMovimentoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public IActionResult GetSaldoConta(int id)
        {
            // Verifica se a conta existe e está ativa
            var conta = _repository.GetContaById(id);
            if (conta == null)
            {
                return BadRequest(new { Tipo = "INVALID_ACCOUNT", Mensagem = "Conta inexistente." });
            }
            else if (conta.Status == StatusConta.Inativa)
            {
                return BadRequest(new { Tipo = "INACTIVE_ACCOUNT", Mensagem = "Conta inativa." });
            }

            // Calcula o saldo da conta
            decimal saldo = _repository.GetSomaCreditos(id) - _repository.GetSomaDebitos(id);

            // Retorna o saldo da conta
            return Ok(new
            {
                NumeroConta = conta.Numero,
                TitularConta = conta.Titular,
                DataHoraConsulta = DateTime.Now,
                Saldo = saldo
            });
        }
    }
}
