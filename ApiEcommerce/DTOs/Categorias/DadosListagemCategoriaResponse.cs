using ApiEcommerce.Models;

namespace API_Ecommerce.DTOs.Categorias
{
    public class DadosListagemCategoriaResponse
    {
        public long Id { get; set; }
        public string Nome { get; set; }

        public DadosListagemCategoriaResponse() { }

        public DadosListagemCategoriaResponse(Categoria categoria)
        {
            Id = categoria.Id;
            Nome = categoria.Nome;
        }
    }
}