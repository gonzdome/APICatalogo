using APICatalogo.Models;

namespace APICatalogo.Services.Interfaces;

public interface ICategoriaService
{
    IEnumerable<Categoria> GetCategories();
    IEnumerable<Categoria> GetProductCategories();
    Categoria GetCategoryDetailsById(int id);
    Categoria CreateCategory(Categoria produtoPayload);
    Categoria UpdateCategoryById(int id, Categoria categoryToUpdate);
    Categoria DeleteCategoryById(int id);
}
