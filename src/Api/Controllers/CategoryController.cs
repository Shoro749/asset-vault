using Api.Dtos;
using Api.Modules.Errors;
using Application.Categories.Commands;
using Application.Common.Interfaces.Queries;
using Domain.Category;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("categories")]
[ApiController]
public class CategoryController(ISender sender, ICategoryQueries categoryQueries) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoryDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var category = await categoryQueries.GetById(new CategoryId(id), cancellationToken);

        return category.Match<ActionResult<CategoryDto>>(
            value => CategoryDto.FromDomainModel(value),
            () => NotFound());
    }
    
    [HttpPost]
    public async Task<ActionResult<CategoryDto>> Create(
        [FromBody] CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(request, cancellationToken);

        return result.Match<ActionResult<CategoryDto>>(
            category => CategoryDto.FromDomainModel(category),
            error => error.ToObjectResult());
    }
}