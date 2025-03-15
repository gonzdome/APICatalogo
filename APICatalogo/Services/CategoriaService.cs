using APICatalogo.Models;
using APICatalogo.Repositories;
using APICatalogo.Repositories.Interfaces;
using APICatalogo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaService(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    public IEnumerable<Categoria> GetCategories()
    {
        return _categoriaRepository.GetAll();
    }

    public Categoria GetCategoryDetailsById(int id)
    {
        var category = GetAndReturnCategory(id);
        return category;
    }

    public IEnumerable<Categoria> GetProductCategories()
    {
        return _categoriaRepository.GetProductCategories();
    }

    public Categoria CreateCategory(Categoria categoriaPayload)
    {
        return _categoriaRepository.Create(categoriaPayload);
    }

    public Categoria DeleteCategoryById(int id)
    {
        var category = GetAndReturnCategory(id);
        return _categoriaRepository.Delete(category);
    }

    public Categoria UpdateCategoryById(int id, Categoria categoryToUpdate)
    {
        var category = GetAndReturnCategory(id);

        return _categoriaRepository.Update(categoryToUpdate);
    }

    private Categoria? GetAndReturnCategory(int id)
    {
        var category = _categoriaRepository.Get(p => p.CategoriaId == id);
        if (category == null)
            throw new Exception($"Categoria with id {id} not found!");

        return category;
    }
}
