using System.ComponentModel.DataAnnotations;

namespace ApiEcommerce.DTOs.Categorias
{
    public class AtualizarCategoriaRequest
    {
        [Required(ErrorMessage = "O id é obrigatório")]
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 60 caracteres")]
        public string? Nome { get; set; }

        [StringLength(255, ErrorMessage = "A descrição pode ter no máximo 255 caracteres")]
        public string? Descricao { get; set; }
    }
}
