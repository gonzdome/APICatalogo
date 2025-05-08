using APICatalogo.Models;

namespace APICatalogo.Repositories.Interfaces;

public interface ICategoriaRepository : IGenericRepository<Categoria>
{
    Task<PagedList<Categoria>> GetPaginatedCategories(Pagination pagination);
    Task<PagedList<Categoria>> GetFilteredCategories(CategoryNameSearch filters);
    Task<IEnumerable<Categoria>> GetProductCategories();
}
