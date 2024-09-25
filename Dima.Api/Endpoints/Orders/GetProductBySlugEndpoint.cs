using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Orders;

public class GetProductBySlugEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
     => app.MapGet("/{slug}", HandleAsync)
         .WithName("Products: Get Product by slug")
         .WithSummary("Recupera um produto pelo slug")
         .WithDescription("Recupera um produto pelo slug")
         .WithOrder(4)
         .Produces<Response<Product?>>();

    private static async Task<IResult> HandleAsync(IProductHandler handler, string slug)
    {
        var request = new GetProductBySlugRequest
        {
            Slug = slug
        };

        var result = await handler.GetBySlugAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
