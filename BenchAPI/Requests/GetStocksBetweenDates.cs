using benchAPI.Data;
using benchAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace benchAPI.GetStocksBetweenDates
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
        private readonly DataContext _context;
        private readonly IMediator _mediator;

        public Handler(DataContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var cmd = new GetStocksFromSheetBetweenDates.Request(request.DateFrom, request.DateTo);
            var result = await _mediator.Send(cmd);
            var stocksFromSheet = result.stocks;
            
            var currentDatabaseList = _context.Stocks.Where(stock => stock.Date >= request.DateFrom && stock.Date < request.DateTo).ToList();
        
            if (stocksFromSheet.Count == currentDatabaseList.Count)
            {
                return new Response
                {
                    stocks = currentDatabaseList,
                };
            }
        
            foreach (var stock in stocksFromSheet)
            {
                if (_context.Stocks.Any(n => n.Date == stock.Date) == false)
                {
                    _context.Stocks.Add(stock);
                }
            }
            await _context.SaveChangesAsync();
            
            var stocksFromDatabase = await _context.Stocks.Where(stock => stock.Date >= request.DateFrom && stock.Date < request.DateTo).ToListAsync();
            return new Response
            {
                stocks = stocksFromDatabase,
            };
        }
    }
}
