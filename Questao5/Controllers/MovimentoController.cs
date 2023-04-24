using Microsoft.AspNetCore.Mvc;
using Questao5.Models;
using Questao5.Repositories;

namespace Questao5.Controllers
{
    public class MovimentoController : Controller
    {
        private readonly IMovimentoRepository _repository;

        public MovimentoController(IMovimentoRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("movimentar")]
        public IActionResult MovimentarConta(Movimento Movimento)
        {
            if (Movimento.Valor <= 0)
                return BadRequest(new { Tipo = "INVALID_VALUE", Mensagem = "O valor deve ser positivo." });

            if (Movimento.TipoMovimento != "C" && Movimento.TipoMovimento != "D")
                return BadRequest(new { Tipo = "INVALID_TYPE", Mensagem = "O tipo de movimento deve ser 'C' ou 'D'." });

            int idMovimento = _repository.InserirMovimento(Movimento);

            return Ok(new { IdMovimento = idMovimento });
        }
    }
}
