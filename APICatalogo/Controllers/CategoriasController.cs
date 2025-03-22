using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Services;
using APICatalogo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet("GetCategories")]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategories()
    {
        var categoria = _categoriaService.GetCategories();
        return Ok(categoria);
    }

    [HttpGet("GetProductCategories")]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetProductCategories()
    {
        var categoria = _categoriaService.GetProductCategories();
        return Ok(categoria);
    }

    [HttpGet("{id:int}", Name = "GetCategoryDetailsById")]
    public async Task<ActionResult<CategoriaDTO>> GetCategoryDetailsById(int id)
    {
        var categoria = _categoriaService.GetCategoryDetailsById(id);
        return Ok(categoria);
    }

    [HttpPost("CreateCategory")]
    public ActionResult<CategoriaDTO> CreateCategory(CategoriaDTO categoriaPayload)
    {
        var categoria = _categoriaService.CreateCategory(categoriaPayload);
        return Ok(categoria);
    }

    [HttpPut("{id:int}", Name = "UpdateCategoryById")]
    public ActionResult<CategoriaDTO> UpdateCategoryById(int id, CategoriaDTO categoryToUpdate)
    {
        var categoria = _categoriaService.UpdateCategoryById(id, categoryToUpdate);
        return Ok(categoria);
    }

    [HttpDelete("{id:int}", Name = "DeleteCategoryById")]
    public ActionResult<CategoriaDTO> DeleteCategoryById(int id)
    {
        var categoria = _categoriaService.DeleteCategoryById(id);
        return Ok(categoria);
    }
}
