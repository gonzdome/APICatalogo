using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }

    public PagedList<Categoria> GetPaginatedCategories(Pagination pagination)
    {
        var categories = GetAll().OrderBy(p => p.Nome).AsQueryable();
        var paginatedCategories = PagedList<Categoria>.ToPagedList(categories, pagination.PageNumber, pagination.PageSize);
        return paginatedCategories;
    }

    public PagedList<Categoria> GetFilteredCategories(CategoryNameSearch filters)
    {
        var categories = GetAll().OrderBy(p => p.Nome).AsQueryable();

        if (!string.IsNullOrEmpty(filters.Name))
            categories = categories.Where(c => c.Nome.Contains(filters.Name));

        var paginatedCategories = PagedList<Categoria>.ToPagedList(categories, filters.PageNumber, filters.PageSize);
        return paginatedCategories;
    }

    public IEnumerable<Categoria> GetProductCategories()
    {
        return _context.Categorias.Include(c => c.Produtos).AsNoTracking().ToList();
    }
}
             