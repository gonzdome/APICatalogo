using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

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
        var response = _context.Produtos.ToList();
        if (response is null)
            return NotFound("Produtos não encontrados!");

        return response;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Produto> GetById(int id)
    {
        var response = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        if (response is null)
            return NotFound("Produto não encontrado!");

        return response;
    }
}
