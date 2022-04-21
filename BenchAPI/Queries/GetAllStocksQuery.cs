using benchAPI.Models;
using MediatR;

namespace benchAPI.Queries;

public class GetAllStocksQuery : IRequest<List<Stock>>
{
}