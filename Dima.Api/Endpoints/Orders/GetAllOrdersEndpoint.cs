using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Orders;

public class GetAllOrdersEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
       => app.MapGet("/", HandleAsync)
           .WithName("Orders: Get All")
           .WithSummary("Recupera todos os pedidos")
           .WithDescription("Recupera todos os pedidos")
           .WithOrder(5)
           .Produces<PagedResponse<List<Order>?>>();

    private static async Task<IResult> HandleAsync(IOrderHandler handler, ClaimsPrincipal user,
                [FromQuery] int pageSize = Configuration.DefaultPageSize,
                [FromQuery] int pageNumber = Configuration.DefaultPageNumber)
    {
        var request = new GetAllOrderRequest
        {
            UserId = user.Identity!.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await handler.GetAllAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
