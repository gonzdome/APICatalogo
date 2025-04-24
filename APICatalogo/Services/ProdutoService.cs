namespace APICatalogo.Services;

public class ProdutoService : IProdutoService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProdutoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<Produto>> GetPaginatedProducts(Pagination pagination)
    {
        return _unitOfWork.ProdutoRepository.GetPaginatedProducts(pagination);
    }

    public async Task<IEnumerable<Produto>> GetProducts()
    {
        return _unitOfWork.ProdutoRepository.GetAll();
    }

    public async Task<Produto> GetProductDetailsById(int id)
    {
        var product = GetAndReturnProduct(id);
        return product;
    }

    IEnumerable<Produto> IProdutoService.GetProductsByCategoryId(int categoryId)
    {
        return _unitOfWork.ProdutoRepository.GetProductsByCategoryId(categoryId);
    }

    public Produto CreateProduct(Produto produtoPayload)
    {
        var product = _unitOfWork.ProdutoRepository.Create(produtoPayload);
        _unitOfWork.Commit();
        return product;
    }

    public Produto UpdateProductById(int id, Produto productToUpdate)
    {
        var product = GetAndReturnProduct(id);

        product.Nome = productToUpdate.Nome;
        product.Preco = productToUpdate.Preco;
        product.Descricao = productToUpdate.Descricao;
        product.Estoque = productToUpdate.Estoque;
        product.ImagemUrl = productToUpdate.ImagemUrl;
        product.CategoriaId = productToUpdate.CategoriaId;

        var updatedProduct = _unitOfWork.ProdutoRepository.Update(product);
        _unitOfWork.Commit();
        return updatedProduct;
    }

    public Produto DeleteProductById(int id)
    {
        var product = GetAndReturnProduct(id);
        var deletedProduct = _unitOfWork.ProdutoRepository.Delete(product);
        _unitOfWork.Commit();
        return deletedProduct;
    }
    private Produto? GetAndReturnProduct(int id)
    {
        var product = _unitOfWork.ProdutoRepository.Get(p => p.ProdutoId == id);
        if (product == null)
            throw new Exception($"Product with id {id} not found!");

        return product;
    }
}
