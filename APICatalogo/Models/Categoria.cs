using APICatalogo.Validation;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models;

[Table("Categorias")]
public class Categoria
{
    public Categoria()
    {
        Produtos = new Collection<Produto>();    
    }

    [Key]
    public int CategoriaId { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(20, ErrorMessage = "O nome deve ter entre 5 e 20 caracteres", MinimumLength = 5)]
    [FirstLetterUpperCase]
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }

    public ICollection<Produto>? Produtos { get; set; }
}
