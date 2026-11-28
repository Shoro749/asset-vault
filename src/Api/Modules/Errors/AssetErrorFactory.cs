using Application.Assets.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class AssetErrorFactory
{
    public static ObjectResult ToObjectResult(this AssetException error)
    {
        return new ObjectResult(error.Message)
        {
            StatusCode = error switch
            {
                AssetAlreadyExistsException => StatusCodes.Status409Conflict,
                AssetNotFoundException => StatusCodes.Status404NotFound,
                AssetUnhandledException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Asset error handler is not implemented.")
            }
        };
    }
}