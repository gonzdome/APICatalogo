using APICatalogo.Models;

namespace APICatalogo.Repositories.Interface;

public interface ICategoriaRepository : IGenericRepository<Categoria>
{
    IEnumerable<Categoria> GetProductCategories();
}
