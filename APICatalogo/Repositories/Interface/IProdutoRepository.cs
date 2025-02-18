using APICatalogo.Models;

namespace APICatalogo.Repositories.Interface;

public interface IProdutoRepository
{
    IEnumerable<Produto> GetProducts();
    Produto GetProductDetailsById(int id);
    Produto CreateProduct(Produto product);
    Produto UpdateProductById(int id, Produto product);
    Produto DeleteProductById(int id);
}
