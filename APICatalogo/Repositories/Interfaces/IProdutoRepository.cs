

namespace APICatalogo.Repositories.Interfaces;

public interface IProdutoRepository : IGenericRepository<Produto>
{
    //IEnumerable<Produto> GetPaginatedProducts(Pagination pagination);
    PagedList<Produto> GetPaginatedProducts(Pagination pagination);
    IEnumerable<Produto> GetProductsByCategoryId(int categoryId);
}
