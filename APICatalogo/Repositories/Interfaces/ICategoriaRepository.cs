using APICatalogo.Models;

namespace APICatalogo.Repositories.Interfaces;

public interface ICategoriaRepository : IGenericRepository<Categoria>
{
    IEnumerable<Categoria> GetProductCategories();
}
