using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace benchAPI;

[ApiController]
[Route("api/stocks")]
public class StockApiController : ControllerBase
{
    private readonly ISender _mediator;

    public StockApiController( ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(type: typeof(GetAllStocks.Response), statusCode: StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllStocks()
    {
        var cmd = new GetAllStocks.Request();
        return Ok(await _mediator.Send(cmd));
    }

    [HttpGet("stocksByDates")]
    [ProducesResponseType(type: typeof(GetStocksBetweenDates.Response), statusCode: StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStocksBetweenDates(
        [FromQuery(Name = "dateFrom")]DateTime dateFrom, 
        [FromQuery(Name = "dateTo")]DateTime dateTo)
    {
        var cmd = new GetStocksBetweenDates.Request(dateFrom, dateTo);
        return Ok(await _mediator.Send(cmd));
    }
}