namespace APICatalogo.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet("GetPaginatedCategories")]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public async Task<ActionResult<PagedList<CategoriaDTO>>> GetPaginatedCategories([FromQuery] Pagination pagination)
    {
        var response = await _categoriaService.GetPaginatedCategories(pagination);

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(response.paginationMetadata));
        
        return Ok(response.categories);
    }

    [HttpGet("GetFilteredCategories")]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public async Task<ActionResult<PagedList<CategoriaDTO>>> GetFilteredCategories([FromQuery] CategoryNameSearch filters)
    {
        var response = await _categoriaService.GetFilteredCategories(filters);

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(response.paginationMetadata));

        return Ok(response.categories);
    }

    [HttpGet("GetProductCategories")]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetProductCategories()
    {
        var categoria = _categoriaService.GetProductCategories();
        return Ok(categoria);
    }

    [HttpGet("{id:int}", Name = "GetCategoryDetailsById")]
    public async Task<ActionResult<CategoriaDTO>> GetCategoryDetailsById(int id)
    {
        var categoria = _categoriaService.GetCategoryDetailsById(id);
        return Ok(categoria);
    }

    [HttpPost("CreateCategory")]
    public ActionResult<CategoriaDTO> CreateCategory(CategoriaDTO categoriaPayload)
    {
        var categoria = _categoriaService.CreateCategory(categoriaPayload);
        return Ok(categoria);
    }

    [HttpPut("{id:int}", Name = "UpdateCategoryById")]
    public ActionResult<CategoriaDTO> UpdateCategoryById(int id, CategoriaDTO categoryToUpdate)
    {
        var categoria = _categoriaService.UpdateCategoryById(id, categoryToUpdate);
        return Ok(categoria);
    }

    [HttpDelete("{id:int}", Name = "DeleteCategoryById")]
    public ActionResult<CategoriaDTO> DeleteCategoryById(int id)
    {
        var categoria = _categoriaService.DeleteCategoryById(id);
        return Ok(categoria);
    }
}
