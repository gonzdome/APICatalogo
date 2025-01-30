using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("Categories")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        try
        {
            var response = _context.Categorias.AsNoTracking().ToList();
            if (response is null)
                return NotFound("Categories not found!");

            return response;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error ocurred when calling: Get");
        }

    }

    [HttpGet("ProductCategories")]
    public ActionResult<IEnumerable<Categoria>> GetProductCategories()
    {
        try
        {
            var response = _context.Categorias.Include(c => c.Produtos).AsNoTracking().ToList();
            if (response is null)
                return NotFound("Categories not found!");

            return response;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error ocurred when calling: ProductCategories");
        }
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    public ActionResult<Categoria> GetById(int id)
    {
        try
        {
            var response = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
            if (response is null)
                return NotFound("Category not found!");

            return response;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error ocurred when calling: GetById");
        }       
    }

    [HttpPost]
    public ActionResult Post(Categoria categoria)
    {
        try
        {
            if (categoria is null)
                return BadRequest();

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetCategory", new { id = categoria.CategoriaId }, categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error ocurred when calling: Post");
        }  
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Categoria categoria)
    {
        try
        {
            if (id != categoria.CategoriaId)
                return BadRequest();

            if (categoria is null)
                return BadRequest();

            _context.Categorias.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error ocurred when calling: Put");
        }       
    }

    [HttpDelete("{id:int}")]
    public ActionResult DeleteById(int id)
    {
        try
        {
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
            if (categoria is null)
                return NotFound("Category not found!");

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error ocurred when calling: DeleteById");
        }
    }
}
