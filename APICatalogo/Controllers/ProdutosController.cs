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

    [HttpGet("{id:int}", Name="GetProduct")]
    public ActionResult<Produto> GetById(int id)
    {
        var response = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        if (response is null)
            return NotFound("Produto não encontrado!");

        return response;
    }

    [HttpPost]
    public ActionResult Post(Produto product)
    {
        if (product is null)
            return BadRequest();

        _context.Produtos.Add(product);
        _context.SaveChanges();

        return new CreatedAtRouteResult("GetProduct", new { id = product.ProdutoId }, product);
    }
}
