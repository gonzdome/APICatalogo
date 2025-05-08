namespace APICatalogo.Services.Interfaces;

public interface ICategoriaService
{
    Task<GetPaginatedCategoriesViewModel> GetPaginatedCategories(Pagination pagination);
    Task<GetPaginatedCategoriesViewModel> GetFilteredCategories(CategoryNameSearch filters);
    Task<IEnumerable<Categoria>> GetProductCategories();
    Task<CategoriaDTO> GetCategoryDetailsById(int id);
    Task<CategoriaDTO> CreateCategory(CategoriaDTO produtoPayload);
    Task<CategoriaDTO> UpdateCategoryById(int id, CategoriaDTO categoryToUpdate);
    Task<CategoriaDTO> DeleteCategoryById(int id);
}
