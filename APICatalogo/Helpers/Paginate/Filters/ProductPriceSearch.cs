namespace APICatalogo.Helpers.Paginate.Filters;

public class ProductPriceSearch : Pagination
{
    public decimal? Price { get; set; }
    public string? Condition { get; set; }
}
