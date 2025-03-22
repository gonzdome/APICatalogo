using APICatalogo.DTOs;
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

        List<CategoriaDTO> categoriesDTO = new List<CategoriaDTO>();

        var categoriesToDTO = allCategories.Select(c => MapCategoryDTO(c));

        return categoriesToDTO;
    }

    public CategoriaDTO GetCategoryDetailsById(int id)
    {
        var category = GetAndReturnCategory(id);
        return MapCategoryDTO(category);
    }

    public IEnumerable<Categoria> GetProductCategories()
    {
        return _unitOfWork.CategoriaRepository.GetProductCategories();
    }

    public CategoriaDTO CreateCategory(CategoriaDTO categoriaPayload)
    {
        Categoria categoria = new Categoria();

        categoria.Nome = categoriaPayload.Nome;
        categoria.ImagemUrl = categoriaPayload.ImagemUrl;

        var category = _unitOfWork.CategoriaRepository.Create(categoria);

        _unitOfWork.Commit();

        return MapCategoryDTO(category);
    }

    public CategoriaDTO UpdateCategoryById(int id, CategoriaDTO categoryToUpdate)
    {
        var category = GetAndReturnCategory(id);

        category.Nome = categoryToUpdate.Nome;
        category.ImagemUrl = categoryToUpdate.ImagemUrl;

        var updatedCategory = _unitOfWork.CategoriaRepository.Update(category);
        _unitOfWork.Commit();
        return MapCategoryDTO(category);
    }

    public CategoriaDTO DeleteCategoryById(int id)
    {
        var category = GetAndReturnCategory(id);
        var deletedCategory = _unitOfWork.CategoriaRepository.Delete(category);
        _unitOfWork.Commit();
        return MapCategoryDTO(deletedCategory);
    }

    private Categoria? GetAndReturnCategory(int id)
    {
        var category = _unitOfWork.CategoriaRepository.Get(p => p.CategoriaId == id);
        if (category == null)
            throw new Exception($"Categoria with id {id} not found!");

        return category;
    }

    private static CategoriaDTO MapCategoryDTO(Categoria category)
    {
        CategoriaDTO categoryDTO = new CategoriaDTO();
        categoryDTO.CategoriaId = category.CategoriaId;
        categoryDTO.Nome = category.Nome;
        categoryDTO.ImagemUrl = category.ImagemUrl;

        return categoryDTO;
    }
}
