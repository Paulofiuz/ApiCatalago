using Catalago.Context;
using Catalago.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Catalago.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("produtos")]
    public ActionResult<IEnumerable<Categoria>> GetCategoriaProdutos()
    {
        try
        {
            var categorias = _context.Categorias
                                     .Include(p => p.Produtos)
                                     .Where(c => c.CategoriaId <= 5)
                                     .ToList();

            return Ok(categorias);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
        }
    }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        try
        {
            var categorias = _context.Categorias.AsNoTracking().ToList();
            return Ok(categorias);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar categorias: {ex.Message}");
        }
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<Categoria> Get(int id)
    {
        try
        {
            var categoria = _context.Categorias.Find(id);
            if (categoria is null)
            {
                return NotFound("Categoria não encontrada!");
            }

            return Ok(categoria);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar categoria: {ex.Message}");
        }
    }

    [HttpPost]
    public ActionResult Post(Categoria categoria)
    {
        if (categoria is null)
        {
            return BadRequest("Dados inválidos.");
        }

        try
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return CreatedAtRoute("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
        }
        catch (DbUpdateException dbEx)
        {
            return StatusCode(500, $"Erro ao salvar no banco de dados: {dbEx.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro inesperado: {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId)
        {
            return BadRequest("IDs não coincidem.");
        }

        try
        {
            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }
        catch (DbUpdateException dbEx)
        {
            return StatusCode(500, $"Erro ao atualizar no banco de dados: {dbEx.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro inesperado: {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        try
        {
            var categoria = _context.Categorias.Find(id);
            if (categoria is null)
            {
                return NotFound("Categoria não encontrada.");
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(new { message = "Categoria removida com sucesso!" });
        }
        catch (DbUpdateException dbEx)
        {
            return StatusCode(500, $"Erro ao excluir categoria: {dbEx.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro inesperado: {ex.Message}");
        }
    }
}
