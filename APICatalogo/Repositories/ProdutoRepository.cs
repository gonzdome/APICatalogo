using APICatalogo.Context;
using APICatalogo.Helpers;
using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class ProdutoRepository : GenericRepository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }

    public IEnumerable<Produto> GetProductsByCategoryId(int categoryId)
    {
        return GetAll().Where(c => c.CategoriaId == categoryId);
    }

    public IEnumerable<Produto> GetPaginatedProducts(Pagination pagination)
    {
        return GetAll().OrderBy(p => p.Nome)
                       .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                       .Take(pagination.PageSize).ToList();
    }
}
