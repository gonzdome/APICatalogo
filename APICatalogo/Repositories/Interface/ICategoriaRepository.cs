using APICatalogo.Models;

namespace APICatalogo.Repositories.Interface;

public interface ICategoriaRepository
{
    IEnumerable<Categoria> GetCategories();
    IEnumerable<Categoria> GetProductCategories();
    Categoria GetCategoryDetailsById(int id);
    Categoria CreateCategory(Categoria categoria);
    Categoria UpdateCategoryById(int id, Categoria categoria);
    Categoria DeleteCategoryById(int id);
}
