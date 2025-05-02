namespace APICatalogo.Services.Interfaces;

public interface IProdutoService
{
    Task<GetPaginatedProductsViewModel> GetPaginatedProducts(Pagination pagination);
    Task<GetPaginatedProductsViewModel> GetFilteredProducts(ProductPriceSearch filters);
    Task<IEnumerable<ProdutoDTO>> GetProducts();
    Task<ProdutoDTO> GetProductDetailsById(int id);
    IEnumerable<Produto> GetProductsByCategoryId(int categoryId);
    ProdutoDTO CreateProduct(ProdutoDTO produtoPayload);
    ProdutoDTO UpdateProductById(int id, ProdutoDTO productToUpdate);
    ProdutoDTO DeleteProductById(int id);
}
