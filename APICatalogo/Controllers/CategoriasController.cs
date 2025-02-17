using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("GetCategories")]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategories()
    {
        var response = await _context.Categorias.AsNoTracking().ToListAsync();
        if (response is null)
            return NotFound("Categories not found!");

        return response;
    }

    [HttpGet("GetProductCategories")]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetProductCategories()
    {
        var response = await _context.Categorias.Include(c => c.Produtos).AsNoTracking().ToListAsync();
        if (response is null)
            return NotFound("Categories not found!");

        return response;
    }

    [HttpGet("{id:int}", Name = "GetCategoryDetailsById")]
    public async Task<ActionResult<Categoria>> GetCategoryDetailsById(int id)
    {
        var response = await _context.Categorias.FirstOrDefaultAsync(p => p.CategoriaId == id);
        if (response is null)
            return NotFound("Category not found!");

        return response;
    }

    [HttpPost("CreateCategory")]
    public ActionResult<Categoria> CreateCategory(Categoria categoria)
    {
        if (categoria is null)
            return BadRequest();

        _context.Categorias.Add(categoria);
        _context.SaveChanges();

        return new CreatedAtRouteResult("GetCategoryDetailsById", new { id = categoria.CategoriaId }, categoria);
    }

    [HttpPut("{id:int}", Name = "UpdateCategoryById")]
    public ActionResult<Categoria> UpdateCategoryById(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId)
            return BadRequest();

        if (categoria is null)
            return BadRequest();

        _context.Categorias.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(categoria);
    }

    [HttpDelete("{id:int}", Name = "DeleteCategoryById")]
    public ActionResult<Categoria> DeleteCategoryById(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
        if (categoria is null)
            return NotFound("Category not found!");

        _context.Categorias.Remove(categoria);
        _context.SaveChanges();

        return Ok(categoria);
    }
}
