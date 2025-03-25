using APICatalogo.Models;

namespace APICatalogo.DTOs.Mappings;

public static class CategoriaDTOMappingExtensions
{
    public static CategoriaDTO MapToCategoryDTO(this Categoria category)
    {
        if (category == null)
            return null;

        return new CategoriaDTO()
        {
            CategoriaId = category.CategoriaId,
            Nome = category.Nome,
            ImagemUrl = category.ImagemUrl
        };
    }

    public static Categoria MapToCategory(this CategoriaDTO categoryDTO)
    {
        if (categoryDTO == null)
            return null;

        return new Categoria()
        {
            CategoriaId = categoryDTO.CategoriaId,
            Nome = categoryDTO.Nome,
            ImagemUrl = categoryDTO.ImagemUrl
        };
    }
}
