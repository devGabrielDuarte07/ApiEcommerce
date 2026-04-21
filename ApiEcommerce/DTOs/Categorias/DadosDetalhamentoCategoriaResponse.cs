
using ApiEcommerce.Models;

namespace ApiEcommerce.DTOs.Categorias
{
    public class DadosDetalhamentoCategoriaResponse
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }

        public DadosDetalhamentoCategoriaResponse() { }

        public DadosDetalhamentoCategoriaResponse(Categoria categoria)
        {
            Id = categoria.Id;
            Nome = categoria.Nome;
            Descricao = categoria.Descricao;
        }
    }
}
