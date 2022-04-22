using benchAPI.Helpers;
using benchAPI.Models;
using benchAPI.Queries;
using Google.Apis.Sheets.v4;
using MediatR;

namespace benchAPI.Handlers;

public class GetAllStocksHandler : IRequestHandler<GetAllStocksQuery, List<Stock>>
{
    private readonly SpreadsheetsResource.ValuesResource _googleSheetValues;
    private readonly string _sheetName;
    private readonly string _spreadsheetId;
    
    public GetAllStocksHandler(GoogleSheetsHelper googleSheetsHelper)
    {
        _googleSheetValues = googleSheetsHelper.Service.Spreadsheets.Values;
        _sheetName = googleSheetsHelper.SheetName;
        _spreadsheetId = googleSheetsHelper.SpreadsheetId;
    }
    
    public async Task<List<Stock>> Handle(GetAllStocksQuery request, CancellationToken cancellationToken)
    {
        var sheetRange = $"{_sheetName}!A2:B";
        var sheetRequest = _googleSheetValues.Get(_spreadsheetId, sheetRange);
        var response = await sheetRequest.ExecuteAsync(cancellationToken);
        var values = response.Values;

        return StocksHelper.ResponseToStocksList(values);
    }
    
}