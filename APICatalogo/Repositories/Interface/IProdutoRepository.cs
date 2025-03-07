using APICatalogo.Models;

namespace APICatalogo.Repositories.Interface;

public interface IProdutoRepository : IGenericRepository<Produto>
{
    IEnumerable<Produto> GetProductsByCategoryId(int categoryId);
}
