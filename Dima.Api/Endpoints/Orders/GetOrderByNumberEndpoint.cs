﻿using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Orders;

public class GetOrderByNumberEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
      => app.MapGet("/{number}", HandleAsync)
          .WithName("Orders: Get Order by number")
          .WithSummary("Recupera um pedido pelo numero")
          .WithDescription("Recupera um pedido pelo numero")
          .WithOrder(6)
          .Produces<Response<Order?>>();

    private static async Task<IResult> HandleAsync(IOrderHandler handler, ClaimsPrincipal user, string number)
    {
        var request = new GetOrderByNumberRequest
        {
            UserId = user.Identity!.Name ?? string.Empty,
            Number = number
        };

        var result = await handler.GetByNumberAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
