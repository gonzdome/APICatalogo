﻿using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("api/products")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("GetProducts")]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProducts()
    {
        var response = await _context.Produtos.AsNoTracking().ToListAsync();
        if (response is null)
            return NotFound("Products not found!");

        return response;
    }

    [HttpGet("{id:int}", Name = "GetProductDetailsById")]
    public async Task<ActionResult<Produto>> GetProductDetailsById(int id)
    {
        var response = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id);
        if (response is null)
            return NotFound("Product not found!");

        return response;
    }

    [HttpPost]
    [Route("CreateProduct")]
    public ActionResult<Produto> CreateProduct(Produto product)
    {
        if (product is null)
            return BadRequest();

        _context.Produtos.Add(product);
        _context.SaveChanges();

        return new CreatedAtRouteResult("GetProductDetailsById", new { id = product.ProdutoId }, product);
    }

    [HttpPut("{id:int}", Name = "UpdateProductById")]
    public ActionResult<Produto> UpdateProductById(int id, Produto product)
    {
        if (id != product.ProdutoId)
            return BadRequest();

        if (product is null)
            return BadRequest();

        _context.Produtos.Entry(product).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(product);
    }

    [HttpDelete("{id:int}", Name = "DeleteProductById")]
    public ActionResult<Produto> DeleteProductById(int id)
    {
        var product = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        if (product is null)
            return NotFound("Product not found!");

        _context.Produtos.Remove(product);
        _context.SaveChanges();

        return Ok(product);
    }
}
