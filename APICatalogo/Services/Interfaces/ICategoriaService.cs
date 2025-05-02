namespace APICatalogo.Services.Interfaces;

public interface ICategoriaService
{
    Task<GetPaginatedCategoriesViewModel> GetPaginatedCategories(Pagination pagination);
    Task<GetPaginatedCategoriesViewModel> GetFilteredCategories(CategoryNameSearch filters);
    IEnumerable<Categoria> GetProductCategories();
    CategoriaDTO GetCategoryDetailsById(int id);
    CategoriaDTO CreateCategory(CategoriaDTO produtoPayload);
    CategoriaDTO UpdateCategoryById(int id, CategoriaDTO categoryToUpdate);
    CategoriaDTO DeleteCategoryById(int id);
}
