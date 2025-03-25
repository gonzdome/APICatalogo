using APICatalogo.DTOs;
using APICatalogo.DTOs.Mappings;
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

    public IEnumerable<CategoriaDTO> GetCategories()
    {
        IEnumerable<Categoria> allCategories = _unitOfWork.CategoriaRepository.GetAll();

        var categoriesToDTO = allCategories.Select(c => c.MapToCategoryDTO());

        return categoriesToDTO;
    }

    public CategoriaDTO GetCategoryDetailsById(int id)
    {
        var category = GetAndReturnCategoryById(id);
        return category.MapToCategoryDTO();
    }

    public IEnumerable<Categoria> GetProductCategories()
    {
        return _unitOfWork.CategoriaRepository.GetProductCategories();
    }

    public CategoriaDTO CreateCategory(CategoriaDTO categoryPayload)
    {
        Categoria category = categoryPayload.MapToCategory();

        var createdCategory = _unitOfWork.CategoriaRepository.Create(category);

        _unitOfWork.Commit();

        return createdCategory.MapToCategoryDTO();
    }

    public CategoriaDTO UpdateCategoryById(int id, CategoriaDTO categoryToUpdate)
    {
        var category = GetAndReturnCategoryById(id);

        category.Nome = categoryToUpdate.Nome;
        category.ImagemUrl = categoryToUpdate.ImagemUrl;

        var updatedCategory = _unitOfWork.CategoriaRepository.Update(category);
        _unitOfWork.Commit();
        return category.MapToCategoryDTO();
    }

    public CategoriaDTO DeleteCategoryById(int id)
    {
        var category = GetAndReturnCategoryById(id);

        var deletedCategory = _unitOfWork.CategoriaRepository.Delete(category);

        _unitOfWork.Commit();

        return category.MapToCategoryDTO();
    }

    private Categoria? GetAndReturnCategoryById(int id)
    {
        var category = _unitOfWork.CategoriaRepository.Get(p => p.CategoriaId == id);
        if (category == null)
            throw new Exception($"Categoria with id {id} not found!");

        return category;
    }


}
