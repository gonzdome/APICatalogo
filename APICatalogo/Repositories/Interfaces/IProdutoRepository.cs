

namespace APICatalogo.Repositories.Interfaces;

public interface IProdutoRepository : IGenericRepository<Produto>
{
    Task<PagedList<Produto>> GetPaginatedProducts(Pagination pagination);
    Task<PagedList<Produto>> GetFilteredProducts(ProductPriceSearch filter);
    Task<IEnumerable<Produto>> GetProductsByCategoryId(int categoryId);
}
