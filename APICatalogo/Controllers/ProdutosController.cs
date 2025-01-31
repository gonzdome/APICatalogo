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
    public ActionResult<IEnumerable<Produto>> GetProducts()
    {
        try
        {
            var response = _context.Produtos.AsNoTracking().ToList();
            if (response is null)
                return NotFound("Products not found!");

            return response;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error ocurred when calling: GetProducts");
        }

    }

    [HttpGet("{id:int}")]
    public ActionResult<Produto> GetProductDetailsById(int id)
    {
        try
        {
            var response = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if (response is null)
                return NotFound("Product not found!");

            return response;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error ocurred when calling: GetProductDetailsById");
        }

    }

    [HttpPost("CreateProduct")]
    public ActionResult CreateProduct(Produto product)
    {
        try
        {
            if (product is null)
                return BadRequest();

            _context.Produtos.Add(product);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetProduct", new { id = product.ProdutoId }, product);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error ocurred when calling: CreateProduct");
        }

    }

    [HttpPut("{id:int}")]
    public ActionResult UpdateProductById(int id, Produto product)
    {
        try
        {
            if (id != product.ProdutoId)
                return BadRequest();

            if (product is null)
                return BadRequest();

            _context.Produtos.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(product);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error ocurred when calling: UpdateProductById");
        }

    }

    [HttpDelete("{id:int}")]
    public ActionResult DeleteProductById(int id)
    {
        try
        {
            var product = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if (product is null)
                return NotFound("Product not found!");

            _context.Produtos.Remove(product);
            _context.SaveChanges();

            return Ok(product);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error ocurred when calling: DeleteProductById");
        }

    }
}
