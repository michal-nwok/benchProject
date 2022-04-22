using benchAPI.Data;
using benchAPI.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace benchAPI.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class StockController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly DataContext _context;

    public StockController( IMediator mediator, DataContext context)
    {
        _context = context;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStocks()
    {
        var query = new GetAllStocksQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{dateFrom}/{dateTo}")]
    public async Task<IActionResult> GetStocksBetweenDates(DateTime dateFrom, DateTime dateTo)
    {
        if (dateFrom > dateTo)
        {
            return Problem();
        }
        var query = new GetStocksBetweenDatesQuery(dateFrom, dateTo);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}