using benchAPI.Models;
using MediatR;

namespace benchAPI.Queries;

public class GetStocksBetweenDatesQuery : IRequest<List<Stock>>
{
    public DateTime _dateFrom { get; }
    public DateTime _dateTo { get; }

    public GetStocksBetweenDatesQuery(DateTime dateFrom, DateTime dateTo)
    {
        _dateFrom = dateFrom;
        _dateTo = dateTo;
    }
}