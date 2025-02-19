using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("api/products")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutosController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    [HttpGet("GetProducts")]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProducts()
    {
        var produto =  _produtoRepository.GetProducts();
        return Ok(produto);
    }

    [HttpGet("{id:int}", Name = "GetProductDetailsById")]
    public async Task<ActionResult<Produto>> GetProductDetailsById(int id)
    {
        var produto = _produtoRepository.GetProductDetailsById(id);
        return Ok(produto);
    }

    [HttpPost]
    [Route("CreateProduct")]
    public ActionResult<Produto> CreateProduct(Produto produtoPayload)
    {
        var produto = _produtoRepository.CreateProduct(produtoPayload);
        return Ok(produto);
    }

    [HttpPut("{id:int}", Name = "UpdateProductById")]
    public ActionResult<Produto> UpdateProductById(int id, Produto produtoPayload)
    {
        var produto = _produtoRepository.UpdateProductById(id, produtoPayload);
        return Ok(produto);
    }

    [HttpDelete("{id:int}", Name = "DeleteProductById")]
    public ActionResult<Produto> DeleteProductById(int id)
    {
        var produto = _produtoRepository.DeleteProductById(id);
        return Ok(produto);
    }
}
