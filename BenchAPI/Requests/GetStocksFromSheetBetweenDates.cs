using benchAPI.Helpers;
using benchAPI.Models;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using MediatR;

namespace benchAPI.GetStocksFromSheetBetweenDates
{
    using TRequest = Request;
    using TResponse = Response;
    
    public class Request : IRequest<TResponse>
    {
        public DateTime DateFrom { get; }
        public DateTime DateTo { get; }

        public Request(DateTime dateFrom, DateTime dateTo)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
        }
    }

    public class Response
    {
        public List<Stock> stocks { get; set; } 
    }

    public class Handler : IRequestHandler<TRequest, TResponse>
    {
        private readonly ISender _mediator;
    
        private readonly SpreadsheetsResource.ValuesResource _googleSheetValues;
        private readonly string _sheetName;
        private readonly string _spreadsheetId;
        
        public Handler(GoogleSheetsHelper googleSheetsHelper, ISender mediator)
        {
            _mediator = mediator;
        
            _googleSheetValues = googleSheetsHelper.Service.Spreadsheets.Values;
            _sheetName = googleSheetsHelper.SheetName;
            _spreadsheetId = googleSheetsHelper.SpreadsheetId;
        }
        
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            await UpdateSheetForDates(request.DateFrom, request.DateTo);

            var cmd = new GetAllStocks.Request();
            var result = await _mediator.Send(cmd);
            
            return new TResponse
            {
                stocks = result.stocks,
            };
        }
        
        private async Task UpdateSheetForDates(DateTime dateFrom, DateTime dateTo)
        {
            var stringToPaste = $"=GOOGLEFINANCE(\"AAPL\"; \"price\"; DATE({dateFrom.Year};{dateFrom.Month};{dateFrom.Day}); DATE({dateTo.Year};{dateTo.Month};{dateTo.Day}); \"DAILY\")"; 
            var sheetRange = $"{_sheetName}!A1";
            var valueRange = new ValueRange();
        
            var objectList = new List<object>() {stringToPaste};
            valueRange.Values = new List<IList<object>>() {objectList};
        
            var updateRequest = _googleSheetValues.Update(valueRange, _spreadsheetId, sheetRange);
            updateRequest.ValueInputOption =
                SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            await updateRequest.ExecuteAsync();
        }
    }
    
    
}
