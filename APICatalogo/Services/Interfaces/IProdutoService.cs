using APICatalogo.Models;

namespace APICatalogo.Services.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<Produto>> GetPaginatedProducts(Pagination pagination);
    Task<IEnumerable<Produto>> GetProducts();
    Task<Produto> GetProductDetailsById(int id);
    IEnumerable<Produto> GetProductsByCategoryId(int categoryId);
    Produto CreateProduct(Produto produtoPayload);
    Produto UpdateProductById(int id, Produto productToUpdate);
    Produto DeleteProductById(int id);
}
