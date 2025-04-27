namespace APICatalogo.DTOs.Mappings;

public static class ProdutoDTOMappingExtensions
{
    public static ProdutoDTO MapToProductDTO(this Produto product)
    {
        if (product == null)
            return null;

        return new ProdutoDTO()
        {
            ProdutoId = product.ProdutoId,
            Nome = product.Nome,
            Descricao = product.Descricao,
            Preco = product.Preco,
            ImagemUrl = product.ImagemUrl,
            Estoque = product.Estoque,
            DataCadastro = product.DataCadastro,
            CategoriaId = product.CategoriaId,
            Categoria = product.Categoria,
        };
    }

    public static Produto MapToProduct(this ProdutoDTO productDTO)
    {
        if (productDTO == null)
            return null;

        return new Produto()
        {
            ProdutoId = productDTO.ProdutoId,
            Nome = productDTO.Nome,
            Descricao = productDTO.Descricao,
            Preco = productDTO.Preco,
            ImagemUrl = productDTO.ImagemUrl,
            Estoque = productDTO.Estoque,
            DataCadastro = productDTO.DataCadastro,
            CategoriaId = productDTO.CategoriaId,
            Categoria = productDTO.Categoria,
        };
    }
}
