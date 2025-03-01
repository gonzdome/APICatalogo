using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Categoria> GetCategories()
    {
        return _context.Categorias.AsNoTracking().ToList();
    }

    public Categoria GetCategoryDetailsById(int id)
    {
        return _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
    }

    public IEnumerable<Categoria> GetProductCategories()
    {
        return _context.Categorias.Include(c => c.Produtos).AsNoTracking().ToList();
    }

    public Categoria CreateCategory(Categoria categoria)
    {
        if (categoria is null)
            throw new ArgumentNullException(nameof(categoria));

        _context.Categorias.Add(categoria);
        _context.SaveChanges();

        return categoria;
    }

    public Categoria UpdateCategoryById(int id, Categoria category)
    {
        var categoryById = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
        if (categoryById is null)
            throw new ArgumentNullException(nameof(categoryById));

        _context.Categorias.Entry(category).State = EntityState.Modified;
        _context.SaveChanges();

        return category;
    }

    public Categoria DeleteCategoryById(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
        if (categoria is null)
            throw new ArgumentNullException(nameof(categoria));

        _context.Categorias.Remove(categoria);
        _context.SaveChanges();

        return categoria;
    }
}
             