using System.ComponentModel.DataAnnotations;

namespace CrudCliente.DTOs.Cliente
{
    public class AtualizarClienteRequest
    {

        [Required]
        public int Id { get; set; }
        
        [Required]
        [MinLength(3), MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(150), EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string Cpf { get; set; }

        [MaxLength(20)]
        public string? Telefone { get; set; }
    }
}
