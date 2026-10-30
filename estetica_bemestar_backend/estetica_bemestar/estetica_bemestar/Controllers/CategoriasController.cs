using estetica_bemestar.Data;
using estetica_bemestar.DTOs;
using estetica_bemestar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace estetica_bemestar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly EsteticaBemestarContext _context;

        // Injeção de dependência do DbContext
        public CategoriasController(EsteticaBemestarContext context)
        {
            _context = context;
        }


        // GET: api/categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategorias()
        {
            var categorias = await _context.Categoria 
                .Select(c => new CategoriaDTO
                {
                    IdCategoria = c.Idcategoria,
                    Categoria = c.Categoria 
                })
                .ToListAsync();

            return Ok(categorias);
        }

        // GET: api/categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaDTO>> GetCategoria(int id)
        {
            var categoria = await _context.Categoria // ⚠️ Se no seu Context for "Categoria", mude aqui
                .Where(c => c.Idcategoria == id)
                .Select(c => new CategoriaDTO
                {
                    IdCategoria = c.Idcategoria,
                    Categoria = c.Categoria // ✅ Corrigido: era Categoria1
                })
                .FirstOrDefaultAsync();

            if (categoria == null)
            {
                return NotFound($"Categoria com ID {id} não encontrada.");
            }

            return Ok(categoria);
        }


        // POST: api/categorias
        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> PostCategoria(CategoriaPutDTO dto)
        {
            // Validação básica
            if (string.IsNullOrWhiteSpace(dto.Categoria))
            {
                return BadRequest("O nome da categoria é obrigatório.");
            }

            // Verifica se já existe categoria com o mesmo nome (campo UNIQUE no banco)
            var existe = await _context.Categoria
                .AnyAsync(c => c.Categoria.ToLower() == dto.Categoria.ToLower());

            if (existe)
            {
                return Conflict($"Já existe uma categoria com o nome '{dto.Categoria}'.");
            }

            var categoria = new Categorium
            {
                Categoria = dto.Categoria.Trim()
            };

            _context.Categoria.Add(categoria);
            await _context.SaveChangesAsync();

            var response = new CategoriaDTO
            {
                IdCategoria = categoria.Idcategoria,
                Categoria = categoria.Categoria
            };

            // Retorna 201 Created com a URL do recurso criado
            return CreatedAtAction(nameof(GetCategoria), new { id = response.IdCategoria }, response);
        }

        // PUT: api/categorias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, CategoriaPutDTO dto)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido.");
            }

            if (string.IsNullOrWhiteSpace(dto.Categoria))
            {
                return BadRequest("O nome da categoria é obrigatório.");
            }

            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound($"Categoria com ID {id} não encontrada.");
            }

            // Verifica se o novo nome já existe em outra categoria
            var existe = await _context.Categoria
                .AnyAsync(c => c.Idcategoria != id && c.Categoria.ToLower() == dto.Categoria.ToLower());

            if (existe)
            {
                return Conflict($"Já existe uma categoria com o nome '{dto.Categoria}'.");
            }

            categoria.Categoria = dto.Categoria.Trim();
            await _context.SaveChangesAsync();

            return NoContent(); // 204 - sucesso sem conteúdo
        }

        // DELETE: api/categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound($"Categoria com ID {id} não encontrada.");
            }

            // ⚠️ Verifica se há itens (produtos/serviços) vinculados a essa categoria
            var temItensVinculados = await _context.Items
                .AnyAsync(i => i.Idcategoria == id);

            if (temItensVinculados)
            {
                return BadRequest(
                    $"Não é possível excluir a categoria '{categoria.Categoria}' " +
                    "porque existem produtos ou serviços vinculados a ela."
                );
            }

            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 - sucesso sem conteúdo
        }
    }
}