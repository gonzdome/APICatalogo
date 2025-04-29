namespace APICatalogo.Models.ViewModels.Categories;

public class GetPaginatedCategoriesViewModel
{
    public IEnumerable<CategoriaDTO> categories { get; set; }
    public PaginationMetadata paginationMetadata { get; set; }
}
