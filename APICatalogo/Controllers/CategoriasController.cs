using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriasController(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    [HttpGet("GetCategories")]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategories()
    {
        var categoria = _categoriaRepository.GetCategories();
        return Ok(categoria);
    }

    [HttpGet("GetProductCategories")]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetProductCategories()
    {
        var categoria = _categoriaRepository.GetProductCategories();
        return Ok(categoria);
    }

    [HttpGet("{id:int}", Name = "GetCategoryDetailsById")]
    public async Task<ActionResult<Categoria>> GetCategoryDetailsById(int id)
    {
        var categoria = _categoriaRepository.GetCategoryDetailsById(id);
        return Ok(categoria);
    }

    [HttpPost("CreateCategory")]
    public ActionResult<Categoria> CreateCategory(Categoria categoriaPayload)
    {
        var categoria = _categoriaRepository.CreateCategory(categoriaPayload);
        return Ok(categoria);
    }

    [HttpPut("{id:int}", Name = "UpdateCategoryById")]
    public ActionResult<Categoria> UpdateCategoryById(int id, Categoria categoriaPayload)
    {
        var categoria = _categoriaRepository.UpdateCategoryById(id, categoriaPayload);
        return Ok(categoria);
    }

    [HttpDelete("{id:int}", Name = "DeleteCategoryById")]
    public ActionResult<Categoria> DeleteCategoryById(int id)
    {
        var categoria = _categoriaRepository.DeleteCategoryById(id);
        return Ok(categoria);
    }
}
