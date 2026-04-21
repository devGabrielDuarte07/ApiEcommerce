using API_Ecommerce.DTOs.Categorias;
using ApiEcommerce.Data;
using ApiEcommerce.DTOs.Categorias;
using ApiEcommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace ApiEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext db;

        public CategoriaController(AppDbContext context)
        {
            db = context;
        }

        [HttpPost]
        public IActionResult CriarCategoria(CriarCategoriaRequest dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existe = db.Categorias.Any(c => c.Nome == dto.Nome);

            if (existe)
            {
                return BadRequest("Já existe uma categoria com esse nome");
            }
            var categoria = new Categoria()
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao
            };
            db.Categorias.Add(categoria);
            db.SaveChanges();

            return Ok("Categoria criada com sucesso");
        }

        [HttpGet]
        public IActionResult ListarCategoria(int page = 1, int pageSize = 10)
        {
            var query = db.Categorias.Where(c => c.IsAtivo);

            var total = query.Count();

            var categorias = query
                .OrderBy(c => c.Nome)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new DadosListagemCategoriaResponse(c))
                .ToList();

            return Ok(new
            {
                total,
                page,
                pageSize,
                data = categorias
            });
        }

        [HttpGet("{id}")]
        public IActionResult DetalharCategoria(int id)
        {
            var categoria = db.Categorias.Where(c => c.Id == id && c.IsAtivo).Select(c => new DadosDetalhamentoCategoriaResponse(c)).FirstOrDefault();

            if (categoria == null)
            {
                return NotFound("Categoria nao encontrada");
            }

            return Ok(categoria);
        }
        [HttpPut]
        public IActionResult AtualizarCategoria(AtualizarCategoriaRequest dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categoria = db.Categorias.FirstOrDefault(c => c.Id == dto.Id && c.IsAtivo);

            if (categoria == null)
            {
                return NotFound("Categoria não encotrada");
            }

            if (!string.IsNullOrWhiteSpace(dto.Nome))
                categoria.Nome = dto.Nome;

            if (dto.Descricao != null)
                categoria.Descricao = dto.Descricao;

            db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarCategoria(int id)
        {
            var categoria = db.Categorias.FirstOrDefault(c => c.Id == id && c.IsAtivo);

            if (categoria == null) {
                return NotFound("Categoria não encontrada");
            }

            categoria.IsAtivo = false;

            db.SaveChanges();

            return Ok("Categoria deletada com sucesso");
        }
    }
}
