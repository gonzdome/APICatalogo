
using APICatalogo.Helpers.Paginate;
using APICatalogo.Models;

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

    public PagedList<Produto> GetPaginatedProducts(Pagination pagination)
    {
        var products = GetAll().OrderBy(p => p.Nome).AsQueryable();
        var paginatedProducts = PagedList<Produto>.ToPagedList(products, pagination.PageNumber, pagination.PageSize);
        return paginatedProducts;
    }

    public PagedList<Produto> GetFilteredProducts(ProductPriceSearch filter)
    {
        var products = GetAll().OrderBy(p => p.Nome).AsQueryable();

        if (filter.Price.HasValue &&!string.IsNullOrEmpty(filter.Condition))
        {
            decimal preco = filter.Price.Value;

            products = filter.Condition.ToLowerInvariant() switch
            {
                "bigger" => products.Where(p => p.Preco > preco),
                "lesser" => products.Where(p => p.Preco < preco),
                "equals" => products.Where(p => p.Preco == preco),
                _ => products
            };

            products = products.OrderBy(p => p.Preco);
        }

        var filteredProducts = PagedList<Produto>.ToPagedList(products, filter.PageNumber, filter.PageSize);
        return filteredProducts;
    }
}
