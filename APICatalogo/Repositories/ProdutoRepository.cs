
using APICatalogo.Helpers.Paginate;
using APICatalogo.Models;

namespace APICatalogo.Repositories;

public class ProdutoRepository : GenericRepository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Produto>> GetProductsByCategoryId(int categoryId)
    {
        var products = await GetAll();
        return products.Where(c => c.CategoriaId == categoryId);
    }

    public async Task<PagedList<Produto>> GetPaginatedProducts(Pagination pagination)
    {
        var products = await GetAll();
        var orderedProducts = products.OrderBy(p => p.Nome).AsQueryable();
        var paginatedProducts = PagedList<Produto>.ToPagedList(orderedProducts, pagination.PageNumber, pagination.PageSize);
        return paginatedProducts;
    }

    public async Task<PagedList<Produto>> GetFilteredProducts(ProductPriceSearch filter)
    {
        var products = await GetAll();
        var orderedProducts = products.OrderBy(p => p.Nome).AsQueryable();

        if (filter.Price.HasValue &&!string.IsNullOrEmpty(filter.Condition))
        {
            decimal preco = filter.Price.Value;

            orderedProducts = filter.Condition.ToLowerInvariant() switch
            {
                "bigger" => orderedProducts.Where(p => p.Preco > preco),
                "lesser" => orderedProducts.Where(p => p.Preco < preco),
                "equals" => orderedProducts.Where(p => p.Preco == preco),
                _ => orderedProducts
            };

            orderedProducts = orderedProducts.OrderBy(p => p.Preco);
        }

        var filteredProducts = PagedList<Produto>.ToPagedList(orderedProducts, filter.PageNumber, filter.PageSize);
        return filteredProducts;
    }
}
