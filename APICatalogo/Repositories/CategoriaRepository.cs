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

    public async Task<PagedList<Categoria>> GetPaginatedCategories(Pagination pagination)
    {
        var categories = await GetAll();
        var orderedCategories = categories.OrderBy(p => p.Nome).AsQueryable();

        var paginatedCategories = PagedList<Categoria>.ToPagedList(orderedCategories, pagination.PageNumber, pagination.PageSize);
        return paginatedCategories;
    }

    public async Task<PagedList<Categoria>> GetFilteredCategories(CategoryNameSearch filters)
    {
        var categories = await GetAll();
        var orderedCategories = categories.OrderBy(p => p.Nome).AsQueryable();

        if (!string.IsNullOrEmpty(filters.Name))
            categories = categories.Where(c => c.Nome.Contains(filters.Name));

        var paginatedCategories = PagedList<Categoria>.ToPagedList(orderedCategories, filters.PageNumber, filters.PageSize);
        return paginatedCategories;
    }

    public async Task<IEnumerable<Categoria>> GetProductCategories()
    {
        return await _context.Categorias.Include(c => c.Produtos).AsNoTracking().ToListAsync();
    }
}
