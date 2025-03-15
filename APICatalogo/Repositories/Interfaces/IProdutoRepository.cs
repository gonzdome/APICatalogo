using APICatalogo.Models;

namespace APICatalogo.Repositories.Interfaces;

public interface IProdutoRepository : IGenericRepository<Produto>
{
    IEnumerable<Produto> GetProductsByCategoryId(int categoryId);
}
