using benchAPI.Helpers;
using benchAPI.Models;
using Google.Apis.Sheets.v4;
using MediatR;

namespace benchAPI.GetAllStocks
{
    using TRequest = Request;
    using TResponse = Response;
    
    public class Request : IRequest<TResponse>
    {
    }

    public class Response
    {
        public List<Stock> stocks { get; set; } 
    }

    public class Handler : IRequestHandler<TRequest, TResponse>
    {
        private readonly SpreadsheetsResource.ValuesResource _googleSheetValues;
        private readonly string _sheetName;
        private readonly string _spreadsheetId;
        
        public Handler(GoogleSheetsHelper googleSheetsHelper)
        {
            _googleSheetValues = googleSheetsHelper.Service.Spreadsheets.Values;
            _sheetName = googleSheetsHelper.SheetName;
            _spreadsheetId = googleSheetsHelper.SpreadsheetId;
        }
        
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var sheetRange = $"{_sheetName}!A2:B";
            var sheetRequest = _googleSheetValues.Get(_spreadsheetId, sheetRange);
            var response = await sheetRequest.ExecuteAsync(cancellationToken);
            var values = response.Values;

            var listOfStocks = StocksHelper.ResponseToStocksList(values);
            
            return new TResponse
            {
                stocks = listOfStocks,
            };
        }
    }
}