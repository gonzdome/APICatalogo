using APICatalogo.DTOs;
using APICatalogo.Models;

namespace APICatalogo.Services.Interfaces;

public interface ICategoriaService
{
    IEnumerable<CategoriaDTO> GetPaginatedCategories(Pagination pagination);
    IEnumerable<Categoria> GetProductCategories();
    CategoriaDTO GetCategoryDetailsById(int id);
    CategoriaDTO CreateCategory(CategoriaDTO produtoPayload);
    CategoriaDTO UpdateCategoryById(int id, CategoriaDTO categoryToUpdate);
    CategoriaDTO DeleteCategoryById(int id);
}
