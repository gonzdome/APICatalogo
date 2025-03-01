using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public Produto CreateProduct(Produto produto)
    {
        if (produto is null)
            throw new ArgumentNullException(nameof(produto));

        _context.Produtos.Add(produto);
        _context.SaveChanges();

        return produto;
    }

    public Produto DeleteProductById(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        if (produto is null)
            throw new ArgumentNullException(nameof(produto));

        _context.Produtos.Remove(produto);
        _context.SaveChanges();

        return produto;
    }

    public Produto GetProductDetailsById(int id)
    {
        return _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
    }

    public IEnumerable<Produto> GetProducts()
    {
        return _context.Produtos.AsNoTracking().ToList();
    }

    public Produto UpdateProductById(int id, Produto product)
    {
        var productById = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        if (productById is null)
            throw new ArgumentNullException(nameof(productById));

        _context.Produtos.Update(product);
        _context.SaveChanges();

        return product;
    }
}
