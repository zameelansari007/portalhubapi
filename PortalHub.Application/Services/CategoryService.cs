using AutoMapper;
using FluentValidation;
using PortalHub.Application.DTOs.Portal;
using PortalHub.Application.Interfaces.Queries;
using PortalHub.Application.Interfaces.Repositories;
using PortalHub.Domain.Models.Portal;

public class CategoryService :
    CrudService<
        Category,
        CreateCategoryDto,
        UpdateCategoryDto,
        CategoryResponseDto>
{
    public CategoryService(
        IQueryRepository<Category> queryRepo,
        IRepository<Category> commandRepo,
        IMapper mapper,
        IValidator<Category> validator)
        : base(queryRepo, commandRepo, mapper, validator)
    {
    }
}
