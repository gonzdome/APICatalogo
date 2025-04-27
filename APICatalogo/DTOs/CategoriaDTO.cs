namespace APICatalogo.DTOs;

public class CategoriaDTO
{
    public int CategoriaId { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(20, ErrorMessage = "O nome deve ter entre 5 e 20 caracteres", MinimumLength = 5)]
    [FirstLetterUpperCase]
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
}
