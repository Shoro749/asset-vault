using Api.Modules.Errors;
using Application.Assets.Commands;
using Application.Common.Interfaces.Queries;
using Domain.Asset;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("assets")]
[ApiController]
public class AssetController(ISender sender, IAssetQueries assetQueries) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AssetDto>>> GetAssets(CancellationToken cancellationToken)
    {
        var assets = await assetQueries.GetAll(cancellationToken);
        
        return assets.Select(AssetDto.FromDomainModel).ToList();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AssetDto>> GetAsset(Guid id, CancellationToken cancellationToken)
    {
        var asset = await assetQueries.GetById(new AssetId(id), cancellationToken);

        return asset.Match<ActionResult<AssetDto>>(
            a => AssetDto.FromDomainModel(a),
            () => NotFound());
    }

    [HttpPost]
    public async Task<ActionResult<AssetDto>> Create([FromBody] AssetDto asset, CancellationToken cancellationToken)
    {
        var request = new CreateAssetCommand
        {
            Title = asset.Title,
            Description = asset.Description,
            Attributes = AssetAttributesDto.ToDomainModel(asset.Attributes)
        };
        
        var result = await sender.Send(request, cancellationToken);

        return result.Match<ActionResult<AssetDto>>(
            a => AssetDto.FromDomainModel(a),
            e => e.ToObjectResult());
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<AssetDto>> Update(
        [FromRoute] Guid id,
        [FromBody] AssetDto asset,
        CancellationToken cancellationToken)
    {
        var input = new UpdateAssetCommand
        {
            AssetId = id,
            Title = asset.Title,
            Description = asset.Description,
        };
        
        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<AssetDto>>(
            a => AssetDto.FromDomainModel(a),
            e => e.ToObjectResult());
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteAsset(Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteAssetCommand { AssetId = id };

        var result = await sender.Send(request, cancellationToken);

        return result.Match<ActionResult>(
            _ => NoContent(),
            e => e.ToObjectResult());
    }
    
    // [HttpPost("assets/{id:guid}/image")]
}