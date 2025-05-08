namespace APICatalogo.Services.Interfaces;

public interface IProdutoService
{
    Task<GetPaginatedProductsViewModel> GetPaginatedProducts(Pagination pagination);
    Task<GetPaginatedProductsViewModel> GetFilteredProducts(ProductPriceSearch filters);
    Task<IEnumerable<ProdutoDTO>> GetProducts();
    Task<ProdutoDTO> GetProductDetailsById(int id);
    Task<IEnumerable<Produto>> GetProductsByCategoryId(int categoryId);
    Task<ProdutoDTO> CreateProduct(ProdutoDTO produtoPayload);
    Task<ProdutoDTO> UpdateProductById(int id, ProdutoDTO productToUpdate);
    Task<ProdutoDTO> DeleteProductById(int id);
}
