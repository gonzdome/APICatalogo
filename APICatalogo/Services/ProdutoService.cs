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
        var paginatedProducts = await _unitOfWork.ProdutoRepository.GetPaginatedProducts(pagination);
        var paginatedProductsToDTO = paginatedProducts.Select(c => c.MapToProductDTO());

        return PaginatedProductsResponse(paginatedProducts, paginatedProductsToDTO);
    }

    public async Task<GetPaginatedProductsViewModel> GetFilteredProducts(ProductPriceSearch filters)
    {
        var paginatedProducts = await _unitOfWork.ProdutoRepository.GetFilteredProducts(filters);
        var paginatedProductsToDTO = paginatedProducts.Select(c => c.MapToProductDTO());

        return PaginatedProductsResponse(paginatedProducts, paginatedProductsToDTO);
    }

    private static GetPaginatedProductsViewModel PaginatedProductsResponse(PagedList<Produto> paginatedProducts, IEnumerable<ProdutoDTO> paginatedProductsToDTO)
    {
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
        var products = await _unitOfWork.ProdutoRepository.GetAll();
        var productsToDTO = products.Select(c => c.MapToProductDTO());
        return productsToDTO;
    }

    public async Task<ProdutoDTO> GetProductDetailsById(int id)
    {
        var product = await GetAndReturnProduct(id);
        return product.MapToProductDTO();
    }

    public async Task<IEnumerable<Produto>> GetProductsByCategoryId(int productId)
    {
        return await _unitOfWork.ProdutoRepository.GetProductsByCategoryId(productId);
    }

    public async Task<ProdutoDTO> CreateProduct(ProdutoDTO productPayload)
    {
        Produto product = productPayload.MapToProduct();

        var createdProduct = await _unitOfWork.ProdutoRepository.Create(product);

        _unitOfWork.Commit();

        return createdProduct.MapToProductDTO();
    }

    public async Task<ProdutoDTO> UpdateProductById(int id, ProdutoDTO productToUpdate)
    {
        var product = await GetAndReturnProduct(id);

        product.Nome = productToUpdate.Nome;
        product.Preco = productToUpdate.Preco;
        product.Descricao = productToUpdate.Descricao;
        product.Estoque = productToUpdate.Estoque;
        product.ImagemUrl = productToUpdate.ImagemUrl;
        product.CategoriaId = productToUpdate.CategoriaId;

        var updatedProduct = await _unitOfWork.ProdutoRepository.Update(product);
        _unitOfWork.Commit();
        return updatedProduct.MapToProductDTO();
    }

    public async Task<ProdutoDTO> DeleteProductById(int id)
    {
        var product = await GetAndReturnProduct(id);
        var deletedProduct = await _unitOfWork.ProdutoRepository.Delete(product);
        _unitOfWork.Commit();
        return deletedProduct.MapToProductDTO();
    }
    private async Task<Produto?> GetAndReturnProduct(int id)
    {
        var product = await _unitOfWork.ProdutoRepository.Get(p => p.ProdutoId == id);
        if (product == null)
            throw new Exception($"Product with id {id} not found!");

        return product;
    }
}
