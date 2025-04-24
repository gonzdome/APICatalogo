using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("api/products")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet("GetPaginatedProducts")]
    public async Task<ActionResult<IEnumerable<Produto>>> GetPaginatedProducts([FromQuery] Pagination pagination)
    {
        var produtos = _produtoService.GetPaginatedProducts(pagination);
        return Ok(produtos);
    }

    [HttpGet("GetProducts")]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProducts()
    {
        var produtos =  _produtoService.GetProducts();
        return Ok(produtos);
    }

    [HttpGet("{id:int}", Name = "GetProductDetailsById")]
    public async Task<ActionResult<Produto>> GetProductDetailsById(int id)
    {
        var produto = _produtoService.GetProductDetailsById(id);
        return Ok(produto);
    }

    [HttpPost]
    [Route("CreateProduct")]
    public ActionResult<Produto> CreateProduct(Produto produtoPayload)
    {
        var produto = _produtoService.CreateProduct(produtoPayload);
        return Ok(produto);
    }

    [HttpPut("{id:int}", Name = "UpdateProductById")]
    public ActionResult<Produto> UpdateProductById(int id, Produto productToUpdate)
    {
        var produto = _produtoService.UpdateProductById(id, productToUpdate);
        return Ok(produto);
    }

    [HttpDelete("{id:int}", Name = "DeleteProductById")]
    public ActionResult<Produto> DeleteProductById(int id)
    {
        var produto = _produtoService.DeleteProductById(id);
        return Ok(produto);
    }
}
