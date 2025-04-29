using APICatalogo.DTOs.Mappings;

namespace APICatalogo.Services;

public class ProdutoService : IProdutoService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProdutoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetPaginatedProductsViewModel> GetPaginatedProducts(Pagination pagination)
    {
        var paginatedProducts = _unitOfWork.ProdutoRepository.GetPaginatedProducts(pagination);
        var paginatedProductsToDTO = paginatedProducts.Select(c => c.MapToProductDTO());

        GetPaginatedProductsViewModel response = new GetPaginatedProductsViewModel();

        response.products = paginatedProductsToDTO;
        response.paginationMetadata = new PaginationMetadata()
        {
            TotalCount = paginatedProducts.TotalCount,
            PageSize = paginatedProducts.PageSize,
            CurrentPage = paginatedProducts.CurrentPage,
            TotalPages = paginatedProducts.TotalPages,
            HasNextPage = paginatedProducts.HasNextPage,
            HasPreviousPage = paginatedProducts.HasPreviousPage,
        };

        return response;
    }

    public async Task<IEnumerable<ProdutoDTO>> GetProducts()
    {
        var products = _unitOfWork.ProdutoRepository.GetAll();
        var productsToDTO = products.Select(c => c.MapToProductDTO());
        return productsToDTO;
    }

    public async Task<ProdutoDTO> GetProductDetailsById(int id)
    {
        var product = GetAndReturnProduct(id);
        return product.MapToProductDTO();
    }

    public IEnumerable<Produto> GetProductsByCategoryId(int productId)
    {
        return _unitOfWork.ProdutoRepository.GetProductsByCategoryId(productId);
    }

    public ProdutoDTO CreateProduct(ProdutoDTO productPayload)
    {
        Produto product = productPayload.MapToProduct();

        var createdProduct = _unitOfWork.ProdutoRepository.Create(product);

        _unitOfWork.Commit();

        return createdProduct.MapToProductDTO();
    }

    public ProdutoDTO UpdateProductById(int id, ProdutoDTO productToUpdate)
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
        return updatedProduct.MapToProductDTO();
    }

    public ProdutoDTO DeleteProductById(int id)
    {
        var product = GetAndReturnProduct(id);
        var deletedProduct = _unitOfWork.ProdutoRepository.Delete(product);
        _unitOfWork.Commit();
        return deletedProduct.MapToProductDTO();
    }
    private Produto? GetAndReturnProduct(int id)
    {
        var product = _unitOfWork.ProdutoRepository.Get(p => p.ProdutoId == id);
        if (product == null)
            throw new Exception($"Product with id {id} not found!");

        return product;
    }
}
