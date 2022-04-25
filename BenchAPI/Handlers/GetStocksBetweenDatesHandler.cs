using benchAPI.Data;
using benchAPI.Models;
using benchAPI.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace benchAPI.Handlers;

public class GetStocksBetweenDatesHandler : IRequestHandler<GetStocksBetweenDatesQuery, List<Stock>>
{
    private readonly DataContext _context;
    private readonly IMediator _mediator;

    public GetStocksBetweenDatesHandler(DataContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<List<Stock>> Handle(GetStocksBetweenDatesQuery request, CancellationToken cancellationToken)
    {
        if (_context.Stocks.Any(stock => stock.Date >= request._dateFrom && stock.Date <= request._dateTo) == false)
        {
            var query = new GetAllStocksQuery();
            var result = await _mediator.Send(query);

            var stocksBetweenDates =
                result.Where(stock => stock.Date >= request._dateFrom && stock.Date <= request._dateTo).ToList();
            foreach (var stock in stocksBetweenDates)
            {
                if (_context.Stocks.Any(n => n.Date == stock.Date) == false)
                {
                    _context.Add(stock);
                }
            }
            await _context.SaveChangesAsync();
        }

        return await _context.Stocks.Where(stock => stock.Date >= request._dateFrom && stock.Date <= request._dateTo).ToListAsync();
    }
}