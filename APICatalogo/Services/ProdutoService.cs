using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
using APICatalogo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<IEnumerable<Produto>> GetProducts()
    {
        return _produtoRepository.GetAll();
    }

    public async Task<Produto> GetProductDetailsById(int id)
    {
        var product = GetAndReturnProduct(id);
        return product;
    }

    IEnumerable<Produto> IProdutoService.GetProductsByCategoryId(int categoryId)
    {
        return _produtoRepository.GetProductsByCategoryId(categoryId);
    }

    public Produto CreateProduct(Produto produtoPayload)
    {
        var product = _produtoRepository.Create(produtoPayload);
        return product;
    }

    public Produto UpdateProductById(int id, Produto productToUpdate)
    {
        var product = GetAndReturnProduct(id);
        return _produtoRepository.Update(productToUpdate);
    }

    public Produto DeleteProductById(int id)
    {
        var product = GetAndReturnProduct(id);
        return _produtoRepository.Delete(product);
    }
    private Produto? GetAndReturnProduct(int id)
    {
        var product = _produtoRepository.Get(p => p.ProdutoId == id);
        if (product == null)
            throw new Exception($"Product with id {id} not found!");

        return product;
    }
}
