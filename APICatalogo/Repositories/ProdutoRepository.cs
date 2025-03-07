using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories.Interface;
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
}
