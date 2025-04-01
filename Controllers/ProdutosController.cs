using Catalago.Context;
using Catalago.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Catalago.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        try
        {
            var produtos = _context.Produtos.ToList();
            if (produtos is null || !produtos.Any())
            {
                return NotFound("Nenhum produto encontrado!");
            }
            return Ok(produtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar produtos: {ex.Message}");
        }
    }

    [HttpGet("{id:int}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        try
        {
            var produto = _context.Produtos.Find(id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado!");
            }
            return Ok(produto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar produto: {ex.Message}");
        }
    }

    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        if (produto is null)
        {
            return BadRequest("Dados inválidos.");
        }

        try
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return CreatedAtRoute("ObterProduto", new { id = produto.ProdutoId }, produto);
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
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
        {
            return BadRequest("IDs não coincidem.");
        }

        try
        {
            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
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
            var produto = _context.Produtos.Find(id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado.");
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(new { message = "Produto removido com sucesso!" });
        }
        catch (DbUpdateException dbEx)
        {
            return StatusCode(500, $"Erro ao excluir produto: {dbEx.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro inesperado: {ex.Message}");
        }
    }
}
