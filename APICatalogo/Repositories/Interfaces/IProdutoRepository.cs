using APICatalogo.Helpers;
using APICatalogo.Models;

namespace APICatalogo.Repositories.Interfaces;

public interface IProdutoRepository : IGenericRepository<Produto>
{
    IEnumerable<Produto> GetPaginatedProducts(Pagination pagination);
    IEnumerable<Produto> GetProductsByCategoryId(int categoryId);
}
