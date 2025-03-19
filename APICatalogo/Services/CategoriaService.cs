using APICatalogo.Models;
using APICatalogo.Repositories;
using APICatalogo.Repositories.Interfaces;
using APICatalogo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Services;

public class CategoriaService : ICategoriaService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoriaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<Categoria> GetCategories()
    {
        return _unitOfWork.CategoriaRepository.GetAll();
    }

    public Categoria GetCategoryDetailsById(int id)
    {
        var category = GetAndReturnCategory(id);
        return category;
    }

    public IEnumerable<Categoria> GetProductCategories()
    {
        return _unitOfWork.CategoriaRepository.GetProductCategories();
    }

    public Categoria CreateCategory(Categoria categoriaPayload)
    {
        var category = _unitOfWork.CategoriaRepository.Create(categoriaPayload);
        _unitOfWork.Commit();
        return category;
    }

    public Categoria UpdateCategoryById(int id, Categoria categoryToUpdate)
    {
        var category = GetAndReturnCategory(id);

        category.Nome = categoryToUpdate.Nome;
        category.ImagemUrl = categoryToUpdate.ImagemUrl;

        var updatedCategory = _unitOfWork.CategoriaRepository.Update(category);
        _unitOfWork.Commit();
        return updatedCategory;
    }

    public Categoria DeleteCategoryById(int id)
    {
        var category = GetAndReturnCategory(id);
        var deletedCategory = _unitOfWork.CategoriaRepository.Delete(category);
        _unitOfWork.Commit();
        return deletedCategory;
    }

    private Categoria? GetAndReturnCategory(int id)
    {
        var category = _unitOfWork.CategoriaRepository.Get(p => p.CategoriaId == id);
        if (category == null)
            throw new Exception($"Categoria with id {id} not found!");

        return category;
    }
}
