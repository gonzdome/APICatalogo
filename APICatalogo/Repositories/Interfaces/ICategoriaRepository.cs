using APICatalogo.Models;

namespace APICatalogo.Repositories.Interfaces;

public interface ICategoriaRepository : IGenericRepository<Categoria>
{
    PagedList<Categoria> GetPaginatedCategories(Pagination pagination);
    PagedList<Categoria> GetFilteredCategories(CategoryNameSearch filters);
    IEnumerable<Categoria> GetProductCategories();
}
