

namespace APICatalogo.Repositories.Interfaces;

public interface IProdutoRepository : IGenericRepository<Produto>
{
    //IEnumerable<Produto> GetPaginatedProducts(Pagination pagination);
    PagedList<Produto> GetPaginatedProducts(Pagination pagination);
    PagedList<Produto> GetFilteredProducts(ProductPriceSearch filter);
    IEnumerable<Produto> GetProductsByCategoryId(int categoryId);
}
