namespace APICatalogo.Controllers;

[Route("api/products")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet("GetPaginatedProducts")]
    public async Task<ActionResult<PagedList<ProdutoDTO>>> GetPaginatedProducts([FromQuery] Pagination pagination)
    {
        var response = await _produtoService.GetPaginatedProducts(pagination);

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(response.paginationMetadata));

        return Ok(response.products);
    }

    [HttpGet("GetFilteredProducts")]
    public async Task<ActionResult<PagedList<ProdutoDTO>>> GetFilteredProducts([FromQuery] ProductPriceSearch filters)
    {
        var response = await _produtoService.GetFilteredProducts(filters);

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(response.paginationMetadata));

        return Ok(response.products);
    }

    [HttpGet("GetProducts")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProducts()
    {
        var produtos =  _produtoService.GetProducts();
        return Ok(produtos);
    }

    [HttpGet("{id:int}", Name = "GetProductDetailsById")]
    public async Task<ActionResult<ProdutoDTO>> GetProductDetailsById(int id)
    {
        var produto = _produtoService.GetProductDetailsById(id);
        return Ok(produto);
    }

    [HttpPost]
    [Route("CreateProduct")]
    public ActionResult<ProdutoDTO> CreateProduct(ProdutoDTO productPayload)
    {
        var produto = _produtoService.CreateProduct(productPayload);
        return Ok(produto);
    }

    [HttpPut("{id:int}", Name = "UpdateProductById")]
    public ActionResult<ProdutoDTO> UpdateProductById(int id, ProdutoDTO productToUpdate)
    {
        var produto = _produtoService.UpdateProductById(id, productToUpdate);
        return Ok(produto);
    }

    [HttpDelete("{id:int}", Name = "DeleteProductById")]
    public ActionResult<ProdutoDTO> DeleteProductById(int id)
    {
        var produto = _produtoService.DeleteProductById(id);
        return Ok(produto);
    }
}
