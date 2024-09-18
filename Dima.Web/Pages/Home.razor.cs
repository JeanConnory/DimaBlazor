using Dima.Core.Handlers;
using Dima.Core.Models.Reports;
using Dima.Core.Requests.Reports;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages;

public partial class HomePage : ComponentBase
{
    #region Properties

    public bool ShowValues { get; set; } = true;
    public FinancialSummary? Summary { get; set; }

    #endregion

    #region Services

    [Inject]
    public IReportHandler Handler { get; set; } = null!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        await GetFinancialSummaryAsync();
    }

    private async Task GetFinancialSummaryAsync()
    {
        var request = new GetFinancialSummaryRequest();
        var result = await Handler.GetFinancialSummaryAsync(request);
        if (!result.IsSuccess || result.Data is null)
        {
            Snackbar.Add("Falha ao obter dados do relatório", Severity.Error);
            return;
        }

        Summary = result.Data;
    }

    #endregion

    #region Private Methods

    public void ToggleShowValues() => ShowValues = !ShowValues;

    #endregion
}
