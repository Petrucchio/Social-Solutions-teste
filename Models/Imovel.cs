using System.ComponentModel.DataAnnotations;

namespace Social_solution_teste.Models
{
    public class Imovel
    {
        public int Id { get; set; }
        public int Cliente_id { get; set; }
        [Required]
        public string Tipo_de_negocio { get; set; }
        [Required]
        public float Valor_imovel { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}
