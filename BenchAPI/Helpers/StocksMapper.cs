using System.Globalization;
using benchAPI.Models;

namespace benchAPI.Helpers;

public class StocksMapper
{
    public static List<Stock> ResponseToStocksList(IList<IList<object>> values)
    {
        var stocks = new List<Stock>();

        foreach (var stock in values)
        {
            //if (DateTime.Parse(stock[0].ToString()!) > DateTime.Parse("2018-01-01"))
            stocks.Add(new Stock()
            {
                Date = DateTime.ParseExact(stock[0].ToString()!, Constants.DATE_FORMAT, new CultureInfo("en-US")),
                ClosePrice = Decimal.Parse(stock[1].ToString()!)
            });
        }
        return stocks;
    }
}