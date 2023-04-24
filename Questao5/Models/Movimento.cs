using System.ComponentModel.DataAnnotations;

namespace Questao5.Models
{
    public class Movimento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string IdentificacaoRequisicao { get; set; }

        [Required]
        public int IdContaCorrente { get; set; }

        [Required]
        public decimal Valor { get; set; }

        [Required]
        [RegularExpression(@"^[CD]$")]
        public string TipoMovimento { get; set; }

        public DateTime DataMovimento { get; set; } = DateTime.Now;

    }
}
