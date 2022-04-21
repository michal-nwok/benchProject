using System.Globalization;
using benchAPI.Helpers;
using benchAPI.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace benchAPI.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class StockController : ControllerBase
{
    private readonly IMediator _mediator;

    public StockController( IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStocks()
    {
        var query = new GetAllStocksQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // [HttpGet("{dateFrom}/{dateTo}")]
    // public async Task<IActionResult> GetStocksBetweenDates(string dateFrom, string dateTo)
    // {
    //     DateTime dateFromParsed;
    //     DateTime dateToParsed;
    //     
    //     if (!DateTime.TryParseExact(dateFrom, Constants.DATE_FORMAT, new CultureInfo("en-US"), 
    //             DateTimeStyles.None, out dateFromParsed))
    //     {
    //         return Problem();
    //     }
    //     if (!DateTime.TryParseExact(dateTo, Constants.DATE_FORMAT, new CultureInfo("en-US"),
    //             DateTimeStyles.None, out dateToParsed))
    //     {
    //         return Problem();
    //     }
    //
    //     if (dateFromParsed > dateToParsed)
    //     {
    //         return Problem();
    //     }
    //     
    //     return Ok($"{dateFromParsed} - {dateToParsed}");
    // }
}