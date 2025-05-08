using APICatalogo.DTOs;
using APICatalogo.DTOs.Mappings;
using APICatalogo.Helpers.Paginate;
using APICatalogo.Models;
using APICatalogo.Repositories;
using APICatalogo.Repositories.Interfaces;
namespace APICatalogo.Services;

public class CategoriaService : ICategoriaService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoriaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetPaginatedCategoriesViewModel> GetPaginatedCategories(Pagination pagination)
    {
        var allCategories = await _unitOfWork.CategoriaRepository.GetPaginatedCategories(pagination);
        var categoriesToDTO = allCategories.Select(c => c.MapToCategoryDTO());

        return PaginatedCategoriesResponse(allCategories, categoriesToDTO);
    }

    public async Task<GetPaginatedCategoriesViewModel> GetFilteredCategories(CategoryNameSearch filters)
    {
        var allCategories = await _unitOfWork.CategoriaRepository.GetFilteredCategories(filters);
        var categoriesToDTO = allCategories.Select(c => c.MapToCategoryDTO());

        return PaginatedCategoriesResponse(allCategories, categoriesToDTO);
    }

    private static GetPaginatedCategoriesViewModel PaginatedCategoriesResponse(PagedList<Categoria> allCategories, IEnumerable<CategoriaDTO> categoriesToDTO)
    {
        GetPaginatedCategoriesViewModel response = new GetPaginatedCategoriesViewModel();

        response.categories = categoriesToDTO;
        response.paginationMetadata = new PaginationMetadata()
        {
            TotalCount = allCategories.TotalCount,
            PageSize = allCategories.PageSize,
            CurrentPage = allCategories.CurrentPage,
            TotalPages = allCategories.TotalPages,
            HasNextPage = allCategories.HasNextPage,
            HasPreviousPage = allCategories.HasPreviousPage,
        };

        return response;
    }

    public async Task<CategoriaDTO> GetCategoryDetailsById(int id)
    {
        var category = await GetAndReturnCategoryById(id);
        return category.MapToCategoryDTO();
    }

    public async Task<IEnumerable<Categoria>> GetProductCategories()
    {
        return await _unitOfWork.CategoriaRepository.GetProductCategories();
    }

    public async Task<CategoriaDTO> CreateCategory(CategoriaDTO categoryPayload)
    {
        Categoria category = categoryPayload.MapToCategory();

        var createdCategory = await _unitOfWork.CategoriaRepository.Create(category);

        _unitOfWork.Commit();

        return createdCategory.MapToCategoryDTO();
    }

    public async Task<CategoriaDTO> UpdateCategoryById(int id, CategoriaDTO categoryToUpdate)
    {
        var category = await GetAndReturnCategoryById(id);

        category.Nome = categoryToUpdate.Nome;
        category.ImagemUrl = categoryToUpdate.ImagemUrl;

        var updatedCategory = await _unitOfWork.CategoriaRepository.Update(category);
        _unitOfWork.Commit();
        return category.MapToCategoryDTO();
    }

    public async Task<CategoriaDTO> DeleteCategoryById(int id)
    {
        var category = await GetAndReturnCategoryById(id);

        var deletedCategory = await _unitOfWork.CategoriaRepository.Delete(category);

        _unitOfWork.Commit();

        return category.MapToCategoryDTO();
    }

    private async Task<Categoria?> GetAndReturnCategoryById(int id)
    {
        var category = await _unitOfWork.CategoriaRepository.Get(p => p.CategoriaId == id);
        if (category == null)
            throw new Exception($"Categoria with id {id} not found!");

        return category;
    }
}
