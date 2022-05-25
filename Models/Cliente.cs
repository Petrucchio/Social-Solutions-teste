using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Social_solution_teste.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome é necessário")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "E-mail é necessário")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "CPF é necessário")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "CPF precisa ter 11 digitos no minimo")]
        public string CPF { get; set; }
        public bool Cliente_Ativo { get; set; }

    }
       
}
