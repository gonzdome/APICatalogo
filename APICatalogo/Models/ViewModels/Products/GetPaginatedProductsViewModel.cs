namespace APICatalogo.Models.ViewModels.Products;

public class GetPaginatedProductsViewModel
{
    public IEnumerable<ProdutoDTO> products { get; set; }
    public PaginationMetadata paginationMetadata { get; set; }
}
