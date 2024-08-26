using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Transactions;

public class GetTransactionsByPeriodEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
     => app.MapGet("/", HandleAsync)
     .WithName("Transaction: Get All")
     .WithSummary("Recupera todas as transações")
     .WithDescription("Recupera todas as transações")
     .WithOrder(5)
     .Produces<PagedResponse<List<Transaction>?>>();

    private static async Task<IResult> HandleAsync(ITransactionHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber, [FromQuery] int pageSize = Configuration.DefaultPageSize,
        [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
    {
        var request = new GetTransactionByPeriodRequest
        {
            UserId = "test@balta.io",
            PageNumber = pageNumber,
            PageSize = pageSize,
            StartDate = startDate,
            EndDate = endDate
        };

        var result = await handler.GetByPeriodAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
