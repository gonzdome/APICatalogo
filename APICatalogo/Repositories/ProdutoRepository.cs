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
}
