using ApiEcommerce.Data;
using ApiEcommerce.Models;
using CrudCliente.DTOs.Cliente;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CrudCliente.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        private readonly AppDbContext db;
        public ClienteController(AppDbContext context)
        {
            db = context;
        }

        [HttpPost]
        public IActionResult CriarCliente(CriarClienteRequest dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (db.Clientes.Any(c => c.Email == dto.Email || c.Cpf == dto.Cpf))
            {
                return BadRequest("Email ou CPF já cadastrado");
            }

            var cliente = new Cliente
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Cpf = dto.Cpf,
                Telefone = dto.Telefone,
                IsAtivo = true
            };

            db.Clientes.Add(cliente);
            db.SaveChanges();
            return Ok("Cliente criado com sucesso");
        }

        [HttpGet]
        public IActionResult GetCLientes()
        {
            var clientes = db.Clientes.Where(c => c.IsAtivo).Select(c => new ClienteResponse
            {
                Nome = c.Nome,
                Email = c.Email,
                Cpf = c.Cpf,
                Telefone= c.Telefone,
            }).ToList();

            if (clientes == null) 
            {
                return NotFound("Nenhum cliente cadastrado");
            }

            return Ok(clientes);
        }

        [HttpGet("{Id}")]
        public IActionResult GetClienteId(int Id)
        {
            var cliente = db.Clientes.Where(c =>  c.Id == Id && c.IsAtivo).Select(c => new ClienteResponse
            {
                Nome = c.Nome,
                Email = c.Email,
                Cpf = c.Cpf,
                Telefone = c.Telefone,
            }).FirstOrDefault();

            if (cliente == null)
            {
                return NotFound("Nenhum cliente encontrado");
            }

            return Ok(cliente);
        }

        [HttpPut]
        public IActionResult AtualizarCliente(AtualizarClienteRequest dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cliente = db.Clientes.FirstOrDefault(c => c.Id == dto.Id && c.IsAtivo);

            if(cliente == null)
            {
                return NotFound("CLiente não encontrado");
            }

            if (db.Clientes.Any(c => (c.Email == dto.Email || c.Cpf == dto.Cpf) && c.Id != dto.Id))
            {
                return BadRequest("Email ou CPF já cadastrado");
            }

            cliente.Nome = dto.Nome;
            cliente.Email = dto.Email;
            cliente.Cpf = dto.Cpf;
            cliente.Telefone = dto.Telefone;

            db.SaveChanges();
            return Ok("Cliente atualizado com sucesso");
        }

        [HttpDelete("{Id}")]
        public IActionResult ExcluirClienteId(int Id)
        {
            var cliente = db.Clientes.FirstOrDefault(c => c.Id == Id && c.IsAtivo);
            
            if(cliente == null)
            {
                return NotFound("Cliente não encontrado");
            }

            cliente.IsAtivo = false;
            db.SaveChanges();

            return Ok("Cliente deletado com sucesso");
        }
    }
}
